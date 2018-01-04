using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorpocastFAQApi.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
