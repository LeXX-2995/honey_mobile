using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BarcodeReaderSample.Models
{
    public enum SyncDataTypes
    {
        [Display(Name = "Товары")]
        Product,

        [Display(Name = "Клиенты")]
        Suppliers,

        [Display(Name = "Заказы")]
        Orders,

        [Display(Name = "Детали заказов")]
        OrderDetails,

        [Display(Name = "Коды")]
        CodesMapping
    }

    public enum SyncStatus
    {
        [Display(Name = "В ожидании")]
        Waiting,

        [Display(Name = "Принимается")]
        Receiving,

        [Display(Name = "Завершено")]
        Completed,

        [Display(Name = "Ошибка")]
        Error,

        [Display(Name = "Данные пустые")]
        NoData
    }
}
