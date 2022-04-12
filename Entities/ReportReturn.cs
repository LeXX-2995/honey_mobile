using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ReportReturn
    {
        public Guid Id { get; set; }
        public string ReportNumber { get; set; }
        public ReturnStatus ReturnStatus { get; set; }
        public string OrderDetails { get; set; }
    }
}
