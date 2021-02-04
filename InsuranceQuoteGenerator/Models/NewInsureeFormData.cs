﻿namespace InsuranceQuoteGenerator.Models
{
    public class NewInsureeFormData
    {
        public int InsureeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public int CarYear { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public bool DUI { get; set; }
        public int SpeedingTickets { get; set; }
        public bool CoverageType { get; set; }
        public AutoQuote AutoQuoteId { get; set; }
        public AutoQuote MonthlyQuoteRate { get; set; }
        public AutoQuote YearlyQuoteRate { get; set; }
    }
}