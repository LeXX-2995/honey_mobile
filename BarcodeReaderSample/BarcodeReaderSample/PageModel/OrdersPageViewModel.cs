using System;
using System.Collections.ObjectModel;
using BarcodeReaderSample;
using BarcodeReaderSample.Pages;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class OrdersPageViewModel : BaseViewModel
    {
        public ObservableCollection<OrdersModel> Orders { get; set; }
        public Command SelectOrderCommand { get; set; }
        public OrdersPageViewModel(INavigation navigation, HoneywellBarcodeReader scanner)
        {
            Scanner = scanner;
            Navigation = navigation;

            SelectOrderCommand = new Command<OrdersModel>(SelectOrder);

            Orders = new ObservableCollection<OrdersModel>
            {
                new OrdersModel
                {
                    OrderName = "Заказ №1",
                    Id = Guid.NewGuid(),
                },
                new OrdersModel
                {
                    OrderName = "Заказ №2",
                    Id = Guid.NewGuid(),
                },
                new OrdersModel
                {
                    OrderName = "Заказ №334",
                    Id = Guid.NewGuid(),
                },
                new OrdersModel
                {
                    OrderName = "Заказ №12",
                    Id = Guid.NewGuid(),
                },
                new OrdersModel
                {
                    OrderName = "Заказ №56",
                    Id = Guid.NewGuid(),
                },
            };
        }

        private async void SelectOrder(OrdersModel order)
        {
            await Navigation.PushAsync(new OrderProductsPage(Navigation, Scanner));
        }
    }
}
