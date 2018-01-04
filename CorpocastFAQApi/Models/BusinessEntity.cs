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
        public string Id { set; get; }

        public string CorpocastSubcriberNumber { set; get; }

        public string Code { set; get; }

        public string Name { set; get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
