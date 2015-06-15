using CRMSanto.Models;
using mailinblue;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.BusinessLayer.Repository
{
    public class MailRepository
    {
        public List<Klant> GetJarigen()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                DateTime dt = DateTime.Now.AddDays(7);
                var query = (from k in context.Klant
                             where ((SqlFunctions.DatePart("DAY", k.Geboortedatum) == SqlFunctions.DatePart("DAY", dt)) && (SqlFunctions.DatePart("MONTH", k.Geboortedatum) == SqlFunctions.DatePart("MONTH", dt)))
                             select k);
                return query.ToList<Klant>();
            }
        }
        public void SendMail(Klant k)
        {
            {
                API sendinBlue = new mailinblue.API("r0GZv13CEFbk8yVq");
                Dictionary<string, string> to = new Dictionary<string, string>();
                to.Add(k.Email, k.Voornaam);
                List<string> from_name = new List<string>();
                from_name.Add("massage.santo@gmail.com");
                from_name.Add("Massage Santo");
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Content-Type", "text/html; charset=iso-8859-1");
                headers.Add("X-param1", "value1");
                headers.Add("X-param2", "value2");
                headers.Add("X-Mailin-custom", "my custom value");
                headers.Add("X-Mailin-IP", "102.102.1.2");
                headers.Add("X-Mailin-Tag", "My tag");
                Object sendEmail = sendinBlue.send_email(to, "Gelukkige verjaardag", from_name, "Gelukkige verjaardag", "This is the text", new Dictionary<string, string>(), headers);
            }
        }
    }
}
