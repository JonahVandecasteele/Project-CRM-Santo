using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mailinblue;
using CRMSanto.Models;

namespace CRMSanto.BusinessLayer.Mailing
{
    public class SendinBlue
    {
        //public static void Main(string[] args)
        //{
        //    API test = new mailinblue.API("r0GZv13CEFbk8yVq");
        //    Console.Write(test.get_processes(1, 2));
        //    Console.ReadKey(true);
        //}

        public static void Main()
        {
            Klant klant = new Klant();
            API sendinBlue = new mailinblue.API("r0GZv13CEFbk8yVq");
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("NAME", "name");
            attributes.Add("SURNAME", "surname");
            attributes.Add("DATE ADDED", "1994-04-14");
            List<int> listid = new List<int>();
            listid.Add(1);
            listid.Add(4);
            listid.Add(4);
            List<int> listid_unlink = new List<int>();
            listid_unlink.Add(2);
            listid_unlink.Add(5);
            Object createUpdatetUser = sendinBlue.create_update_user("example@example.net", attributes, 0, listid, listid_unlink, 0);
            Console.WriteLine(createUpdatetUser);
        }
    }
}
