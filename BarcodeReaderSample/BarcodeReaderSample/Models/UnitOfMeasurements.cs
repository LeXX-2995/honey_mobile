using System.ComponentModel.DataAnnotations;

namespace TraceIQ.Expeditor.Models
{
    public enum UnitOfMeasurements
    {
        [Display(Name = "Коробка")]
        Box = 1,

        [Display(Name = "Паллета")]
        Pallete,

        [Display(Name = "Штука")]
        Item
    }
}