using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReaderSample.Models
{
    public class OrderDetailsBillModel 
    {
        public Guid OrderDetailId { get; set; }
        public string Name { get; set; }

        public string UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        public string Ikpu { get; set; }

        public string Barcode { get; set; }

        public string Vat { get; set; }

        public double TotalVat { get; set; }
        public List<string> Codes { get; set; }

        public int AmountInItems { get; set; }
    }
}
