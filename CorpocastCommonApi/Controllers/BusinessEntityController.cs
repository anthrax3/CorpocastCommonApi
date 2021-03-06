﻿/*
 
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

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CorpocastCosmoDBDAL;
using CorpocastCommonModels.Models;

namespace CorpocastCommonApi.Controllers
{
    [Route("api/[controller]")]
    public class BusinessEntityController : Controller
    {
        
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            CosmoDBBusinessEntity cosmoDBBusinessEntity=new CosmoDBBusinessEntity();

            BusinessEntity businessEntity = cosmoDBBusinessEntity.GetOneAsync(id).Result;
            
            return businessEntity.ToString();

        }

        // POST api/<controller>
        [HttpPost]
        public async void PostAsync([FromBody]BusinessEntity value)
        {
            CosmoDBBusinessEntity cosmoDBBusinessEntity = new CosmoDBBusinessEntity();

            await cosmoDBBusinessEntity.CreateAsync(value);            

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
