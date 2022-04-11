using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double? Total { get; set; }
        public double Cash { get; set; }
        public double Terminal { get; set; }
        public string QrPaymentUrl { get; set; }
        public PaymentType PaymentType { get; set; }
        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }
        public string FiscalBoxData { get; set; }
        public bool IsWaitingFiscalBox { get; set; }
        public Supplier Supplier { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}