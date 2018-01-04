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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorpocastFAQApi.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace CorpocastFAQApi.Controllers
{
    [Route("api/[controller]")]
    public class BusinessEntityController : Controller
    {
        private DocumentClient CosmoDBDocumentClient;

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<string> GetAsync(string id)
        {
            

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            this.CosmoDBDocumentClient = new DocumentClient(new Uri(CorpocastFAQApi.Program.Configuration["CosmoDBEndpointUri"]), CorpocastFAQApi.Program.Configuration["CosmoDBPrimaryKey"]);

            var response= await this.CosmoDBDocumentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri("CorpocastFAQ", "CorpocastBusinessEntityCollection", id));

            BusinessEntity businessEntity = (BusinessEntity)(dynamic)response.Resource;

            return businessEntity.ToString();
        }

        // POST api/<controller>
        [HttpPost]
        public async void PostAsync([FromBody]BusinessEntity value)
        {
            await SavePost(value);            

        }

        private async Task SavePost(BusinessEntity businessEntity)
        {
            this.CosmoDBDocumentClient = new DocumentClient(new Uri(CorpocastFAQApi.Program.Configuration["CosmoDBEndpointUri"]), CorpocastFAQApi.Program.Configuration["CosmoDBPrimaryKey"]);

            if (businessEntity.Id == null || businessEntity.Id== string.Empty)
            {
                businessEntity.Id = string.Concat(businessEntity.CorpocastSubcriberNumber, businessEntity.Code);

            }
            await this.CosmoDBDocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("CorpocastFAQ", "CorpocastBusinessEntityCollection"), businessEntity);

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            int x;
            x = 1;
            x++;

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
