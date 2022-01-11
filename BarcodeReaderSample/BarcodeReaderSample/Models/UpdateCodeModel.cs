﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NobelXamarin.Models
{
    public class UpdateCodeModel
    {
        public Guid ProductId { get; set; }
        public bool IsSuccess { get; set; }
        public Guid? ArrivalId { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? ShipmentId { get; set; }
        public Guid? InventoryId { get; set; }
        public Guid? WriteOffId { get; set; }
        public bool IsItemChecked { get; set; }
        public string ErrorMessage { get; set; }
        public int AssembledAmount { get; set; }
        public string ProductName { get; set; }
        public Guid ReportId { get; set; }
        public string Sscc { get; set; }
        public UnitOfMeasurements UnitOfMeasurement { get; set; }
    }
}
