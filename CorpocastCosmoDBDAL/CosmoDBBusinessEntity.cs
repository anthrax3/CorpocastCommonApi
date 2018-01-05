using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using CorpocastCommonModels.Models;
using System.Threading.Tasks;

using System.Configuration;

using System.Linq;

using System.Net;





namespace CorpocastCosmoDBDAL
{
    public class CosmoDBBusinessEntity : CosmoDBConnect
    {
        public async Task<string> CreateAsync(BusinessEntity businessEntity)
        {
            
            
            if (businessEntity.Id == null || businessEntity.Id== string.Empty)
            {
                businessEntity.Id = string.Concat(businessEntity.CorpocastSubcriberNumber, businessEntity.Code);

            }

            await this.CosmoDBDocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("CorpocastFAQ", "CorpocastBusinessEntityCollection"), businessEntity);
            

            return "true";
        }

        public async Task<BusinessEntity> GetOneAsync(string id)
        {

            var response = await this.CosmoDBDocumentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri("CorpocastFAQ", "CorpocastBusinessEntityCollection", id));

            BusinessEntity businessEntity = (BusinessEntity)(dynamic)response.Resource;

            return businessEntity;
        }                
    }
}
