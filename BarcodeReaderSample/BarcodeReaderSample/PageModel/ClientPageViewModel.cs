using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BarcodeReaderSample;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Pages;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class ClientPageViewModel : BaseViewModel
    {
        public ObservableCollection<ClientsModel> Clients { get; set; }
        private readonly SynchronizationContext _mUiContext = SynchronizationContext.Current;
        public Command SelectClientCommand { get; set; }
        public ClientPageViewModel(INavigation navigation, HoneywellBarcodeReader scanner, IDbService dbService)
        {
            Scanner = scanner;
            Navigation = navigation;
            DbService = dbService;

            SelectClientCommand = new Command<ClientsModel>(SelectClient);

            Clients = new ObservableCollection<ClientsModel>();
        }

        private async void SelectClient(ClientsModel obj)
        {
            await Navigation.PushAsync(new OrdersPage(Navigation, Scanner, obj.Id, DbService));
        }

        public void GetClients()
        {
            Clients.Clear();

            var getClients = DbService.GetClients();
            if(getClients.Result != OperationStatus.Success)
            {
                async void SendOrPostCallback(object o)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getClients.ErrorMessage, "ОК");
                }

                _mUiContext.Post(SendOrPostCallback, null);
            }

            _mUiContext.Post(s =>
            {
                getClients.Value.ForEach(c => Clients.Add(c));
            }, null);
        }
    }
}
