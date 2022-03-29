using System;
using System.Collections.Generic;
using System.Text;
using BarcodeReaderSample.Models;
using Entities;
using TraceIQ.Expeditor.Models;

namespace BarcodeReaderSample.Interface
{
    public interface IDbService
    {
        OperationResult<Setting> GetSetting();
        OperationResult AddSetting(string url, int port);
        OperationResult<User> Login(string login, string password);

        OperationResult AddUser(User user);

        OperationResult AddProducts(List<Product> products);
        OperationResult AddSuppliers(List<Supplier> suppliers);

        OperationResult AddOrders(List<Order> orders);
        OperationResult AddOrderDetails(List<OrderDetail> orderDetails);
        OperationResult AddCodeMappings(List<CodesMapping> codesMappings);


        OperationResult<List<ClientsModel>> GetClients();
        OperationResult<List<OrdersModel>> GetOrders(Guid supplierId);
        OperationResult<List<OrderDetailsModel>> GetOrderDetails(Guid orderId);
        OperationResult<List<OrderDetailsBillModel>> GetOrderDetailBillTransferTypeOfPayment(Guid orderId);

        OperationResult<ProductApproveModel> GetOrdersByCode(string code, Guid orderId);

        OperationResult ApproveOrderDetail(ProductApproveModel model);

        OperationResult<List<string>> GetOrderDataMatrix(Guid orderId);
        OperationResult UpdateOrderWaiting(Guid orderId, double cash, double terminal);

        OperationResult<bool> GetOrderWaitingStatus(Guid orderId);
        OperationResult<int> GetOrderTotal(Guid orderId);
        OperationResult<Order> GetOrder(Guid orderId);
        OperationResult CompleteOrder(Guid orderId);
        OperationResult UpdateOrderQrUrl(Guid orderId, string qrUrl);
        OperationResult RejectOrder(Guid orderId);
        OperationResult<List<OrderDetailsBillModel>> GetOrderDetailBill(Guid orderId);
    }
}
