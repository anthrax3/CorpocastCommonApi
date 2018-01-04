using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;




namespace CorpocastFAQApi
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        private DocumentClient client;

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("azurekeys.json");

            Configuration = builder.Build();


            try
            {
                Program p = new Program();
                p.CreateDatabaseIfNotExisting().Wait();
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }





            BuildWebHost(args).Run();
        }

        private async Task CreateDatabaseIfNotExisting()
        {
            this.client = new DocumentClient(new Uri(Configuration["CosmoDBEndpointUri"]), Configuration["CosmoDBPrimaryKey"]);


            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = "CorpocastFAQ" });

            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("CorpocastFAQ"), new DocumentCollection { Id = "CorpocastFAQCollection" });

            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("CorpocastFAQ"), new DocumentCollection { Id = "CorpocastBusinessEntityCollection" });

        }

            public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
