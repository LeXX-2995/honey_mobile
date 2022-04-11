using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using TraceIQ.Expeditor.Models;

namespace BarcodeReaderSample.Models
{
    public class ReportReturnModel
    {
        public Guid TransportId { get; set; }
        public List<OrderDetailsModel> OrderDetailsModels { get; set; }
    }
}
