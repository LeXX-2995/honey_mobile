using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Android.Content.Res;
using Android.OS.Strictmode;
using Android.Support.V7.Graphics;
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

        public OperationResult IncrementCounter()
        {
            return DigitalTrackingContext.Run(db =>
            {
                var setting = db.Setting.FirstOrDefault();
                if(setting == null)
                    return OperationResult.Fail("Настройки не найдены");

                setting.BillCount++;

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success,
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
                        existingOrder.PartialShipmentAllowed = order.PartialShipmentAllowed;
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

                foreach (var dataMatrix in codesMappings.Where(s => s.DataMatrix != null).Select(s => s.DataMatrix).ToList())
                {
                    db.DataMatrix.Add(dataMatrix);
                }

                foreach (var box in codesMappings.Where(s => s.Box != null).Select(s => s.Box).ToList())
                {
                    db.Boxes.Add(box);
                }

                foreach (var pallet in codesMappings.Where(s => s.Pallet != null).Select(s => s.Pallet).ToList())
                {
                    if(pallet.PalletDataMatrix == null || !pallet.PalletDataMatrix.Any())
                        return OperationResult.Fail("Датаматриксы паллетных кодов не найдены");

                    db.Pallets.Add(pallet);
                }

                db.CodesMappings.AddRange(codesMappings);

                db.SaveChanges();

                var setting = db.Setting.FirstOrDefault();
                if (setting != null)
                    setting.BillCount = 0;

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
                        ProductName = s.Product.Name,
                        AggregationQuantity = s.UnitOfMeasurement == UnitOfMeasurement.Item ? 1 : s.UnitOfMeasurement == UnitOfMeasurement.Box ? s.Product.AmountInBox : s.Product.AmountInPallet
                    })
                    .ToList();

                return new OperationResult<List<OrderDetailsModel>>
                {
                    Result = OperationStatus.Success,
                    Value = orderDetails
                };
            });
        }

        public OperationResult<List<OrderDetailsModel>> GetGoodsOnStock()
        {
            return DigitalTrackingContext.Run(db =>
            {
                var orderDetails = db.Orders
                    .AsNoTracking()
                    .Include(s => s.OrderDetails)
                    .ThenInclude(s => s.Product)
                    .Include(s => s.OrderDetails)
                    .ThenInclude(s => s.OrderCodeMappings)
                    .Where(s => s.PartialShipmentAllowed || s.OrderStatus == OrderStatus.Rejected)
                    .SelectMany(s => s.OrderDetails)
                    .Select(s => new OrderDetailsModel
                    {
                        Id = s.Id,
                        Price = s.Price,
                        UnitOfMeasurement = s.UnitOfMeasurement,
                        Amount = s.Quantity,
                        AssembledAmount = s.OrderCodeMappings != null ? s.OrderCodeMappings.Count : default,
                        ProductName = s.Product.Name,
                        ProductId = s.ProductId,
                        AggregationQuantity = s.UnitOfMeasurement == UnitOfMeasurement.Item ? 1 : s.UnitOfMeasurement == UnitOfMeasurement.Box ? s.Product.AmountInBox : s.Product.AmountInPallet
                    })
                    .Where(s => s.Amount != s.AssembledAmount).ToList();

                orderDetails.ForEach(s =>
                {
                    s.Amount -= s.AssembledAmount;
                    s.AssembledAmount = 0;
                });

                var returnOrderDetails = new List<OrderDetailsModel>();

                foreach (var groupedDetails in orderDetails.GroupBy(s => new {s.ProductId, s.UnitOfMeasurement}))
                {
                    returnOrderDetails.Add(new OrderDetailsModel
                    {
                        UnitOfMeasurement = groupedDetails.Key.UnitOfMeasurement,
                        AssembledAmount = 0,
                        Amount = groupedDetails.Sum(s => s.Amount),
                        ProductName = groupedDetails.First().ProductName,
                        ProductId = groupedDetails.Key.ProductId,
                        OrderDetailIds = groupedDetails.Select(s => s.Id).ToList()
                    });   
                }

                return new OperationResult<List<OrderDetailsModel>>
                {
                    Result = OperationStatus.Success,
                    Value = returnOrderDetails
                };
            });
        }

        public OperationResult<List<OrderDetailsBillModel>> GetOrderDetailBillTransferTypeOfPayment(Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var orderDetails = db.OrderDetails
                    .AsNoTracking()
                    .Include(s => s.Product)
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.Box)
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.DataMatrix)
                    .Include(s => s.OrderCodeMappings)
                        .ThenInclude(s => s.Pallet)
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

                    if (orderDetail.OrderCodeMappings.Any(s => s.BoxId.HasValue && s.Box != null))
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
                            Codes = orderDetail.OrderCodeMappings.Select(s => s.Box.Code).ToList(),
                            OrderDetailId = orderDetail.Id
                        });
                    }

                    if (orderDetail.OrderCodeMappings.Any(s => s.PalletId.HasValue && s.Pallet != null))
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

        public OperationResult<ProductApproveModel> GetOrdersByCode(string code, Guid orderId)
        {
            return DigitalTrackingContext.Run(db =>
            {
                DataMatrix dataMatrix = null;
                Box box = null;

                var pallet = db.Pallets
                    .AsNoTracking()
                    .Include(s => s.Product)
                    .FirstOrDefault(s =>
                        s.Code == code ||
                        s.Code == RemoveFirstTwoCharacters(code) ||
                        s.Code == code.Replace("(", string.Empty).Replace(")", string.Empty));

                if (pallet == null)
                {
                    box = db.Boxes
                        .AsNoTracking()
                        .Include(s => s.Product)
                        .FirstOrDefault(s =>
                            s.Code.Trim() == code.Trim() ||
                            s.Code == RemoveFirstTwoCharacters(code) ||
                            s.Code == code.Replace("(", string.Empty).Replace(")", string.Empty));
                }
                
                if (box == null && pallet == null && code.Length > 10 && !code.Replace("(", string.Empty).Replace(")", string.Empty).All(c => c >= '0' && c <= '9'))
                {
                    dataMatrix = db.DataMatrix
                        .AsNoTracking()
                        .Include(s => s.Product)
                        .FirstOrDefault(s => s.Code.Trim().Contains(code.Substring(0, code.Length - 7)));
                }

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
                    return OperationResult<ProductApproveModel>.Fail($"Товар с кодом {code} не найден");

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

                var orderCodeMapping = unitOfMeasurement.Value switch
                {
                    UnitOfMeasurement.Item => db.OrderCodeMappings
                        .Include(s => s.Order)
                            .ThenInclude(s => s.Supplier)
                        .AsNoTracking().FirstOrDefault(s => s.DataMatrixCodeId == itemId),
                    UnitOfMeasurement.Box => db.OrderCodeMappings
                        .Include(s => s.Order)
                            .ThenInclude(s => s.Supplier)
                        .AsNoTracking().FirstOrDefault(s => s.BoxId == itemId),
                    UnitOfMeasurement.Pallet => db.OrderCodeMappings
                        .Include(s => s.Order)
                            .ThenInclude(s => s.Supplier)
                        .AsNoTracking().FirstOrDefault(s => s.PalletId == itemId),
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (orderCodeMapping != null && orderCodeMapping.OrderId != orderId)
                    return OperationResult<ProductApproveModel>.Fail($"Данный код привязан в заказе '{orderCodeMapping.Order.OrderNumber}' контрагента '{orderCodeMapping.Order.Supplier.Name}'");

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

        public OperationResult AnyInCompleteOrders()
        {
            return DigitalTrackingContext.Run(db =>
            {
                var anyIncompleteOrders = db
                    .Orders
                    .AsNoTracking()
                    .Any(s => s.OrderStatus == OrderStatus.Transit);

                if(anyIncompleteOrders)
                    return OperationResult.Fail("Есть незавершенные заказы.");

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult UpdateReportReturns(List<ReportReturn> reportReturns)
        {
            return DigitalTrackingContext.Run(db =>
            {
                if(!reportReturns.Any())
                    return new OperationResult
                    {
                        Result = OperationStatus.Success
                    };

                var dbReportReturns = db.ReportReturn
                    .Where(s => reportReturns.Select(t => t.Id).Contains(s.Id))
                    .ToList();

                foreach (var report in reportReturns)
                {
                    var reportReturn = dbReportReturns.FirstOrDefault(s => s.Id == report.Id);
                    if (reportReturn != null)
                        reportReturn.ReturnStatus = report.ReturnStatus;
                }

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
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

        public OperationResult CreateReportReturn(ReportReturnModel model)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var reportReturns = db.ReportReturn.AsNoTracking().ToList();

                if (reportReturns.Any())
                {
                    var reportReturnOrderDetailIds = reportReturns.Where(s => !string.IsNullOrWhiteSpace(s.OrderDetails))
                        .SelectMany(s => s.OrderDetails.Split(",").Select(Guid.Parse).ToList()).ToList();

                    var orderDetIds = model.OrderDetailsModels.SelectMany(s => s.OrderDetailIds).ToList();

                    if (orderDetIds.Any(s => reportReturnOrderDetailIds.Contains(s)))
                        return OperationResult.Fail("Документ возврата был уже создан. Синхронизируйтесь пожалуйста.");
                }

                db.ReportReturn.Add(new ReportReturn
                {
                    Id = model.Id,
                    OrderDetails = string.Join(",", model.OrderDetailsModels.SelectMany(s => s.OrderDetailIds).ToList()),
                    ReturnStatus = ReturnStatus.Created,
                });

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        public OperationResult<List<ReportReturn>> GetIncompleteReportReturns()
        {
            return DigitalTrackingContext.Run(db =>
            {
                var reportReturns = db.ReportReturn.AsNoTracking().Where(s => s.ReturnStatus != ReturnStatus.Completed).ToList();

                return new OperationResult<List<ReportReturn>>
                {
                    Result = OperationStatus.Success,
                    Value = reportReturns
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

        public OperationResult UpdateOrderQrUrl(Order order)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var orderEntity = db.Orders.FirstOrDefault(s => s.Id == order.Id);
                if (orderEntity == null)
                    return OperationResult<int>.Fail("Запись не найдена");

                orderEntity.FiscalBoxData = order.FiscalBoxData;
                orderEntity.PartialShipmentAllowed = order.PartialShipmentAllowed;

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

        public OperationResult DeleteAllData()
        {
            return DigitalTrackingContext.Run(db =>
            {
                var codeMappings = db.CodesMappings.ToList();
                db.CodesMappings.RemoveRange(codeMappings);

                var orderDetails = db.OrderDetails.ToList();
                db.OrderDetails.RemoveRange(orderDetails);

                var dataMatrxiCodes = db.DataMatrix.ToList();
                db.DataMatrix.RemoveRange(dataMatrxiCodes);

                var boxes = db.Boxes.ToList();
                db.Boxes.RemoveRange(boxes);

                var pallet = db.Pallets.ToList();
                db.Pallets.RemoveRange(pallet);

                var orders = db.Orders.ToList();
                db.Orders.RemoveRange(orders);

                db.SaveChanges();

                return new OperationResult
                {
                    Result = OperationStatus.Success
                };
            });
        }

        

        public OperationResult CheckGoodsOnStockWithReportReturns(List<OrderDetailsModel> detailsModel)
        {
            return DigitalTrackingContext.Run(db =>
            {
                var reportReturns = db.ReportReturn.AsNoTracking().ToList();

                var orderDetIds = detailsModel.SelectMany(s => s.OrderDetailIds).ToList();

                if(!reportReturns.Any())
                    return new OperationResult
                    {
                        Result = OperationStatus.Failed,
                        ErrorMessage = "Есть остатки на транспорте и не создан документ возврата"
                    };

                var reportReturnOrderDetailIds = reportReturns.Where(s => !string.IsNullOrWhiteSpace(s.OrderDetails))
                    .SelectMany(s => s.OrderDetails.Split(",").Select(Guid.Parse).ToList()).ToList();
                
                if(!orderDetIds.Any(s => reportReturnOrderDetailIds.Contains(s)))
                    return new OperationResult
                    {
                        Result = OperationStatus.Failed,
                        ErrorMessage = "Есть остатки на транспорте и не создан документ возврата"
                    };

                foreach (var reportReturn in reportReturns)
                {
                    if (!string.IsNullOrWhiteSpace(reportReturn.OrderDetails))
                    {
                        var orderDetailIds = reportReturn.OrderDetails.Split(",").Select(Guid.Parse).ToList();
                        if (orderDetIds.Any(s => orderDetailIds.Contains(s)) && reportReturn.ReturnStatus != ReturnStatus.Completed)
                            return new OperationResult
                            {
                                Result = OperationStatus.Failed,
                                ErrorMessage = "Есть незавершенные документы возвратов"
                            };
                    }
                }

                return new OperationResult
                {
                    Result = OperationStatus.Success,
                };
            });
        }

        public OperationResult<List<OrderDetailsBillModel>> GetOrderDetailBill(Guid orderId, bool isForPrinting = false)
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
                        .ThenInclude(s => s.PalletDataMatrix)
                    .Where(s => s.OrderId == orderId)
                    .ToList();

                var orderBills = new List<OrderDetailsBillModel>();

                foreach (var orderDetail in orderDetails)
                {
                    if (orderDetail.OrderCodeMappings.Any(s => s.DataMatrixCodeId.HasValue && s.DataMatrix != null))
                    {
                        var orderCodeMappingCount = orderDetail.OrderCodeMappings.Count;

                        var bill = new OrderDetailsBillModel
                        {
                            Barcode = orderDetail.Product.Barcode,
                            Name = orderDetail.Product.Name,
                            Ikpu = orderDetail.Product.Ikpu,
                            UnitOfMeasurement = "шт.",
                            Quantity = orderDetail.Quantity != orderCodeMappingCount
                                ? orderCodeMappingCount
                                : orderDetail.Quantity,
                            Price = orderDetail.Price.ToString("#,##0.00"),
                            Vat = orderDetail.Vat.ToString("#,##0.00"),
                            Codes = orderDetail.OrderCodeMappings.Select(s => s.DataMatrix.Code).ToList(),
                            OrderDetailId = orderDetail.Id,
                        };

                        bill.AmountInItems = orderDetail.UnitOfMeasurement == UnitOfMeasurement.Item
                            ? bill.Quantity
                            : orderDetail.UnitOfMeasurement == UnitOfMeasurement.Box
                                ? bill.Quantity * orderDetail.Product.AmountInBox
                                : bill.Quantity * orderDetail.Product.AmountInPallet;

                        bill.TotalVat = bill.AmountInItems * orderDetail.Vat;

                        orderBills.Add(bill);
                    }

                    if (orderDetail.OrderCodeMappings.Any(s => s.BoxId.HasValue && s.Box != null && s.Box.DataMatrices != null && s.Box.DataMatrices.Any()))
                    {
                        var orderCodeMappingCount = orderDetail.OrderCodeMappings.Count;

                        var bill = new OrderDetailsBillModel
                        {
                            Barcode = orderDetail.Product.Barcode,
                            Name = orderDetail.Product.Name,
                            Quantity = orderDetail.Quantity != orderCodeMappingCount
                                ? orderCodeMappingCount
                                : orderDetail.Quantity,
                            Ikpu = orderDetail.Product.Ikpu,
                            UnitOfMeasurement = "шт.",
                            Price = orderDetail.Price.ToString("#,##0.00"),
                            Vat = orderDetail.Vat.ToString("#,##0.00"),
                            Codes = orderDetail.OrderCodeMappings
                                .SelectMany(s => s.Box.DataMatrices.Select(t => t.Code)).ToList(),
                            OrderDetailId = orderDetail.Id,
                            
                        };

                        bill.AmountInItems = orderDetail.UnitOfMeasurement == UnitOfMeasurement.Item
                            ? bill.Quantity
                            : orderDetail.UnitOfMeasurement == UnitOfMeasurement.Box
                                ? bill.Quantity * orderDetail.Product.AmountInBox
                                : bill.Quantity * orderDetail.Product.AmountInPallet;

                        bill.TotalVat = bill.AmountInItems * Math.Round(orderDetail.Vat, 2, MidpointRounding.AwayFromZero);

                        orderBills.Add(bill);
                    }

                    if (orderDetail.OrderCodeMappings.Any(s => s.PalletId.HasValue && s.Pallet != null && s.Pallet.PalletDataMatrix != null && s.Pallet.PalletDataMatrix.Any()))
                    {
                        var orderCodeMappingCount = orderDetail.OrderCodeMappings.Count;

                        var bill = new OrderDetailsBillModel
                        {
                            Barcode = orderDetail.Product.Barcode,
                            Name = orderDetail.Product.Name,
                            Quantity = orderDetail.Quantity != orderCodeMappingCount
                                ? orderCodeMappingCount
                                : orderDetail.Quantity,
                            Ikpu = orderDetail.Product.Ikpu,
                            UnitOfMeasurement = "шт.",
                            Price = orderDetail.Price.ToString("#,##0.00"),
                            Vat = orderDetail.Vat.ToString("#,##0.00"),
                            Codes = isForPrinting
                                ? orderDetail.OrderCodeMappings
                                    .SelectMany(s => s.Pallet.PalletDataMatrix.Select(t => t.Code)).ToList()
                                : orderDetail.OrderCodeMappings.Select(s => s.Pallet.Code).ToList(),
                            OrderDetailId = orderDetail.Id
                        };

                        bill.AmountInItems = orderDetail.UnitOfMeasurement == UnitOfMeasurement.Item
                            ? bill.Quantity
                            : orderDetail.UnitOfMeasurement == UnitOfMeasurement.Box
                                ? bill.Quantity * orderDetail.Product.AmountInBox
                                : bill.Quantity * orderDetail.Product.AmountInPallet;

                        bill.TotalVat = bill.AmountInItems * orderDetail.Vat;

                        orderBills.Add(bill);
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
