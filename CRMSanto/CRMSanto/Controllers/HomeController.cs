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
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
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
                            klant.Adres = new Adres() { Straat = match.Groups[1].Value, Nummer = match.Groups[2].Value,  Postbus = match.Groups[3].Value, Postcode = fetchResponse.Attributes["http://axschema.org/contact/postalCode/home"].Values[0]/*, Gemeente = fetchResponse.Attributes["http://axschema.org/contact/city/home"].Values[0]*/ };
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
                        klant.Foto = base64Photo;
                        
                    }
                    else
                    {
                        ViewBag.Name = "Autentication failed";
                    }
                }
                else
                {
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
    }
}