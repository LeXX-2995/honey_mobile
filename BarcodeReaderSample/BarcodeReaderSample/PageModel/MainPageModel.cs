using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BarcodeReaderSample;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Views;
using FreshMvvm;
using Java.Util;
using NobelXamarin.Models;
using RestSharp;
using Xamarin.Forms;

namespace NobelXamarin.PageModel
{
    public class MainPageModel : BaseViewModel
    {
        public Command OpenArrivalCommand { get; set; }
        public Command OpenShipmentCommand { get; set; }
        public Command OpenInventoryCommand { get; set; }
        public Command OpenWriteOffCommand { get; set; }
        public Command OpenDisaggregationCommand { get; set; }
        private readonly INavigation _navigation;
        public HoneywellBarcodeReader HoneywellBarcodeReader { get; }
        public MainPageModel(INavigation navigation, HoneywellBarcodeReader honeywellBarcodeReader)
        {
            _navigation = navigation;
            OpenArrivalCommand = new Command<InterfaceTypes>(OpenDocumentsList);
            OpenShipmentCommand = new Command<InterfaceTypes>(OpenDocumentsList);
            OpenWriteOffCommand = new Command<InterfaceTypes>(OpenDocumentsList);
            OpenInventoryCommand = new Command<InterfaceTypes>(OpenInventory);
            OpenDisaggregationCommand = new Command(OpenDisaggregation);

            HoneywellBarcodeReader = honeywellBarcodeReader;
            
            if(RestContext.UserModel == null)
                Login();
        }

        private async void Login()
        {
            await _navigation.PushAsync(new LoginPage(_navigation));
        }

        private async void OpenDisaggregation()
        {
            if (RestContext.UserModel == null)
            {
                Login();
                return;
            }
            await _navigation.PushAsync(new DisaggregationView(_navigation));
        }

        private async void OpenInventory(InterfaceTypes interfaceTypes)
        {
            if (RestContext.UserModel == null)
            {
                Login();
                return;
            }
            
            var inventory = RestContext.ExecuteScalar<ReportInventoryModel>($"InventoryApi/GetReportInventory/{RestContext.UserModel.WarehouseId}", null, Method.GET);

            if (inventory.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", inventory.Result.ToString(), "ОК");
                return;
            }

            if (string.IsNullOrWhiteSpace(inventory.Value.ReportNumber))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Нет открытых инвентаризаций", "ОК");
                return;
            }

            var selectedDocument = new DocumentsModel
            {
                Number = inventory.Value.ReportNumber,
                Id = inventory.Value.Id
            };

            await _navigation.PushAsync(new ProductListView(_navigation, selectedDocument, InterfaceTypes.Inventory, HoneywellBarcodeReader));
        }

        private async void OpenDocumentsList(InterfaceTypes interfaceTypes)
        {
            if (RestContext.UserModel == null)
            {
                Login();
                return;
            }
            await _navigation.PushAsync(new DocumentsListView(_navigation, interfaceTypes, HoneywellBarcodeReader));
        }
    }
}
