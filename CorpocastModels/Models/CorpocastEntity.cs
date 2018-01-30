using System;
using System.Collections.Generic;
using System.Text;

namespace CorpocastCommonModels.Models
{
    public class CorpocastEntity
    {

        private void CreateIListInstanceForClass()
        {
            this.FrequentlyAskedQuestions = new List<FrequentlyAskedQuestion>();
            this.News = new List<NewsPost>();

        }

        public CorpocastEntity()
        {
            CreateIListInstanceForClass();
        }

        public long Id { get; set; }

        public CorpocastEntity ParentCorpocastEntity { get; set; }

        public IList<FrequentlyAskedQuestion> FrequentlyAskedQuestions { get; set; }

        public IList<NewsPost> News { get; set; }

    }
}
