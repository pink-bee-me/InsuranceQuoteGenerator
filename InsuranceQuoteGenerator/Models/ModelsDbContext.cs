using System.Data.Entity;

namespace InsuranceQuoteGenerator.Models
{
    public class ModelsDbContext

    {
        public DbSet<Insuree> Insuree { get; set; }
        public DbSet<AutoQuote> AutoQuotes { get; set; }
    }
}
