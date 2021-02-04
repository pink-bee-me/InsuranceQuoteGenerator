namespace InsuranceQuoteGenerator.ViewModels
{
    public class AdminEmailListVM
    {
        public System.DateTime QuoteDate { get; set; }
        public int InsureeId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime LastEmailContactDate { get; set; }
        public decimal MonthlyQuoteRate { get; set; }
        public decimal YearlyQuoteRate { get; set; }
        public bool BecamePolicyHolder { get; set; }
    }
}