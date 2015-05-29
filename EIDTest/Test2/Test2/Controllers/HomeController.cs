using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.UI;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Test2.Controllers
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
        public ActionResult Test()
        {
            if (Request.Form["nl"] == null)
            {
                OpenIdRelyingParty openid = new OpenIdRelyingParty();
                openid.SecuritySettings.AllowDualPurposeIdentifiers = true;
                IAuthenticationResponse response = openid.GetResponse();
                if (null != response)
                {
                    if (response.Status == AuthenticationStatus.Authenticated)
                    {
                        FetchResponse fetchResponse = response.GetExtension<FetchResponse>();
                        ViewBag.Name = fetchResponse.Attributes["http://axschema.org/namePerson"].Values[0];
                        string base64PhotoSafe = fetchResponse.Attributes["http://axschema.org/eid/photo"].Values[0];
                        //string base64UrlSafeAuthCert = fetchResponse.Attributes["http://axschema.org/eid/cert/auth"].Values[0];
                        string base64Photo = base64PhotoSafe.Replace('-', '+').Replace('_', '/');
                        if (base64Photo.Length % 4 > 0)
                            base64Photo = base64Photo.PadRight(base64Photo.Length + 4 - base64Photo.Length % 4, '=');
                        TempData["PhotoURL"] = base64Photo;
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
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/cert/auth");
                fetchRequest.Attributes.AddRequired("http://axschema.org/namePerson");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/photo");
                request.AddExtension(fetchRequest);
                request.RedirectToProvider();
            }
            return View();
        }
    }
}