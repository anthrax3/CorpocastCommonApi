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
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;


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
