//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsuranceQuoteGenerator.Models
{
    public partial class AutoQuote
    {
        public int AutoQuoteId { get; set; }
        public Insuree InsureeId { get; set; }
        public decimal BaseRate { get; set; }
        public decimal AgeUnder18 { get; set; }
        public decimal AgeBtw19and25 { get; set; }
        public decimal Age26andUp { get; set; }
        public decimal AutoYearBefore2000 { get; set; }
        public decimal AutoYearAfter2015 { get; set; }
        public decimal IsPorsche { get; set; }
        public decimal IsCarrera { get; set; }
        public decimal SubTotalBeforeDuiCalc { get; set; }
        public decimal DuiRateUp25Percent { get; set; }
        public decimal SubTotalAfterDuiCalc { get; set; }
        public decimal SpeedingTickets { get; set; }
        public decimal SubTotalBeforeCoverageCalc { get; set; }
        public decimal FullCoverageRateUp50Percent { get; set; }
        public decimal SubTotalAfterCoverageCalc { get; set; }
        public decimal MonthlyQuoteRate { get; set; }
        public decimal YearlyQuoteRate { get; set; }


        public AutoQuote()
        {

        }

        public AutoQuote(Insuree insuree)
        {

        }

        public AutoQuote(NewInsureeFormData newInsureeFormData)
        {

        }
    }
}
