using System;
using System.Collections.Generic;

namespace NobelXamarin.Models
{
    public class ProductsModel
    {
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public int AssembledAmount { get; set; }
        public UnitOfMeasurements UnitOfMeasurement { get; set; }
        public Guid ReportId { get; set; }
        public Guid ProductId { get; set; }
        public List<string> Codes { get; set; }

        public string ItemToDisplayUpper => $"{ProductName.Substring(0, 30)}...";
        public string ItemToDisplayBelow => $"Кол-во - {Amount} || Собрано - {AssembledAmount}";

    }
}