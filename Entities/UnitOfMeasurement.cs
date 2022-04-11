using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public enum UnitOfMeasurement
    {
        [Display(Name = "Штука")]
        Item = 1,
        [Display(Name = "Блок")]
        Box,
        [Display(Name = "Паллета")]
        Pallet,
    }
}