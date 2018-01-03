using Microsoft.EntityFrameworkCore;

namespace CorpocastFAQApi.Models
{
    public class FrequentlyAskedQuestionContext : DbContext
    {
        public FrequentlyAskedQuestionContext(DbContextOptions<FrequentlyAskedQuestionContext> options)
            : base(options)
        {
        }

        public DbSet<FrequentlyAskedQuestion> FrequentlyAskedQuestions { get; set; }

        public DbSet<BusinessEntity> BusinessEntities { get; set; }
    }
}
