using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class DataMatrix
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(Box))]
        public Guid? BoxId { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
        public Box Box { get; set; }
    }
}