using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Box
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(Pallet))]
        public Guid? PalletId { get; set; }
        public bool IsDisAggregated { get; set; }
        public Pallet Pallet { get; set; }
        public Product Product { get; set; }

        public List<DataMatrix> DataMatrices { get; set; }
    }
}