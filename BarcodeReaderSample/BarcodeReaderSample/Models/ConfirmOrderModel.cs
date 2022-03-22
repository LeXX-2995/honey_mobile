using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeReaderSample.Models
{
    public class ConfirmOrderModel
    {
        public Guid OrderId { get; set; }
        public List<ConfirmProductsModel> ConfirmProductsModels { get; set; }
        public double Cash { get; set; }
        public double Terminal { get; set; }
    }

    public class ConfirmProductsModel
    {
        public Guid OrderDetailId { get; set; }
        public List<string> Codes { get; set; }
    }
}
