﻿using System;
using System.Collections.Generic;
using System.Text;
using BarcodeReaderSample.Database;
using BarcodeReaderSample.Interface;
using Entities;
using RestSharp;
using TraceIQ.Expeditor.API;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace BarcodeReaderSample.API
{
    public class BaseApiService : IBaseApiService
    {
        private readonly Setting _setting;

        private const string AuthorizeResource = "Expeditor/Login/";
        private const string ProductsResource = "Expeditor/GetProducts";
        private const string SuppliersResource = "Expeditor/GetSuppliers";
        private const string OrdersResource = "Expeditor/GetOrders/";
        private const string OrderDetailsResource = "Expeditor/GetOrderDetails/";
        private const string CodeMappingsResource = "Expeditor/GetCodeMappings/";
        private const string ConfirmOrderResource = "Expeditor/ConfirmOrder";
        private const string OrderQrUrlResource = "Expeditor/GetOrderQrUrl/";
        public BaseApiService()
        {
            IDbService dbService = new DbService();
            _setting = dbService.GetSetting().Value;
        }

        public OperationResult<User> Authorize(string login, string password)
        {
            var checkSetting = CheckSetting();
            if (checkSetting.Result != OperationStatus.Success)
                return OperationResult<User>.Fail(checkSetting.ErrorMessage);

            var getUser = RestContext.ExecuteScalar<User>(AuthorizeResource + $"{login}/{password}", null, Method.GET);
            if (getUser.Result != OperationStatus.Success)
                return OperationResult<User>.Fail(getUser.ErrorMessage);

            if (getUser.Value.RoleType != RoleType.Expeditor)
                return OperationResult<User>.Fail("Пользователь не является экспедитором");

            return getUser;
        }

        public OperationResult<List<Product>> GetProducts()
        {
            var checkSetting = CheckSetting();
            if (checkSetting.Result != OperationStatus.Success)
                return OperationResult<List<Product>>.Fail(checkSetting.ErrorMessage);

            return RestContext.ExecuteScalar<List<Product>>(ProductsResource, null, Method.GET); ;
        }

        public OperationResult<List<Supplier>> GetSuppliers()
        {
            var checkSetting = CheckSetting();
            if (checkSetting.Result != OperationStatus.Success)
                return OperationResult<List<Supplier>>.Fail(checkSetting.ErrorMessage);

            return RestContext.ExecuteScalar<List<Supplier>>(SuppliersResource, null, Method.GET);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public OperationResult<List<Order>> GetOrders(Guid transportId)
        {
            var checkSetting = CheckSetting();
            if (checkSetting.Result != OperationStatus.Success)
                return OperationResult<List<Order>>.Fail(checkSetting.ErrorMessage);

            return RestContext.ExecuteScalar<List<Order>>(OrdersResource + transportId, null, Method.GET);
        }

        public OperationResult<List<OrderDetail>> GetOrderDetails(Guid transportId)
        {
            var checkSetting = CheckSetting();
            if (checkSetting.Result != OperationStatus.Success)
                return OperationResult<List<OrderDetail>>.Fail(checkSetting.ErrorMessage);

            return RestContext.ExecuteScalar<List<OrderDetail>>(OrderDetailsResource + transportId, null, Method.GET);
        }

        public OperationResult<List<CodesMapping>> GetCodeMapping(Guid transportId)
        {
            var checkSetting = CheckSetting();
            if (checkSetting.Result != OperationStatus.Success)
                return OperationResult<List<CodesMapping>>.Fail(checkSetting.ErrorMessage);

            return RestContext.ExecuteScalar<List<CodesMapping>>(CodeMappingsResource + transportId, null, Method.GET);
        }

        public OperationResult<Order> GetOrderQrUrl(Guid orderId)
        {
            var checkSetting = CheckSetting();
            if (checkSetting.Result != OperationStatus.Success)
                return OperationResult<Order>.Fail(checkSetting.ErrorMessage);

            return RestContext.ExecuteScalar<Order>(OrderQrUrlResource + orderId, null, Method.GET);
        }

        public OperationResult<OperationResult> SendOrderConfirmation(List<string> codes, Guid orderId, double cash, double terminal)
        {
            var checkSetting = CheckSetting();
            if (checkSetting.Result != OperationStatus.Success)
                return OperationResult<OperationResult>.Fail(checkSetting.ErrorMessage);

            var model = new
            {
                OrderId = orderId,
                Codes = codes,
                Cash = cash,
                Terminal = terminal
            };

            return RestContext.ExecuteScalar<OperationResult>(ConfirmOrderResource, null, Method.POST, model);
        }


        private OperationResult CheckSetting()
        {
            if (string.IsNullOrWhiteSpace(_setting.Url) || _setting.Port == default)
                return new OperationResult
                {
                    Result = OperationStatus.Failed,
                    ErrorMessage = "Данные для подключения не указаны"
                };

            RestContext.Ip = $"https://{_setting.Url}:";
            RestContext.Port = _setting.Port.ToString();
            RestContext.Url = RestContext.Ip + RestContext.Port + RestContext.ApiResource;

            return new OperationResult
            {
                Result = OperationStatus.Success
            };
        }
    }
}
