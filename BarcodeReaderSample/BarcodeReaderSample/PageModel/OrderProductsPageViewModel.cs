using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BarcodeReaderSample;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Models;
using BarcodeReaderSample.Pages;
using Entities;
using NobelXamarin.Helpers;
using Plugin.SatoMpXamarinSDK.Abstractions;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class OrderProductsPageViewModel : BaseViewModel
    {
        public ObservableCollection<OrderDetailsModel> OrderDetails { get; set; }
        private readonly SynchronizationContext _mUiContext = SynchronizationContext.Current;
        private readonly Guid _orderId;
        private double _total;
        public double Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public Command AcceptCommand { get; set; }
        public Command RejectCommand { get; set; }
        public Command PrintCommand { get; set; }
        public Command CompleteCommand { get; set; }
        public Command YesCommand { get; set; }
        public Command NoCommand { get; set; }

        private bool _isPrintButtonVisible;
        public bool IsPrintButtonVisible
        {
            get => _isPrintButtonVisible;
            set
            {
                _isPrintButtonVisible = value;
                OnPropertyChanged(nameof(IsPrintButtonVisible));
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

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

        private bool _isRejectPopupOpen;

        public bool IsRejectPopupOpen
        {
            get => _isRejectPopupOpen;
            set
            {
                _isRejectPopupOpen = value;
                OnPropertyChanged(nameof(IsRejectPopupOpen));
            }
        }
        private string _rejectReason;

        public string RejectReason
        {
            get => _rejectReason;
            set
            {
                _rejectReason = value;
                OnPropertyChanged(nameof(RejectReason));
            }
        }

        public Command OpenCompletionCommand { get; set; }
        public Command CancelCommand { get; set; }
        public PaymentType? PaymentType { get; set; }

        private string _paymentTypeText;

        public string PaymentTypeText
        {
            get => _paymentTypeText;
            set
            {
                _paymentTypeText = value;
                OnPropertyChanged(nameof(PaymentTypeText));
            }
        }


        public OrderProductsPageViewModel(INavigation navigation, HoneywellBarcodeReader scanner, IDbService dbService, Guid orderId)
        {
            Scanner = scanner;
            Navigation = navigation;
            _orderId = orderId;
            DbService = dbService;
            BaseApiService = new BaseApiService();
            OrderDetails = new ObservableCollection<OrderDetailsModel>();

            AcceptCommand = new Command(Accept);
            RejectCommand = new Command(Reject);
            PrintCommand = new Command(Print);
            CompleteCommand = new Command(Complete);
            OpenCompletionCommand = new Command(OpenCompletion);
            CancelCommand = new Command(Cancel);
            YesCommand = new Command(YesForReject);
            NoCommand = new Command(Cancel);
        }

        private async void YesForReject()
        {
            if (string.IsNullOrWhiteSpace(RejectReason))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Причина пустая. Пожалуйста заполните ее", "ОК");
                return;
            }

            IsRejectPopupOpen = false;

            var sendReject = BaseApiService.SendRejectOrder(_orderId, RejectReason);
            if (sendReject.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", sendReject.ErrorMessage, "ОК");
                return;
            }

            var rejectOrder = DbService.RejectOrder(_orderId);
            if(rejectOrder.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", rejectOrder.ErrorMessage, "ОК");
                return;
            }

            await Navigation.PopAsync();
        }

        private async void Cancel()
        {
            IsPopupOpen = false;
            IsRejectPopupOpen = false;
        }

        private async void OpenCompletion()
        {
            IsPopupOpen = true;
        }

        private async void Complete()
        {
            IsPopupOpen = false;

            if (!PaymentType.HasValue)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Тип оплаты не найден", "ОК");
                return;
            }

            if (PaymentType.Value == Entities.PaymentType.Deferred)
            {
                var getCodes = DbService.GetOrderDetailBillTransferTypeOfPayment(_orderId);
                if (getCodes.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getCodes.ErrorMessage, "ОК");
                    return;
                }

                var confirmOrder = BaseApiService.SendOrderConfirmation(new ConfirmOrderModel
                {
                    OrderId = _orderId,
                    Terminal = default,
                    Cash = default,
                    ConfirmProductsModels = getCodes.Value.Select(s => new ConfirmProductsModel
                    {
                        OrderDetailId = s.OrderDetailId,
                        Codes = s.Codes
                    }).ToList()
                });

                if(confirmOrder.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", confirmOrder.ErrorMessage, "ОК");
                    return;
                }
            }

            var completeOrder = DbService.CompleteOrder(_orderId);
            if (completeOrder.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", completeOrder.ErrorMessage, "ОК");
                return;
            }

            await Navigation.PopAsync(true);
        }

        private async void Print()
        {
            var getOrderQrUrl = BaseApiService.GetOrderQrUrl(_orderId);
            if (getOrderQrUrl.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", getOrderQrUrl.ErrorMessage, "ОК");
                return;
            }

            if (getOrderQrUrl.Value == null || string.IsNullOrWhiteSpace(getOrderQrUrl.Value.FiscalBoxData))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Данные регистрации продажи еще не прошли", "ОК");
                return;
            }

            getOrderQrUrl.Value.Id = _orderId;

            var updateOrderQrUrl = DbService.UpdateOrderQrUrl(getOrderQrUrl.Value);
            if(updateOrderQrUrl.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", updateOrderQrUrl.ErrorMessage, "ОК"); 
                return;
            }

            var getOrderDetails = DbService.GetOrderDetails(_orderId);
            if (getOrderDetails.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", getOrderDetails.ErrorMessage, "ОК");
                return;
            }
            
            var total = (int)getOrderDetails.Value.Sum(
                    t => t.AssembledAmount * t.AggregationQuantity * t.Price);

            if(total == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Сумма нулевая", "ОК");
                return;
            }

            var getSetting = BaseApiService.GetSetting();
            if (getSetting.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", getSetting.ErrorMessage, "ОК");
                return;
            }

            if(getSetting.Value == null)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "ИНН не найден", "ОК");
                return;
            }

            if (string.IsNullOrWhiteSpace(getSetting.Value.Inn))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "ИНН не найден", "ОК");
                return;
            }

            if (string.IsNullOrWhiteSpace(getSetting.Value.CompanyName))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Наименование организации пустая", "ОК");
                return;
            }

            await Navigation.PushAsync(new LabelPrinterPage(_orderId, DbService, total, getSetting.Value.Inn, getSetting.Value.CompanyName));
        }

        private async void Reject()
        {
            IsRejectPopupOpen = true;
        }

        private async void Accept()
        {
            if (OrderDetails.Any(s => s.Amount != s.AssembledAmount))
            {
                var getOrderQrUrl = BaseApiService.GetOrderQrUrl(_orderId);
                if (getOrderQrUrl.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getOrderQrUrl.ErrorMessage, "ОК");
                    return;
                }

                if (!getOrderQrUrl.Value.PartialShipmentAllowed)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Не все позиции были собраны", "ОК");
                    return;
                }

                getOrderQrUrl.Value.Id = _orderId;

                var updateOrderQrUrl = DbService.UpdateOrderQrUrl(getOrderQrUrl.Value);
                if (updateOrderQrUrl.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", updateOrderQrUrl.ErrorMessage, "ОК");
                    return;
                }
            }

            if(!PaymentType.HasValue)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Тип оплаты не найден", "ОК");
                return;
            }

            if (PaymentType.Value == Entities.PaymentType.Deferred)
            {
                _mUiContext.Post(s =>
                {
                    IsPopupOpen = true;
                }, null);

                return;
            }

            await Navigation.PushAsync(new AcceptPage(Navigation, DbService, _orderId));
        }

        public async void ScannerOnBarcodeRead(object sender, Honeywell.AIDC.CrossPlatform.BarcodeDataArgs e)
        {
            var getOrderByCode = DbService.GetOrdersByCode(
                e.Data, 
                _orderId);

            if (getOrderByCode.Result != OperationStatus.Success)
            {
                async void SendOrPostCallback(object s)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getOrderByCode.ErrorMessage, "ОК");
                }

                _mUiContext.Post(SendOrPostCallback, null);
                return;
            }

            try
            {
                _mUiContext.Post(_ =>
                {
                    Navigation.PushAsync(new ProductApprovePage(getOrderByCode.Value, Navigation, DbService));
                }, null);

            }
            catch (Exception exception)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", exception.Message, "ОК");
            }
        }

        public async void GetOrderDetails()
        {
            _mUiContext.Post(s =>
            {
                OrderDetails.Clear();
            }, null);

            var getOrder = DbService.GetOrder(_orderId);
            if (getOrder.Result != OperationStatus.Success)
                await Application.Current.MainPage.DisplayAlert("Ошибка", getOrder.ErrorMessage, "ОК");
            else
            {
                PaymentType = getOrder.Value.PaymentType;
                PaymentTypeText = PaymentType.GetDisplayName();
            }

            if (getOrder.Value.IsWaitingFiscalBox)
            {
                _mUiContext.Post(s =>
                {
                    IsPrintButtonVisible = true;
                    IsVisible = false;
                }, null);
            }
            else
            {
                _mUiContext.Post(s =>
                {
                    IsPrintButtonVisible = false;
                    IsVisible = true;
                }, null);
            }

            var getOrderDetails  = DbService.GetOrderDetails(_orderId);
            if (getOrderDetails.Result != OperationStatus.Success)
            {
                async void SendOrPostCallback(object o)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getOrderDetails.ErrorMessage, "ОК");
                }

                _mUiContext.Post(SendOrPostCallback, null);
                return;
            }

            _mUiContext.Post(s =>
            {
                getOrderDetails.Value.ForEach(c =>
                {
                    c.UnitOfMeasurementText = c.UnitOfMeasurement.GetDisplayName();
                    OrderDetails.Add(c);
                });

                Total = getOrderDetails.Value.Sum(t => t.AssembledAmount * t.AggregationQuantity * t.Price);
            }, null);
        }


    }
}
