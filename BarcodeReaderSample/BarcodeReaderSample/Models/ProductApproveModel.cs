using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace BarcodeReaderSample.Models
{
    public class ProductApproveModel
    {
        public Guid OrderId { get; set; }
        public Guid OrderDetailId { get; set; }
        public Guid ProductId { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public bool IsChecked { get; set; }
        public int AmountInBox { get; set; }
        public int AmountInPallet { get; set; }
        public Guid ItemId { get; set; }
        public Product Product { get; set; }
    }
}
