using System;

namespace NobelXamarin.Models
{
    public class ReportInventoryModel
    {
        public Guid Id { get; set; }
        public string ReportNumber { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; set; }
        public int TotalFactAmount { get; set; }
        public int TotalDocAmount { get; set; }
        public int TotalDifferenceAmount { get; set; }
    }
}