using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using BarcodeReaderSample;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Pages;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class OrdersPageViewModel : BaseViewModel
    {
        public ObservableCollection<OrdersModel> Orders { get; set; }
        private readonly SynchronizationContext _mUiContext = SynchronizationContext.Current;
        public Command SelectOrderCommand { get; set; }
        private readonly Guid _supplierId;
        public OrdersPageViewModel(INavigation navigation, HoneywellBarcodeReader scanner, Guid supplierId, IDbService dbvService)
        {
            _supplierId = supplierId;
            Scanner = scanner;
            Navigation = navigation;
            DbService = dbvService;

            SelectOrderCommand = new Command<OrdersModel>(SelectOrder);

            Orders = new ObservableCollection<OrdersModel>();

        }

        private async void SelectOrder(OrdersModel order)
        {
            await Navigation.PushAsync(new OrderProductsPage(Navigation, Scanner, DbService, order.Id));
        }

        public void GetOrders()
        {
            _mUiContext.Post(SendOrPostCallback, null);
        }

        private async void SendOrPostCallback(object s)
        {
            Orders.Clear();

            var getOrders = DbService.GetOrders(_supplierId);
            if (getOrders.Result != OperationStatus.Success)
            {
                //async void SendOrPostCallback(object o)
                //{
                await Application.Current.MainPage.DisplayAlert("Ошибка", getOrders.ErrorMessage, "ОК");
                //}

                //_mUiContext.Post(SendOrPostCallback, null);
            }


            getOrders.Value.ForEach(o => Orders.Add(o));
        }
    }
}
