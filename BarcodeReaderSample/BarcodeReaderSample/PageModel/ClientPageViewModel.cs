using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BarcodeReaderSample;
using BarcodeReaderSample.Pages;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class ClientPageViewModel : BaseViewModel
    {
        public ObservableCollection<ClientsModel> Clients { get; set; }
        public Command SelectClientCommand { get; set; }
        public ClientPageViewModel(INavigation navigation, HoneywellBarcodeReader scanner)
        {
            Scanner = scanner;
            Navigation = navigation;

            SelectClientCommand = new Command<ClientsModel>(SelectClient);

            Clients = new ObservableCollection<ClientsModel>
            {
                new ClientsModel
                {
                    Name = "\"AFSAR COMPANY LTD\" MAS`ULIYATI CHEKLANGAN JAMIYAT",
                    Id = Guid.NewGuid()
                },
                new ClientsModel
                {
                    Name = "Andrey",                    
                    Id = Guid.NewGuid()
                },
                new ClientsModel
                {
                    Name = "\"TOSHKENTVINO KOMBINATI\" AKSIYADORLIK JAMIYATI CHET EL INVESTITSIYALARI ISHTIROKIDAGI",
                    Id = Guid.NewGuid()
                },
                new ClientsModel
                {
                    Name = "ООО \"HAMKOR OSIYO\"",
                    Id = Guid.NewGuid()
                },
            };
        }

        private async void SelectClient(ClientsModel obj)
        {
            await Navigation.PushAsync(new OrdersPage(Navigation, Scanner));
        }
    }
}
