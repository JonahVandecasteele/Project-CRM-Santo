using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office365.Discovery;
using Microsoft.Office365.OutlookServices;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using CRMSanto.Utils;
using System.Security.Claims;
using System.Web.Mvc.Filters;


namespace CRMSanto.Controllers
{
    public class HomeController : Controller
    {
        private IAfspraakService afs;

        public HomeController(IAfspraakService afs)
        {
            this.afs = afs;
        }

        public ActionResult Index()
        {
            List<Afspraak> afspraken = afs.GetAfsprakenToday();
            return View(afspraken);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Photo()
        {
            if (TempData["PhotoURL"] != null)
            {
                string base64AuthCert = (string)TempData["PhotoURL"];
                byte[] encodedAuthCert = Convert.FromBase64String(base64AuthCert);
                var stream = new MemoryStream(encodedAuthCert);
                return new FileStreamResult(stream, "image/jpg");
            }
            else
            {
                return new EmptyResult();
            }
        }

       
        public ActionResult EIDTest()
        {
            
            Klant klant = new Klant();
            if (Request.Form["nl"] == null)
            {
                OpenIdRelyingParty openid = new OpenIdRelyingParty();
                openid.SecuritySettings.AllowDualPurposeIdentifiers = true;
                IAuthenticationResponse response = openid.GetResponse();
                if (null != response)
                {
                    if (response.Status == AuthenticationStatus.Authenticated)
                    {
                        ViewBag.Name = "Data Request Complete";
                        FetchResponse fetchResponse = response.GetExtension<FetchResponse>();
                        klant.Voornaam = fetchResponse.Attributes["http://axschema.org/namePerson/first"].Values[0];
                        klant.Naam = fetchResponse.Attributes["http://axschema.org/namePerson/last"].Values[0];
                        klant.Geslacht = new Geslacht() { Naam = fetchResponse.Attributes["http://axschema.org/person/gender"].Values[0] };                 //ID uit DB Halen
                        string input = fetchResponse.Attributes["http://axschema.org/contact/postalAddress/home"].Values[0] ;
                        Match match = Regex.Match(input, @"^(.+)\s(\d+(\s*[^\d\s]+)*)$", RegexOptions.IgnoreCase);
                        if (match.Success)
	                    {
                            string[] straat = match.Groups[1].Value.Split('(');
                            klant.Adres = new Adres() { Straat = straat[0], Nummer = match.Groups[2].Value, Postbus = match.Groups[3].Value, Postcode = fetchResponse.Attributes["http://axschema.org/contact/postalCode/home"].Values[0]/*, Gemeente = fetchResponse.Attributes["http://axschema.org/contact/city/home"].Values[0]*/ };
	                    }
                        

                        string birthYear = fetchResponse.Attributes["http://openid.net/schema/birthDate/birthYear"].Values[0];
                        string birthMonth = fetchResponse.Attributes["http://openid.net/schema/birthDate/birthMonth"].Values[0];
                        string birthday = fetchResponse.Attributes["http://openid.net/schema/birthDate/birthday"].Values[0];
                        klant.Geboortedatum = Convert.ToDateTime(birthday + "/" + birthMonth + "/" + birthYear);
                        ////////
                        string base64PhotoSafe = fetchResponse.Attributes["http://axschema.org/eid/photo"].Values[0];
                        string base64Photo = base64PhotoSafe.Replace('-', '+').Replace('_', '/');
                        if (base64Photo.Length % 4 > 0)
                            base64Photo = base64Photo.PadRight(base64Photo.Length + 4 - base64Photo.Length % 4, '=');
                        TempData["PhotoURL"] = base64Photo;
                        TempData["Local"] = klant.Adres;
                        klant.Foto = base64Photo;
                        
                    }
                    else
                    {
                        ViewBag.Name = "Autentication failed";
                    }
                }
                else
                {
                    if (Request.Form["maps"] != null)
                    {
                        Adres add = (Adres)TempData["Local"];
                        if(add != null)
                        Response.Redirect("http://maps.google.com/maps?q=" + add.Straat + " " + add.Nummer + " " + add.Postcode);
                    }
                    ViewBag.Name = "Not logged in yet.";
                }
            }
            else
            {
                OpenIdRelyingParty openid = new OpenIdRelyingParty();
                IAuthenticationRequest request =
                     openid.CreateRequest("https://www.e-contract.be/eid-idp/endpoints/openid/ident");

                // attribute query
                FetchRequest fetchRequest = new FetchRequest();
                fetchRequest.Attributes.AddRequired("http://axschema.org/namePerson/first");
                fetchRequest.Attributes.AddRequired("http://axschema.org/namePerson/last");
                fetchRequest.Attributes.AddRequired("http://openid.net/schema/birthDate/birthYear");
                fetchRequest.Attributes.AddRequired("http://openid.net/schema/birthDate/birthMonth");
                fetchRequest.Attributes.AddRequired("http://openid.net/schema/birthDate/birthday");
                fetchRequest.Attributes.AddRequired("http://axschema.org/person/gender");
                fetchRequest.Attributes.AddRequired("http://axschema.org/contact/postalAddress/home");
                fetchRequest.Attributes.AddRequired("http://axschema.org/contact/postalCode/home");
                fetchRequest.Attributes.AddRequired("http://axschema.org/contact/city/home");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/photo");
                request.AddExtension(fetchRequest);
                //
                request.RedirectToProvider();
            }
            return View(klant);
        }
        public async Task<ActionResult> Office()
        {
            List<MyContact> myContacts = new List<MyContact>();

            var signInUserId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userObjectId = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(SettingsHelper.Authority, new ADALTokenCache(signInUserId));

            try
            {
                DiscoveryClient discClient = new DiscoveryClient(SettingsHelper.DiscoveryServiceEndpointUri,
                    async () =>
                    {
                        var authResult = await authContext.AcquireTokenSilentAsync(SettingsHelper.DiscoveryServiceResourceId, new ClientCredential(SettingsHelper.ClientId, SettingsHelper.AppKey), new UserIdentifier(userObjectId, UserIdentifierType.UniqueId));

                        return authResult.AccessToken;
                    });

                var dcr = await discClient.DiscoverCapabilityAsync("Contacts");

                OutlookServicesClient exClient = new OutlookServicesClient(dcr.ServiceEndpointUri,
                    async () =>
                    {
                        var authResult = await authContext.AcquireTokenSilentAsync(dcr.ServiceResourceId, new ClientCredential(SettingsHelper.ClientId, SettingsHelper.AppKey), new UserIdentifier(userObjectId, UserIdentifierType.UniqueId));

                        return authResult.AccessToken;
                    });

                var contactsResult = await exClient.Me.Contacts.ExecuteAsync();

                do
                {
                    var contacts = contactsResult.CurrentPage;
                    foreach (var contact in contacts)
                    {
                        myContacts.Add(new MyContact { Name = contact.DisplayName });
                    }

                    contactsResult = await contactsResult.GetNextPageAsync();

                } while (contactsResult != null);
            }
            catch (AdalException exception)
            {
                //handle token acquisition failure
                if (exception.ErrorCode == AdalError.FailedToAcquireTokenSilently)
                {
                    authContext.TokenCache.Clear();

                    //handle token acquisition failure
                }
            }

            return View(myContacts);
        }
        public class MyContact
        {
            public string Name { get; set; }
        }
    }
}