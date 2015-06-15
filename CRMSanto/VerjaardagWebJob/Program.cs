using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using CRMSanto.Models;
using CRMSanto.BusinessLayer.Repository;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;

namespace VerjaardagWebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            Jarigen();
            var host = new JobHost();
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }

        public static void Jarigen()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            KlantenRepository kr = new KlantenRepository(context);
            List<Klant> jarigen = kr.GetJarigen();
            foreach(Klant k in jarigen)
            {
                kr.SendMail(k);
            }
        }
    }
}
