using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Pallet
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDisAggregated { get; set; }
        public List<PalletDataMatrix> PalletDataMatrix { get; set; }
    }
}
