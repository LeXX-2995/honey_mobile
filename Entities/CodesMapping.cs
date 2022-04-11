using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class CodesMapping
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Pallet))]
        public Guid? PalletId { get; set; }

        [ForeignKey(nameof(Box))]
        public Guid? BoxId { get; set; }

        [ForeignKey(nameof(DataMatrix))]
        public Guid? DataMatrixCodeId { get; set; }

        public Box Box { get; set; }
        public Pallet Pallet { get; set; }
        public DataMatrix DataMatrix { get; set; }
    }
}