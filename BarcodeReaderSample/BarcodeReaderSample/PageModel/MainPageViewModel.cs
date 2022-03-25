using System;
using System.Linq;
using BarcodeReaderSample;
using BarcodeReaderSample.Database;
using BarcodeReaderSample.Pages;
using Microsoft.EntityFrameworkCore;
using TraceIQ.Expeditor.API;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public Command OpenOrdersCommand { get; set; }
        public Command OpenGoodsOnStockCommand { get; set; }
        public Command OpenReturnCommand { get; set; }
        public Command OpenLabelPrinterPage { get; set; }
        public Command SynchronizeData { get; set; }
        public MainPageViewModel(INavigation navigation, HoneywellBarcodeReader scanner)
        {
            Scanner = scanner;
            Navigation = navigation;
            OpenOrdersCommand = new Command(OpenOrders);
            OpenGoodsOnStockCommand = new Command(OpenGoodsOnStock);
            OpenReturnCommand = new Command(OpenReturns);
            OpenLabelPrinterPage = new Command(OpenLabelPrinter);
            SynchronizeData = new Command(Synchronize);
            DbService = new DbService();
        }

        private async void Synchronize()
        {
            var setting = DbService.GetSetting();
            if (setting.Result == OperationStatus.Success && !string.IsNullOrWhiteSpace(setting.Value.Url) && setting.Value.Port != default)
            {
                if (RestContext.User == null)
                {
                    Login();
                    return;
                }
            }
            else
            {
                EnterSettings();
                return;
            }

            await Navigation.PushAsync(new SyncPage(Navigation));
        }

        public async void EnterSettings()
        {
            var serverUrl = await Application.Current.MainPage.DisplayPromptAsync("Введите адрес сервера", string.Empty);
            while (string.IsNullOrWhiteSpace(serverUrl))
            {
                serverUrl = await Application.Current.MainPage.DisplayPromptAsync("Введите адрес сервера", string.Empty);
            }

            var port = await Application.Current.MainPage.DisplayPromptAsync("Введите порт", string.Empty);
            var isPortParseable = int.TryParse(port, out var _);
            
            while (string.IsNullOrWhiteSpace(port) || !isPortParseable)
            {
                port = await Application.Current.MainPage.DisplayPromptAsync("Введите порт", string.Empty);

                isPortParseable = int.TryParse(port, out var _);
            }

            var addSetting = DbService.AddSetting(serverUrl, int.Parse(port));
            if(addSetting.Result != OperationStatus.Success)
                await Application.Current.MainPage.DisplayAlert("Ошибка", addSetting.ErrorMessage, "ОК");
        }

        private async void OpenLabelPrinter()
        {
            
        }


        public async void Login()
        {
            await Navigation.PushAsync(new LoginPage(Navigation));
        }

        private async void OpenOrders()
        {
            var setting = DbService.GetSetting();
            if (setting.Result == OperationStatus.Success && !string.IsNullOrWhiteSpace(setting.Value.Url) && setting.Value.Port != default)
            {
                if (RestContext.User == null)
                {
                    Login();
                    return;
                }
            }
            else
            {
                EnterSettings();
                return;
            }

            await Navigation.PushAsync(new ClientsPage(Navigation, Scanner, DbService));
        }

        private async void OpenGoodsOnStock()
        {
            //var setting = DbService.GetSetting();
            //if (setting.Result == OperationStatus.Success && !string.IsNullOrWhiteSpace(setting.Value.Url) && setting.Value.Port != default)
            //{
            //    if (RestContext.User == null)
            //    {
            //        Login();
            //        return;
            //    }
            //}
            //else
            //{
            //    EnterSettings();
            //    return;
            //}

            await Navigation.PushAsync(new GoodsOnStockPage());
        }

        private async void OpenReturns()
        {
            var setting = DbService.GetSetting();
            if (setting.Result == OperationStatus.Success && !string.IsNullOrWhiteSpace(setting.Value.Url) && setting.Value.Port != default)
            {
                if (RestContext.User == null)
                {
                    Login();
                    return;
                }
            }
            else
            {
                EnterSettings();
                return;
            }
        }

        
    }
}
