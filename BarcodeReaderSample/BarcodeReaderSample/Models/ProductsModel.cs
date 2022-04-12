using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace TraceIQ.Expeditor.Models
{
    public class OrderDetailsModel : ModelObject
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

        private int _aggergationQuantity;

        public int AggregationQuantity
        {
            get => _aggergationQuantity;
            set
            {
                _aggergationQuantity = value;
                OnPropertyChanged(nameof(AggregationQuantity));
            }
        }


        private UnitOfMeasurement _unitOfMeasurement;

        public UnitOfMeasurement UnitOfMeasurement
        {
            get => _unitOfMeasurement;
            set
            {
                _unitOfMeasurement = value;
                OnPropertyChanged(nameof(UnitOfMeasurement));
            }
        }

        private string _unitOfMeasurementText;

        public string UnitOfMeasurementText
        {
            get => _unitOfMeasurementText;
            set
            {
                _unitOfMeasurementText = value;
                OnPropertyChanged(nameof(UnitOfMeasurementText));
            }
        }

        public Guid ProductId { get; set; }

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

        public List<Guid> OrderDetailIds { get; set; }
        public string PriceText { get; set; } = "Цена";
        public string AmountText { get; set; } = "Кол-во";

    }
}
