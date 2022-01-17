using System;

namespace NobelXamarin.Models
{
    public class ReportModel 
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public string ReportNumber { get; set; }
        public string InvoiceNumber { get; set; }
    }
}