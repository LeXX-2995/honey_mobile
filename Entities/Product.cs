using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AmountInBox { get; set; }
        public int AmountInPallet { get; set; }
        public string Barcode { get; set; }
        public string Ikpu { get; set; }
    }
}