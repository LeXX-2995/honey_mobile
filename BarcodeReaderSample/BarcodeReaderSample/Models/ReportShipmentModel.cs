using System;

namespace NobelXamarin.Models
{
    public class ReportShipmentModel 
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string ReportNumber { get; set; }
    }
}