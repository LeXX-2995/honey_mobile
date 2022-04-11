using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Inn { get; set; }

        public string LegalAddress { get; set; }

        public string ContactNumber { get; set; }

        public string ClientAddressId { get; set; }
    }
}