using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BarcodeReaderSample;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Pages;
using Plugin.SatoMpXamarinSDK.Abstractions;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class OrderProductsPageViewModel : BaseViewModel
    {
        public ObservableCollection<OrderDetailsModel> OrderDetails { get; set; }
        private readonly SynchronizationContext _mUiContext = SynchronizationContext.Current;
        private ILabelPrinterDevice _printer = null;
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
        }

        private async void Print()
        {
            var getOrderQrUrl = BaseApiService.GetOrderQrUrl(_orderId);
            if (getOrderQrUrl.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", getOrderQrUrl.ErrorMessage, "ОК");
                return;
            }

            if (getOrderQrUrl.Value == null || string.IsNullOrWhiteSpace(getOrderQrUrl.Value.QrPaymentUrl))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Данные регистрации продажи еще не прошли", "ОК");
                return;
            }

            var updateOrderQrUrl = DbService.UpdateOrderQrUrl(_orderId, getOrderQrUrl.Value.QrPaymentUrl);
            if(updateOrderQrUrl.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", updateOrderQrUrl.ErrorMessage, "ОК"); return;
            }


        }

        private async void Reject()
        {
            
        }

        private async void Accept()
        {
            await Navigation.PushAsync(new AcceptPage(Navigation, DbService, _orderId));
        }

        public async void ScannerOnBarcodeRead(object sender, Honeywell.AIDC.CrossPlatform.BarcodeDataArgs e)
        {
            var getOrderByCode = DbService.GetOrdersByCode(e.Data, _orderId);
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
            OrderDetails.Clear();

            var getOrder = DbService.GetOrderWaitingStatus(_orderId);
            if (getOrder.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", getOrder.ErrorMessage, "ОК");
                return;
            }

            if (getOrder.Value)
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
                    OrderDetails.Add(c);
                });

                Total = getOrderDetails.Value.Sum(t => t.AssembledAmount * t.Price);
            }, null);
        }


    }
}
