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

using Microsoft.Azure.Documents.Client;
using CorpocastCommonModels.Models;
using System.Threading.Tasks;
using System;

namespace CorpocastCosmoDBDAL
{
    public class CosmoDBBusinessEntity : CosmoDBConnect
    {
        public async Task<string> CreateAsync(BusinessEntity businessEntity)
        {

            try
            {
                if (businessEntity.Id == null || businessEntity.Id == string.Empty)
                {
                    businessEntity.Id = string.Concat(businessEntity.CorpocastSubcriberNumber, businessEntity.Code);

                }

                await this.CosmoDBDocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("CorpocastFAQ", "CorpocastBusinessEntityCollection"), businessEntity);

                return "true";

            }
            catch(Exception e)
            {
                throw e;
            }
            

        }

        public async Task<BusinessEntity> GetOneAsync(string id)
        {

            var response = await this.CosmoDBDocumentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri("CorpocastFAQ", "CorpocastBusinessEntityCollection", id));

            BusinessEntity businessEntity = (BusinessEntity)(dynamic)response.Resource;

            return businessEntity;
        }                
    }
}
