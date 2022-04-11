using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public enum PaymentType
    {
        [Display(Name = "Наличными")]
        Urgent = 1,

        [Display(Name = "Перечислением")]
        Deferred = 3,

        [Display(Name = "Терминалом")]
        OnRequest = 4
    }
}