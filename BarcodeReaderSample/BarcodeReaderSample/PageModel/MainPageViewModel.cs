using System.Linq;
using BarcodeReaderSample;
using BarcodeReaderSample.Database;
using BarcodeReaderSample.Pages;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public Command OpenOrdersCommand { get; set; }
        public Command OpenGoodsOnStockCommand { get; set; }
        public Command OpenReturnCommand { get; set; }
        public Command OpenLabelPrinterPage { get; set; }

        public DigitalTrackingContext _db { get; set; }
        public MainPageViewModel(INavigation navigation, HoneywellBarcodeReader scanner)
        {
            Scanner = scanner;
            Navigation = navigation;

            OpenOrdersCommand = new Command(OpenOrders);
            OpenGoodsOnStockCommand = new Command(OpenGoodsOnStock);
            OpenReturnCommand = new Command(OpenReturns);
            OpenLabelPrinterPage = new Command(OpenLabelPrinter);

        }

        private async void OpenLabelPrinter()
        {
            await Navigation.PushAsync(new LabelPrinterPage());
        }


        public async void Login()
        {
            
        }

        private async void OpenOrders()
        {
            await Navigation.PushAsync(new ClientsPage(Navigation, Scanner));
        }

        private async void OpenGoodsOnStock()
        {

        }

        private async void OpenReturns()
        {

        }
    }
}
