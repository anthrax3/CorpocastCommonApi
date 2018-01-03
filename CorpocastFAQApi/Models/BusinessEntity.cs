using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CorpocastFAQApi.Models
{
    public class BusinessEntity
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { set; get; }

        public string Name { set; get; }
    }
}
