/*
 
   Copyright 2018 Christian Chicoine

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;




namespace CorpocastCommonApi
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
