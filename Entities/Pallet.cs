using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Pallet
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDisAggregated { get; set; }
        public List<Box> Boxes { get; set; }
    }

    public class Box
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(Pallet))]
        public Guid? PalletId { get; set; }
        public bool IsDisAggregated { get; set; }
        public Pallet Pallet { get; set; }
        public Product Product { get; set; }

        public List<DataMatrix> DataMatrices { get; set; }
    }

    public class DataMatrix
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(Box))]
        public Guid? BoxId { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
        public Box Box { get; set; }
    }

    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AmountInBox { get; set; }
        public int AmountInPallet { get; set; }
        public string Barcode { get; set; }
    }

    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Inn { get; set; }

        public string LegalAddress { get; set; }

        public string ContactNumber { get; set; }

        public string ClientAddressId { get; set; }
    }

    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double? Total { get; set; }
        public double Cash { get; set; }
        public double Terminal { get; set; }
        public string QrPaymentUrl { get; set; }
        public PaymentType PaymentType { get; set; }
        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        public bool IsWaitingQr { get; set; }
        public Supplier Supplier { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        public List<OrderCodeMapping> OrderCodeMappings { get; set; }
    }

    public class CodesMapping
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Pallet))]
        public Guid? PalletId { get; set; }

        [ForeignKey(nameof(Box))]
        public Guid? BoxId { get; set; }

        [ForeignKey(nameof(DataMatrix))]
        public Guid? DataMatrixCodeId { get; set; }

        public Box Box { get; set; }
        public Pallet Pallet { get; set; }
        public DataMatrix DataMatrix { get; set; }
    }

    public class OrderCodeMapping
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Pallet))]
        public Guid? PalletId { get; set; }

        [ForeignKey(nameof(Box))]
        public Guid? BoxId { get; set; }

        [ForeignKey(nameof(DataMatrix))]
        public Guid? DataMatrixCodeId { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderDetail))]
        public Guid OrderDetailId { get; set; }
        public Box Box { get; set; }
        public Pallet Pallet { get; set; }
        public DataMatrix DataMatrix { get; set; }

        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }

    public class Setting
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public int Port { get; set; }
    }

    public enum PaymentType
    {
        [Display(Name = "Наличными")]
        Urgent = 1,

        [Display(Name = "Перечислением")]
        Deferred = 3,

        [Display(Name = "Терминалом")]
        OnRequest = 4
    }

    public enum UnitOfMeasurement
    {
        [Display(Name = "Штука")]
        Item = 1,
        [Display(Name = "Блок")]
        Box,
        [Display(Name = "Паллета")]
        Pallet,
    }

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
        Transfered,
    }
}
