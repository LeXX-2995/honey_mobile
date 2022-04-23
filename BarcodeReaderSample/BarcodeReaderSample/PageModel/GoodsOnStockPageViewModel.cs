using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Database;
using BarcodeReaderSample.Models;
using Entities;
using NobelXamarin.Helpers;
using TraceIQ.Expeditor.API;
using TraceIQ.Expeditor.Models;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;

namespace BarcodeReaderSample.PageModel
{
    public class GoodsOnStockPageViewModel : BaseViewModel
    {
        public ObservableCollection<OrderDetailsModel> GoodsOnStock { get; set; }
        private readonly SynchronizationContext _mUiContext = SynchronizationContext.Current;
        private bool _isPopupOpen;

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                _isPopupOpen = value;
                OnPropertyChanged(nameof(IsPopupOpen));
            }
        }

        private bool _isReturnVisible;

        public bool IsReturnVisible
        {
            get => _isReturnVisible;
            set
            {
                _isReturnVisible = value;
                OnPropertyChanged(nameof(IsReturnVisible));
            }
        }

        public Command CancelCommand { get; set; }
        public Command CompleteCommand { get; set; }
        public Command CreateReturnCommand { get; set; }
        public GoodsOnStockPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            DbService = new DbService();
            BaseApiService = new BaseApiService();
            CreateReturnCommand = new Command(CreateReturn);
            CancelCommand = new Command(Cancel);
            CompleteCommand = new Command(Complete);

            GoodsOnStock = new ObservableCollection<OrderDetailsModel>();
        }

        private async void Complete()
        {
            IsPopupOpen = false;

            if (!GoodsOnStock.Any())
                return;

            var anyIncompleteOrders = DbService.AnyInCompleteOrders();
            if (anyIncompleteOrders.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", anyIncompleteOrders.ErrorMessage, "ОК");
                return;
            }

            var getReportReturns = DbService.GetIncompleteReportReturns();
            if (getReportReturns.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", getReportReturns.ErrorMessage, "ОК");
                return;
            }

            if (getReportReturns.Value.Any())
            {
                var getReportReturnsApi = BaseApiService.CheckReportReturnStatus(getReportReturns.Value.Select(s => s.Id).ToList());
                if (getReportReturnsApi.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getReportReturnsApi.ErrorMessage, "ОК");
                    return;
                }

                var updateReports = DbService.UpdateReportReturns(getReportReturnsApi.Value);
                if (updateReports.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", updateReports.ErrorMessage, "ОК");
                    return;
                }

                if (getReportReturnsApi.Value.Any(s => s.ReturnStatus != ReturnStatus.Completed))
                {
                    await Application.Current.MainPage.DisplayAlert("Предупреждение", "У вас есть незавершенные документы возвратов", "ОК");
                    return;
                }
            }

            var model = new ReportReturnModel
            { 
                Id = Guid.NewGuid(),
                OrderDetailsModels = GoodsOnStock.ToList(),
                TransportId = RestContext.User.TransportId
            };

            var createReportReturn = DbService.CreateReportReturn(model);
            if (createReportReturn.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", createReportReturn.ErrorMessage, "ОК");
                return;
            }

            var sendReportReturn = BaseApiService.SendReportReturn(model);
            if(sendReportReturn.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", sendReportReturn.ErrorMessage, "ОК");
                return;
            }

            if (sendReportReturn.Value.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", sendReportReturn.Value.ErrorMessage, "ОК");
                return;
            }

            await Navigation.PopAsync(true);
        }

        private async void Cancel()
        {
            IsPopupOpen = false;
            await Navigation.PopAsync(true);
        }

        private void CreateReturn()
        {
            IsPopupOpen = true;
        }

        public async void GetGoodsOnStock()
        {
            _mUiContext.Post(s =>
            {
                GoodsOnStock.Clear();
            }, null);

            var getGoodsOnStock = DbService.GetGoodsOnStock();
            if (getGoodsOnStock.Result != OperationStatus.Success)
            {
                async void SendOrPostCallback(object o)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getGoodsOnStock.ErrorMessage, "ОК");
                }

                _mUiContext.Post(SendOrPostCallback, null);
                return;
            }

            _mUiContext.Post(s =>
            {
                IsReturnVisible = getGoodsOnStock.Value.Any();

                getGoodsOnStock.Value.ForEach(c =>
                {
                    c.UnitOfMeasurementText = c.UnitOfMeasurement.GetDisplayName();
                    GoodsOnStock.Add(c);
                });

            }, null);
        }
    }
}
