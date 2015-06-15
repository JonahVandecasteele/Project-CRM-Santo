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
using System.Data.SqlClient;
using System.Data;

namespace VerjaardagWebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {
        string connectionString = "Server=tcp:santo.database.windows.net,1433;Database=santo;User ID=massagesanto@santo;Password={your_password_here};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
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
            MailRepository mr = new MailRepository();
            List<Klant> jarigen = new List<Klant>();
            jarigen = mr.GetJarigen();
            foreach(Klant k in jarigen)
            {
                mr.SendMail(k);
            }           
        }

        //public IEnumerable<String> GetEmailsJarigen()
        //{
        //    using (var connection = new SqlConnection(connectionString)) 
        //    using(var cmd = connection.CreateCommand())
        //    {
        //        connection.Open();
        //        cmd.CommandText = "select email from klant";
        //        using(var reader = cmd.ExecuteReader())
        //        {
        //            while(reader.Read())
        //            {
        //                yield return reader.GetString(reader.GetOrdinal("email"));
        //            }
        //        }
        //    }
        //}
        //public void Bazaar()
        //{
        //    List<string> emails = new List<string>();
        //    SqlConnection connection = new SqlConnection(connectionString));
        //    connection.Open();
        //    SqlCommand command2 = connection.CreateCommand();
        //    command2.CommandText = ("select * from klant");
        //    command2.CommandType = CommandType.Text;
        //    SqlDataReader reader4 = command2.ExecuteReader();
        //    while (reader4.Read())
        //    {
        //        s.Add(Convert.ToInt32((reader4["turn"]).ToString()));
        //    }
        //    conn.Close();
        //}
    }
}
