using System;
using System.Collections.Generic;
using System.Text;
using BarcodeReaderSample.Models;
using Entities;
using TraceIQ.Expeditor.Models;

namespace BarcodeReaderSample.Interface
{
    public interface IBaseApiService
    {
        OperationResult<User> Authorize(string login, string password);
        OperationResult<List<Product>> GetProducts(Guid transportId);
        OperationResult<List<Supplier>> GetSuppliers(Guid transportId);
        OperationResult<List<Order>> GetOrders(Guid transportId);
        OperationResult<List<OrderDetail>> GetOrderDetails(Guid transportId);
        OperationResult<List<CodesMapping>> GetCodeMapping(Guid transportId);
        OperationResult<Order> GetOrderQrUrl(Guid orderId);
        OperationResult<OperationResult> SendOrderConfirmation(ConfirmOrderModel model);
        OperationResult<OperationResult> SendRejectOrder(Guid orderId, string rejectReason);
        OperationResult<SettingModel> GetSetting();
        OperationResult<OperationResult> SendReportReturn(ReportReturnModel model);
        OperationResult<List<ReportReturn>> CheckReportReturnStatus(List<Guid> returnReportIds);
    }
}
