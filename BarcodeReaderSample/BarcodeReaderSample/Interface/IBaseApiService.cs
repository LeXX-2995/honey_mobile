using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using TraceIQ.Expeditor.Models;

namespace BarcodeReaderSample.Interface
{
    public interface IBaseApiService
    {
        OperationResult<User> Authorize(string login, string password);
        OperationResult<List<Product>> GetProducts();
        OperationResult<List<Supplier>> GetSuppliers();
        OperationResult<List<Order>> GetOrders(Guid transportId);
        OperationResult<List<OrderDetail>> GetOrderDetails(Guid transportId);
        OperationResult<List<CodesMapping>> GetCodeMapping(Guid transportId);
        OperationResult<Order> GetOrderQrUrl(Guid orderId);
        OperationResult<OperationResult> SendOrderConfirmation(List<string> codes, Guid orderId, double cash, double terminal);
    }
}
