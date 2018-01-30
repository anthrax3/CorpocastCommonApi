using System;
using System.Collections.Generic;
using System.Text;

namespace CorpocastCommonModels.Models
{
    public class Holding : LegalEntity
    {
        private void CreateIListInstanceForClass()
        {
            this.Subsidiaries = new List<Subsidiary>();
        }

        public Holding()
        {
            CreateIListInstanceForClass();
        }

        public IList<Subsidiary> Subsidiaries { get; set; }



    }
}
