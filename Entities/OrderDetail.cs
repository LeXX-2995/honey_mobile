using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        public List<OrderCodeMapping> OrderCodeMappings { get; set; }
    }
}