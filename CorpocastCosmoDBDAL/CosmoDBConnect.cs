using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;


namespace CorpocastCosmoDBDAL
{
    public class CosmoDBConnect
    {
        public CosmoDBConnect()
        {
            ReadAzureConfigFile();
            CheckForStandardCorpocastCosmoDBCollection();
        }


        private void ReadAzureConfigFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("azurekeys.json");

            Configuration = builder.Build();
            this.CosmoDBEndpointUri = Configuration["CosmoDBEndpointUri"];
            this.CosmoDBPrimaryKey = Configuration["CosmoDBPrimaryKey"];
        }

        private void CheckForStandardCorpocastCosmoDBCollection()
        {
            this.CosmoDBDocumentClient = new DocumentClient(new Uri(this.CosmoDBEndpointUri), this.CosmoDBPrimaryKey);


            this.CosmoDBDocumentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = "CorpocastFAQ" });

            this.CosmoDBDocumentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("CorpocastFAQ"), new DocumentCollection { Id = "CorpocastFAQCollection" });

            this.CosmoDBDocumentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("CorpocastFAQ"), new DocumentCollection { Id = "CorpocastBusinessEntityCollection" });
        }

        public string CosmoDBEndpointUri { get; set; }

        public string CosmoDBPrimaryKey { get; set; }

        private static IConfigurationRoot Configuration { get; set; }

        protected DocumentClient CosmoDBDocumentClient;




    }
}
