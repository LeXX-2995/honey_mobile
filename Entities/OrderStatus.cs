using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public enum OrderStatus
    {
        [Display(Name = "Принят")]
        Accepted = 1,

        [Display(Name = "Создан")]
        Entered,

        [Display(Name = "Отгружен")]
        Dispatched,

        [Display(Name = "В пути")]
        Transit,

        [Display(Name = "Отвергнут")]
        Rejected,

        [Display(Name = "Отвергнут")]
        Transfered
    }
}