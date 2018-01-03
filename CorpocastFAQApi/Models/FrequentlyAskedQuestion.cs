using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorpocastFAQApi.Models
{
    public class FrequentlyAskedQuestion
    {
        public BusinessEntity ParentBusinessEntity{get; set;}

        public long Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }


    }
}
