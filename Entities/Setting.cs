using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Setting
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public int Port { get; set; }
        public int BillCount { get; set; }
    }
}