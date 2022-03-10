using System;
using System.Collections.Generic;
using System.Text;

namespace TraceIQ.Expeditor.Models
{
    public class ProductsModel : ModelObject
    {
        private string _productName;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private double _price;

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private int _amount;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private int _assembledAmount;

        public int AssembledAmount
        {
            get => _assembledAmount;
            set
            {
                _assembledAmount = value;
                OnPropertyChanged(nameof(AssembledAmount));
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

        public string PriceText { get; set; } = "Цена";
        public string AmountText { get; set; } = "Кол-во";

    }
}
