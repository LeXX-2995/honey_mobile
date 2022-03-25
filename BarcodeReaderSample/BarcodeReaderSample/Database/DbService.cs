using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content.Res;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Models;
using Entities;
using Javax.Xml.Xpath;
using Microsoft.EntityFrameworkCore;
using NobelXamarin.Helpers;
using TraceIQ.Expeditor.Models;

namespace BarcodeReaderSample.Database
{
    public class DbService : IDbService
    {
        public OperationResult<Setting> GetSetting()
        {
            return DigitalTrackingContext.Run(db =>
            {
                var setting = db.Setting.AsNoTracking().FirstOrDefault() ?? new Setting();

                return new OperationResult<Setting>
                {
                    Result = OperationStatus.Success,
                    Value = setting
                };
            });
        }

        public OperationResult AddSetting(string url, int port)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var setting = db.Setting.FirstOrDefault();

                if (setting == null)
                {
                    db.Setting.Add(new Setting
                    {
                        Id = Guid.NewGuid(),
                        Port = port,
                        Url = url
                    });
                }
                else
                {
                    setting.Url = url;
                    setting.Port = port;
                }

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success,
                };
            });
        }

        public OperationResult<User> Login(string login, string password)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var user = db.Users
                    .FirstOrDefault(s => s.Login == login);

                if (user == null)
                    return OperationResult<User>.Fail(string.Empty);

                if (!BCrypt.CheckPassword(password, user.Password))
                    return OperationResult<User>.Fail("Логин или пароль неправильный");

                return new OperationResult<User>
                {
                    Result = OperationStatus.Success,
                    Value = user
                };
            });
        }

        public OperationResult AddUser(User user)
        {
            return DigitalTrackingContext.Run(db =>
            {
                db.Users.Add(user);

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult AddProducts(List<Product> products)
        {
            return DigitalTrackingContext.Run(db =>
            {
                if(!products.Any())
                    return new OperationResult
                    {
                        Result = OperationStatus.Success
                    };

                var existingProducts = db
                    .Products
                    .Where(s => products.Select(t => t.Id).Contains(s.Id))
                    .ToList();

                foreach (var product in products)
                {
                    var existingProduct = existingProducts.FirstOrDefault(s => s.Id == product.Id);
                    if (existingProduct != null)
                    {
                        existingProduct.AmountInBox = product.AmountInBox;
                        existingProduct.AmountInPallet = product.AmountInPallet;
                        existingProduct.Barcode = product.Barcode;
                        existingProduct.Name = product.Name;
                        existingProduct.Ikpu = product.Ikpu;
                    }
                    else
                        db.Products.Add(product);
                }

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult AddSuppliers(List<Supplier> suppliers)
        {
            return DigitalTrackingContext.Run(db =>
            {
                if(!suppliers.Any())
                    return new OperationResult
                    {
                        Result = OperationStatus.Success
                    };

                var existingSuppliers = db.Suppliers
                    .Where(s => suppliers.Select(t => t.Id).Contains(s.Id))
                    .ToList();

                foreach (var supplier in suppliers)
                {
                    var existingSupplier = existingSuppliers.FirstOrDefault(s => s.Id == supplier.Id);
                    if (existingSupplier != null)
                    {
                        existingSupplier.ClientAddressId = supplier.ClientAddressId;
                        existingSupplier.ContactNumber = supplier.ContactNumber;
                        existingSupplier.Inn = supplier.Inn;
                        existingSupplier.LegalAddress = supplier.LegalAddress;
                        existingSupplier.Name = supplier.Name;
                    }
                    else
                        db.Suppliers.Add(supplier);
                }

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult AddOrders(List<Order> orders)
        {
            return DigitalTrackingContext.Run(db =>
            {
                if(!orders.Any())
                    return new OperationResult
                    {
                        Result = OperationStatus.Success
                    };

                var existingOrders = db.Orders
                    .Where(s => orders.Select(t => t.Id).Contains(s.Id))
                    .ToList();

                foreach (var order in orders)
                {
                    var existingOrder = existingOrders.FirstOrDefault(s => s.Id == order.Id);
                    if (existingOrder != null)
                    {
                        existingOrder.Cash = order.Cash;
                        existingOrder.OrderDate = order.OrderDate;
                        existingOrder.OrderNumber = order.OrderNumber;
                        existingOrder.OrderStatus = order.OrderStatus;
                        existingOrder.PaymentType = order.PaymentType;
                        existingOrder.QrPaymentUrl = order.QrPaymentUrl;
                        existingOrder.SupplierId = order.SupplierId;
                        existingOrder.Terminal = order.Terminal;
                        existingOrder.Total = order.Total;
                    }
                    else
                        db.Orders.Add(order);
                }

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult AddOrderDetails(List<OrderDetail> orderDetails)
        {
            return DigitalTrackingContext.Run(db =>
            {
                if (!orderDetails.Any())
                    return new OperationResult
                    {
                        Result = OperationStatus.Success
                    };

                var existingOrders = db.OrderDetails
                    .Where(s => orderDetails.Select(t => t.Id).Contains(s.Id))
                    .ToList();

                foreach (var orderDetail in orderDetails)
                {
                    var existingOrder = existingOrders.FirstOrDefault(s => s.Id == orderDetail.Id);
                    if (existingOrder != null)
                    {
                        existingOrder.OrderId = orderDetail.OrderId;
                        existingOrder.Price = orderDetail.Price;
                        existingOrder.Quantity = orderDetail.Quantity;
                        existingOrder.Total = orderDetail.Total;
                        existingOrder.UnitOfMeasurement = orderDetail.UnitOfMeasurement;
                    }
                    else
                        db.OrderDetails.Add(orderDetail);
                }

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult AddCodeMappings(List<CodesMapping> codesMappings)
        {
            return DigitalTrackingContext.Run(db =>
            {
                if(!codesMappings.Any())
                    return new OperationResult
                    {
                        Result = OperationStatus.Success
                    };

                var existingMappings = db.CodesMappings
                    .Include(s => s.DataMatrix)
                    .Include(s => s.Box)
                        .ThenInclude(s => s.DataMatrices)
                    .Include(s => s.Pallet)
                        .ThenInclude(s => s.Boxes)
                            .ThenInclude(s => s.DataMatrices)
                    .Where(s => codesMappings.Select(t => t.Id).Contains(s.Id))
                    .ToList();

                foreach (var codeMapping in codesMappings)
                {
                    var existingCodeMapping = existingMappings.FirstOrDefault(s => s.Id == codeMapping.Id);
                    if (existingCodeMapping == null)
                    {
                        db.CodesMappings.Add(codeMapping);
                    }
                }

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult<List<ClientsModel>> GetClients()
        {
            return DigitalTrackingContext.Run(db =>
            {
                var clients = db.Orders
                    .AsNoTracking()
                    .Where(s => s.OrderStatus == OrderStatus.Transit)
                    .Include(s => s.Supplier)
                    .GroupBy(s => new {s.SupplierId, s.Supplier.Name})
                    .Select(s => new ClientsModel
                    {
                        Id = s.Key.SupplierId,
                        Name = s.Key.Name
                    })
                    .ToList();

                return new OperationResult<List<ClientsModel>>
                {
                    Result = OperationStatus.Success,
                    Value = clients
                };
            });
        }

        public OperationResult<List<OrdersModel>> GetOrders(Guid supplierId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var orders = db.Orders
                    .AsNoTracking()
                    .Where(s => s.SupplierId == supplierId && s.OrderStatus == OrderStatus.Transit)
                    .Select(s => new OrdersModel
                    {
                        Id = s.Id,
                        OrderName = s.OrderNumber
                    })
                    .ToList();


                return new OperationResult<List<OrdersModel>>
                {
                    Result = OperationStatus.Success,
                    Value = orders
                };
            });
        }

        public OperationResult<List<OrderDetailsModel>> GetOrderDetails(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var orderDetails = db.OrderDetails
                    .AsNoTracking()
                    .Include(s => s.Product)
                    .Include(s => s.OrderCodeMappings)
                    .Where(s => s.OrderId == orderId)
                    .Select(s => new OrderDetailsModel
                    {
                        Id = s.Id,
                        Price = s.Price,
                        UnitOfMeasurement = s.UnitOfMeasurement,
                        Amount = s.Quantity,
                        AssembledAmount = s.OrderCodeMappings != null ? s.OrderCodeMappings.Count : default,
                        ProductName = s.Product.Name
                    })
                    .ToList();

                return new OperationResult<List<OrderDetailsModel>>
                {
                    Result = OperationStatus.Success,
                    Value = orderDetails
                };
            });
        }

        public OperationResult<ProductApproveModel> GetOrdersByCode(string code, Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var dataMatrix = db.DataMatrix
                    .AsNoTracking()
                    .Include(s => s.Product)
                    .FirstOrDefault(s => s.Code.Trim() == code.Trim() || s.Code == RemoveFirstTwoCharacters(code));

                var box = db.Boxes
                    .AsNoTracking()
                    .Include(s => s.Product)
                    .FirstOrDefault(s => s.Code.Trim() == code.Trim() || s.Code == RemoveFirstTwoCharacters(code));

                var pallet = db.Pallets
                    .AsNoTracking()
                    .Include(s => s.Product)
                    .FirstOrDefault(s => s.Code == code);

                Guid? productId = default;
                Product product = default;
                UnitOfMeasurement? unitOfMeasurement = default;
                Guid? itemId = default;
                Guid? orderCodeMappingId = default;
                var isDisAggregated = false;
                if (dataMatrix != null)
                {
                    productId = dataMatrix.ProductId;
                    unitOfMeasurement = UnitOfMeasurement.Item;
                    product = dataMatrix.Product;
                    itemId = dataMatrix.Id;
                }
                else if (box != null)
                {
                    productId = box.ProductId;
                    unitOfMeasurement = UnitOfMeasurement.Box;
                    product = box.Product;
                    itemId = box.Id;
                }
                else if (pallet != null)
                {
                    productId = pallet.ProductId;
                    unitOfMeasurement = UnitOfMeasurement.Pallet;
                    product = pallet.Product;
                    itemId = pallet.Id;
                }

                if(!productId.HasValue)
                    return OperationResult<ProductApproveModel>.Fail("Товар не найден");

                var orderDetail = db.OrderDetails
                    .AsNoTracking()
                    .FirstOrDefault(s => s.ProductId == productId.Value && s.OrderId == orderId && s.UnitOfMeasurement == unitOfMeasurement.Value);

                if(orderDetail == null)
                    return OperationResult<ProductApproveModel>.Fail($"В заказе товар '{product.Name}' с единицей измерения '{unitOfMeasurement.Value.GetDisplayName()}' не найден");

                var isChecked = unitOfMeasurement.Value switch
                {
                    UnitOfMeasurement.Item => db.OrderCodeMappings.Any(s => s.DataMatrixCodeId == itemId && s.OrderId == orderId),
                    UnitOfMeasurement.Box => db.OrderCodeMappings.Any(s => s.BoxId == itemId && s.OrderId == orderId),
                    UnitOfMeasurement.Pallet => db.OrderCodeMappings.Any(s => s.PalletId == itemId && s.OrderId == orderId),
                    _ => throw new ArgumentOutOfRangeException()
                };

                return new OperationResult<ProductApproveModel>
                {
                    Result = OperationStatus.Success,
                    Value = new ProductApproveModel
                    {
                        UnitOfMeasurement = unitOfMeasurement.Value,
                        ProductId = productId.Value,
                        AmountInBox = product.AmountInBox,
                        AmountInPallet = product.AmountInPallet,
                        ItemId = itemId.Value,
                        OrderId = orderId,
                        OrderDetailId = orderDetail.Id,
                        ProductName = product.Name,
                        IsChecked = isChecked,
                        Code = code,
                        Product = product
                    }
                };
            });
        }

        public OperationResult ApproveOrderDetail(ProductApproveModel model)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var orderDetails = db.OrderDetails
                    .Include(s => s.OrderCodeMappings)
                    .AsNoTracking()
                    .Where(s => s.OrderId == model.OrderId && s.ProductId == model.ProductId && s.UnitOfMeasurement == model.UnitOfMeasurement)
                    .ToList();

                if(!orderDetails.Any())
                    return OperationResult.Fail("Позиция заказа не найдена");

                OrderCodeMapping orderCodeMapping;

                if (model.IsChecked)
                {
                    switch (model.UnitOfMeasurement)
                    {
                        case UnitOfMeasurement.Item:
                            orderCodeMapping =
                                db.OrderCodeMappings.SingleOrDefault(s => s.DataMatrixCodeId == model.ItemId && s.OrderId == model.OrderId);

                            if (orderCodeMapping == null)
                                return OperationResult.Fail("Товар не связан с заказом");

                            break;
                        case UnitOfMeasurement.Box:
                            orderCodeMapping =
                                db.OrderCodeMappings.SingleOrDefault(s => s.BoxId == model.ItemId && s.OrderId == model.OrderId);

                            if (orderCodeMapping == null)
                                return OperationResult.Fail("Товар не связан с заказом");

                            break;
                        case UnitOfMeasurement.Pallet:
                            orderCodeMapping =
                                db.OrderCodeMappings.SingleOrDefault(s => s.PalletId == model.ItemId && s.OrderId == model.OrderId);

                            if (orderCodeMapping == null)
                                return OperationResult.Fail("Товар не связан с заказом");

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    db.OrderCodeMappings.Remove(orderCodeMapping);
                }
                else
                {
                    if (orderDetails.Sum(s => s.Quantity) == orderDetails.Sum(s => s.OrderCodeMappings.Count))
                        return OperationResult.Fail("Позиции по данному товару полностью уже собраны");

                    switch (model.UnitOfMeasurement)
                    {
                        case UnitOfMeasurement.Item:
                            orderCodeMapping =
                                db.OrderCodeMappings.SingleOrDefault(s => s.DataMatrixCodeId == model.ItemId);

                            if (orderCodeMapping != null)
                                return OperationResult.Fail("Товар уже связан с заказом");

                            db.OrderCodeMappings.Add(new OrderCodeMapping
                            {
                                Id = Guid.NewGuid(),
                                DataMatrixCodeId = model.ItemId,
                                OrderId = model.OrderId,
                                OrderDetailId = model.OrderDetailId,
                            });
                            break;
                        case UnitOfMeasurement.Box:
                            orderCodeMapping =
                                db.OrderCodeMappings.SingleOrDefault(s => s.BoxId == model.ItemId);

                            if (orderCodeMapping != null)
                                return OperationResult.Fail("Товар уже связан с заказом");

                            db.OrderCodeMappings.Add(new OrderCodeMapping
                            {
                                Id = Guid.NewGuid(),
                                BoxId = model.ItemId,
                                OrderId = model.OrderId,
                                OrderDetailId = model.OrderDetailId,
                            });

                            break;
                        case UnitOfMeasurement.Pallet:
                            orderCodeMapping =
                                db.OrderCodeMappings.SingleOrDefault(s => s.PalletId == model.ItemId);

                            if (orderCodeMapping != null)
                                return OperationResult.Fail("Товар уже связан с заказом");

                            db.OrderCodeMappings.Add(new OrderCodeMapping
                            {
                                Id = Guid.NewGuid(),
                                PalletId = model.ItemId,
                                OrderId = model.OrderId,
                                OrderDetailId = model.OrderDetailId,
                            });

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success,
                };
            });
        }

        public OperationResult<List<string>> GetOrderDataMatrix(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var orders = db.OrderDetails
                    .AsNoTracking()
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.Box)
                        .ThenInclude(s => s.DataMatrices)
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.DataMatrix)
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.Pallet)
                        .ThenInclude(s => s.Boxes)
                        .ThenInclude(s => s.DataMatrices)
                    .Where(s => s.OrderId == orderId)
                    .SelectMany(s => s.OrderCodeMappings)
                    .ToList();

                var codes = new List<string>();

                if(orders.Any(s => s.DataMatrixCodeId.HasValue && s.DataMatrix != null))
                    codes.AddRange(orders.Where(s => s.DataMatrixCodeId.HasValue && s.DataMatrix != null).Select(s => s.DataMatrix.Code).ToList());

                if(orders.Any(s => s.BoxId.HasValue && s.Box != null && s.Box.DataMatrices != null && s.Box.DataMatrices.Any()))
                    codes.AddRange(orders.Where(s => s.BoxId.HasValue && s.Box != null && s.Box.DataMatrices != null && s.Box.DataMatrices.Any())
                        .SelectMany(s => s.Box.DataMatrices.Select(t => t.Code)).ToList());

                foreach (var order in orders.Where(s => s.PalletId.HasValue))
                {
                    if (order.Pallet?.Boxes == null || !order.Pallet.Boxes.Any()) 
                        continue;

                    if (order.Pallet.Boxes.Any(s => s.DataMatrices != null && s.DataMatrices.Any()))
                    {
                        codes.AddRange(order.Pallet.Boxes.SelectMany(s => s.DataMatrices.Select(t => t.Code).ToList()));
                    }
                }

                return new OperationResult<List<string>>
                {
                    Result = OperationStatus.Success,
                    Value = codes
                };
            });
        }

        public OperationResult UpdateOrderWaiting(Guid orderId, double cash, double terminal)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var order = db.Orders.FirstOrDefault(s => s.Id == orderId);
                if(order == null)
                    return OperationResult.Fail("Заказа не найден");

                order.IsWaitingFiscalBox = true;
                order.Cash = cash;
                order.Terminal = terminal;

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult<bool> GetOrderWaitingStatus(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var order = db.Orders.AsNoTracking().FirstOrDefault(s => s.Id == orderId);
                if(order == null)
                    return OperationResult<bool>.Fail("Запись не найдена");

                return new OperationResult<bool>
                {
                    Result = OperationStatus.Success,
                    Value = order.IsWaitingFiscalBox
                };
            });
        }

        public OperationResult<int> GetOrderTotal(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var order = db.Orders.AsNoTracking().FirstOrDefault(s => s.Id == orderId);
                if (order == null)
                    return OperationResult<int>.Fail("Запись не найдена");

                return new OperationResult<int>
                {
                    Result = OperationStatus.Success,
                    Value = (int)order.Total.Value
                };
            });
        }

        public OperationResult<Order> GetOrder(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var order = db.Orders.FirstOrDefault(s => s.Id == orderId);
                if (order == null)
                    return OperationResult<Order>.Fail("Запись не найдена");

                return new OperationResult<Order>
                {
                    Result = OperationStatus.Success,
                    Value = order
                };
            });
        }

        public OperationResult CompleteOrder(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var order = db.Orders.FirstOrDefault(s => s.Id == orderId);
                if (order == null)
                    return OperationResult<Order>.Fail("Запись не найдена");

                order.OrderStatus = OrderStatus.Dispatched;

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult UpdateOrderQrUrl(Guid orderId, string qrUrl)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var order = db.Orders.FirstOrDefault(s => s.Id == orderId);
                if (order == null)
                    return OperationResult<int>.Fail("Запись не найдена");

                order.FiscalBoxData = qrUrl;

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult RejectOrder(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var order = db.Orders.FirstOrDefault(s => s.Id == orderId);
                if (order == null)
                    return OperationResult<Order>.Fail("Запись не найдена");

                order.OrderStatus = OrderStatus.Rejected;

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult<List<OrderDetailsBillModel>> GetOrderDetailBill(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var orderDetails = db.OrderDetails
                    .AsNoTracking()
                    .Include(s => s.Product)
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.Box)
                        .ThenInclude(s => s.DataMatrices)
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.DataMatrix)
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.Pallet)
                        .ThenInclude(s => s.Boxes)
                        .ThenInclude(s => s.DataMatrices)
                    .Where(s => s.OrderId == orderId)
                    .ToList();

                var orderBills = new List<OrderDetailsBillModel>();

                foreach (var orderDetail in orderDetails)
                {
                    if (orderDetail.OrderCodeMappings.Any(s => s.DataMatrixCodeId.HasValue && s.DataMatrix != null))
                    {
                        orderBills.Add(new OrderDetailsBillModel
                        {
                            Barcode = orderDetail.Product.Barcode,
                            Name = orderDetail.Product.Name,
                            Ikpu = orderDetail.Product.Ikpu,
                            UnitOfMeasurement = "шт.",
                            Quantity = orderDetail.Quantity,
                            Price = orderDetail.Price.ToString("#,##0.00"),
                            Vat = Math.Round((orderDetail.Price * 0.15), 2, MidpointRounding.AwayFromZero).ToString(),
                            Codes = orderDetail.OrderCodeMappings.Select(s => s.DataMatrix.Code).ToList(),
                            OrderDetailId = orderDetail.Id
                        });
                    }

                    if (orderDetail.OrderCodeMappings.Any(s => s.BoxId.HasValue && s.Box != null && s.Box.DataMatrices != null && s.Box.DataMatrices.Any()))
                    {
                        orderBills.Add(new OrderDetailsBillModel
                        {
                            Barcode = orderDetail.Product.Barcode,
                            Name = orderDetail.Product.Name,
                            Quantity = orderDetail.Quantity,
                            Ikpu = orderDetail.Product.Ikpu,
                            UnitOfMeasurement = "бл.",
                            Price = orderDetail.Price.ToString("#,##0.00"),
                            Vat = Math.Round((orderDetail.Price * 0.15), 2, MidpointRounding.AwayFromZero).ToString(),
                            Codes = orderDetail.OrderCodeMappings.SelectMany(s => s.Box.DataMatrices.Select(t => t.Code)).ToList(),
                            OrderDetailId = orderDetail.Id
                        });
                    }

                    if (orderDetail.OrderCodeMappings.Any(s => s.PalletId.HasValue && s.Pallet != null && s.Pallet.Boxes != null && s.Pallet.Boxes.Any() 
                                                               && s.Pallet.Boxes.Any(t => t.DataMatrices != null && t.DataMatrices.Any())))
                    {
                        orderBills.Add(new OrderDetailsBillModel
                        {
                            Barcode = orderDetail.Product.Barcode,
                            Name = orderDetail.Product.Name,
                            Quantity = orderDetail.Quantity,
                            Ikpu = orderDetail.Product.Ikpu,
                            UnitOfMeasurement = "пал.",
                            Price = orderDetail.Price.ToString("#,##0.00"),
                            Vat = Math.Round((orderDetail.Price * 0.15), 2, MidpointRounding.AwayFromZero).ToString(),
                            Codes = orderDetail.OrderCodeMappings.Select(s => s.Pallet.Code).ToList(),
                            OrderDetailId = orderDetail.Id
                        });
                    }
                }

                return new OperationResult<List<OrderDetailsBillModel>>
                {
                    Result = OperationStatus.Success,
                    Value = orderBills
                };
            });
        }
        private string RemoveFirstTwoCharacters(string value)
        {
            if (value.Length > 2)
                return value.Remove(0, 2);

            return string.Empty;
        }
    }
}
