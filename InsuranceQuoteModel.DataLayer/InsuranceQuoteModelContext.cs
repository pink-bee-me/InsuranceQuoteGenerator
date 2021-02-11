using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace InsuranceQuoteModel.DataLayer
{
    public class InsuranceQuoteModelContext : DbContext
    {
        public  DbSet<Insuree> InsuranceQuoteGenerator.Insurees { get; set; }





    }
}
