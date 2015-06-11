using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static string ActivePage(this HtmlHelper helper, string controller, string action)
        {
            string classValue = "";

            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (currentController == controller && currentAction == action)
            {
                classValue = "active";
            }

            return classValue;
        }

        public static string GetPhotoURL(this HtmlHelper helper, string photo)
        {
            string photoURL = "/Content/img/profile.jpg";
            if (photo != "") photoURL = "http://massagesanto.blob.core.windows.net/images/" + photo;
            return photoURL;
        }

        public static string FormatPhoneNumber(this HtmlHelper helper, string phoneNumber) 
        {
            string formattedNumber = phoneNumber;
            //GSM
            if (phoneNumber.Length == 10)
            {
                formattedNumber = phoneNumber.Substring(0, 4) + " " + phoneNumber.Substring(4, 2) + " " + phoneNumber.Substring(6, 2) + " " + phoneNumber.Substring(8, 2);
            }
            //THUIS
            if (phoneNumber.Length == 9) 
            {
                formattedNumber = phoneNumber.Substring(0,3) + " " + phoneNumber.Substring(3,2) + " " + phoneNumber.Substring(5,2) + " " + phoneNumber.Substring(7,2);
            }
            return formattedNumber;
        }
    }
}