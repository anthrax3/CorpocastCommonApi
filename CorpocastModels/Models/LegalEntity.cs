using System;
using System.Collections.Generic;
using System.Text;

namespace CorpocastCommonModels.Models
{
    public class LegalEntity : CorpocastEntity
    {

        private void CreateIListInstanceForClass()
        {
            this.Premises = new List<Premise>();
        }

        public LegalEntity()
        {
            CreateIListInstanceForClass();
        }

        public string Code { get; set; }
        
        public string Name { get; set; }

        public IList<Premise> Premises { get; set; }

    }
}
