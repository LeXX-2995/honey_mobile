using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Timers;
using BarcodeReaderSample;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Views;
using Java.Util;
using NobelXamarin.Models;
using RestSharp;
using Xamarin.Forms;

namespace NobelXamarin.PageModel
{
    public class DocumentsListModel : BaseViewModel
    {
        public ObservableCollection<DocumentsModel> Documents { get; set; }

        private DocumentsModel _selectedDocument;

        public DocumentsModel SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                _selectedDocument = value;
                OnPropertyChanged(nameof(SelectedDocument));
            }
        }

        public Command SelectItemCommand { get; set; }

        public INavigation Navigation { get; }
        private readonly InterfaceTypes _interfaceTypes;

        public HoneywellBarcodeReader HoneywellBarcodeReader { get; set; }

        public DocumentsListModel(INavigation navigation, InterfaceTypes interfaceTypes, HoneywellBarcodeReader honeywellBarcodeReader)
        {
            Navigation = navigation;
            _interfaceTypes = interfaceTypes;

            Documents = new ObservableCollection<DocumentsModel>();

            HoneywellBarcodeReader = honeywellBarcodeReader;
            
            SelectItemCommand = new Command(OpenProductsList);

            GetData();
        }

        public async void GetData()
        {
            List<DocumentsModel> items;
            switch (_interfaceTypes)
            {
                case InterfaceTypes.Arrival:
                    var reportArrival =
                        RestContext.ExecuteScalar<List<ReportModel>>($"ArrivalApi/GetArrivalReports/{RestContext.UserModel.WarehouseId}", null,
                            Method.GET);

                    if (reportArrival.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", reportArrival.ErrorMessage, "ОК");
                        return;
                    }

                    items = reportArrival.Value.Select(s => new DocumentsModel
                    {
                        Id = s.Id,
                        Number = s.ReportNumber
                    }).ToList();

                    items.ForEach(s => Documents.Add(s));

                    break;
                case InterfaceTypes.Shipment:
                    var reportShipments =
                        RestContext.ExecuteScalar<List<ReportModel>>($"ShipmentApi/GetShipmentReports/{RestContext.UserModel.WarehouseId}", null,
                            Method.GET);

                    if (reportShipments.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", reportShipments.ErrorMessage, "ОК");
                        return;
                    }

                    items = reportShipments.Value.Select(s => new DocumentsModel
                    {
                        Id = s.Id,
                        Number = s.ReportNumber
                    }).ToList();

                    items.ForEach(s => Documents.Add(s));
                    break;
                case InterfaceTypes.Inventory:
                    break;
            }
        }

        private void OnComplete(DocumentsModel model)
        {
            Documents.Remove(model);
        }

        private async void OpenProductsList()
        {
            if (SelectedDocument == null)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Документ не выбран", "ОК");
                return;
            }            
            
            var viewModel = new ProductListView(Navigation, SelectedDocument, _interfaceTypes, HoneywellBarcodeReader);
            // viewModel.Model.OnComplete += OnComplete;
            await Navigation.PushAsync(viewModel);
        }
    }
}
