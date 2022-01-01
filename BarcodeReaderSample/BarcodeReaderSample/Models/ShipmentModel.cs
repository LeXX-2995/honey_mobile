using System;
using System.Collections.Generic;

namespace NobelXamarin.Models
{
    public class ShipmentModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public ProductModel Product { get; set; }

        public int Amount { get; set; }

        public UnitOfMeasurements UnitOfMeasurement { get; set; }

        public List<string> Codes { get; set; }

        public Guid ReportShipmentId { get; set; }

        public int AssembledAmount { get; set; }
    }
}