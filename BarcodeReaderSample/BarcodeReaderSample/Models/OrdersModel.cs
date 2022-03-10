using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIQ.Expeditor.Models
{
    public class OrdersModel : ModelObject
    {
        private string _orderName;

        public string OrderName
        {
            get => _orderName;
            set
            {
                _orderName = value;
                OnPropertyChanged(nameof(OrderName));
            }
        }

        private Guid _id;

        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

    }
}
