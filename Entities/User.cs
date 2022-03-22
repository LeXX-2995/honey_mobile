using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid TransportId { get; set; }
        public RoleType RoleType { get; set; }
    }

    public enum RoleType
    {
        Administrator,

        Accountant,

        Picker,

        Expeditor
    }
}
