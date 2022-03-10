using System;

namespace TraceIQ.Expeditor.Models
{
    public class UserModel
    {
        public  Guid Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public RoleType RoleType { get; set; }

        public string Password { get; set; }

        public Guid WarehouseId { get; set; }

        public  DateTime CreatedDate { get; set; }

        public  bool IsActive { get; set; }
    }

    public enum RoleType
    {
        Administrator,

        Accountant,

        Picker,

        Expeditor
    }
}
