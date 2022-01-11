using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using BarcodeReaderSample;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Views;
using FreshMvvm;
using Honeywell.AIDC.CrossPlatform;
using NobelXamarin.Models;
using RestSharp;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NobelXamarin.PageModel
{
    public class ProductListModel : BaseViewModel
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public ObservableCollection<ProductsModel> Products { get; set; }
        private SynchronizationContext mUIContext = SynchronizationContext.Current;
        private INavigation Navigation { get; }
        private string _selectedNumber;

        public string SelectedNumber
        {
            get => "Номер документа: " + _selectedNumber;
            set
            {
                _selectedNumber = value;
                OnPropertyChanged(nameof(SelectedNumber));
            }
        }
        private readonly string[] _unicodeCharacters = { "\u0002", "\u0018", "\r", "\n" };
        public Command CompleteCommand { get;  }

        public InterfaceTypes InterfaceTypes { get; set; }
        public DocumentsModel SelectedDocument { get; set; }
        public delegate void OnCompleteEventHandler(DocumentsModel model);

        public event OnCompleteEventHandler OnComplete;

        public HoneywellBarcodeReader HoneywellBarcodeReader { get; set; }
        public ProductListModel(INavigation navigation, DocumentsModel selectedDocument, InterfaceTypes interfaceTypes, HoneywellBarcodeReader honeywellBarcodeReader)
        {
            Navigation = navigation;
            SelectedNumber = selectedDocument?.Number;
            SelectedDocument = selectedDocument;
            InterfaceTypes = interfaceTypes;

            Products = new ObservableCollection<ProductsModel>();

            HoneywellBarcodeReader = honeywellBarcodeReader;
            
            CompleteCommand = new Command(Complete);

            GetData();
        }

        private async void GetData()
        {
           Products.Clear();
            
            switch (InterfaceTypes)
            {
                case InterfaceTypes.Arrival:

                    var arrival =
                        RestContext.ExecuteScalar<List<ProductsModel>>($"ArrivalApi/GetArrivals/{SelectedDocument.Id}", null, Method.GET);

                    if (arrival.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", arrival.ErrorMessage, "ОК");
                        return;
                    }

                    arrival.Value.ForEach(s =>
                    {
                        Products.Add(s);
                    });
                    break;
                case InterfaceTypes.Shipment:
                    var shipment =
                        RestContext.ExecuteScalar<List<ProductsModel>>($"ShipmentApi/GetShipments/{SelectedDocument.Id}", null, Method.GET);

                    if (shipment.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", shipment.ErrorMessage, "ОК");
                        return;
                    }

                    shipment.Value.ForEach(s =>
                    {
                        Products.Add(s);
                    });

                    break;
                case InterfaceTypes.Inventory:
                    var inventory =
                        RestContext.ExecuteScalar<List<ProductsModel>>($"InventoryApi/GetInventories/{SelectedDocument.Id}", null, Method.GET);
                    
                    if (inventory.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", inventory.ErrorMessage, "ОК");
                        return;
                    }
                    
                    inventory.Value.ForEach(s =>
                    {
                        Products.Add(s);
                    });

                    break;
                case InterfaceTypes.WriteOff:
                    var writeOff =
                        RestContext.ExecuteScalar<List<ProductsModel>>($"WriteOffApi/GetWriteOffs/{SelectedDocument.Id}", null, Method.GET);

                    if (writeOff.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", writeOff.ErrorMessage, "ОК");
                        return;
                    }

                    writeOff.Value.ForEach(s =>
                    {
                        Products.Add(s);
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async void Complete()
        {
            switch (InterfaceTypes)
            {
                case InterfaceTypes.Arrival:
                    var updateArrival = RestContext.ExecuteScalar<OperationResult>($"ArrivalApi/СompleteArrival/{SelectedDocument.Id}", null, Method.GET);

                    if (updateArrival.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", updateArrival.ErrorMessage, "ОК");
                        return;
                    }

                    if (updateArrival.Value.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", updateArrival.Value.ErrorMessage, "ОК");
                        return;
                    }
                    break;
                case InterfaceTypes.Shipment:
                    var updateShipment = RestContext.ExecuteScalar<OperationResult>($"ShipmentApi/СompleteShipment/{SelectedDocument.Id}", null, Method.GET);

                    if (updateShipment.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", updateShipment.ErrorMessage, "ОК");
                        return;
                    }

                    if (updateShipment.Value.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", updateShipment.Value.ErrorMessage, "ОК");
                        return;
                    }
                    break;
                case InterfaceTypes.Inventory:
                    break;
                case InterfaceTypes.WriteOff:
                    var updateWriteOff = RestContext.ExecuteScalar<OperationResult>($"WriteOffApi/СompleteWriteOff/{SelectedDocument.Id}", null, Method.GET);

                    if (updateWriteOff.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", updateWriteOff.ErrorMessage, "ОК");
                        return;
                    }

                    if (updateWriteOff.Value.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", updateWriteOff.Value.ErrorMessage, "ОК");
                        return;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if(InterfaceTypes != InterfaceTypes.Inventory)
                OnComplete?.Invoke(SelectedDocument);

            await Navigation.PopAsync();
        }

        public async void ScannedDataCollected(object sender, BarcodeDataArgs e)
        {
            var barcode = new Barcode
            {
                Data = e.Data,
            };
            
          barcode.Data = RemoveUnicodeCharacters(barcode.Data);
            
            if (string.IsNullOrWhiteSpace(barcode.Data)) 
                return;
            
            var updateCodeModel = new UpdateCodeModel();
            
            if (InterfaceTypes == InterfaceTypes.Arrival)
            {
                var getArrivalProduct = RestContext.ExecuteScalar<UpdateCodeModel>($"ArrivalApi/GetArrivalByCode/{SelectedDocument.Id}/{barcode.Data}", null, Method.GET);
                if (getArrivalProduct.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getArrivalProduct.Value.ErrorMessage, "ОК");
                    return;
                }
            
                updateCodeModel = getArrivalProduct.Value;
            
                if (!updateCodeModel.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", updateCodeModel.ErrorMessage, "ОК");
                    return;
                }
            
                if (Products.All(s => s.ProductId != updateCodeModel.ProductId))
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Товар в списке не найден", "ОК");
                    return;
                }
                
            } else if (InterfaceTypes == InterfaceTypes.Shipment)
            {
                var getShipmentProduct = RestContext.ExecuteScalar<UpdateCodeModel>($"ShipmentApi/GetShipmentByCode/{SelectedDocument.Id}/{barcode.Data}", null, Method.GET);
                if (getShipmentProduct.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getShipmentProduct.Value.ErrorMessage, "ОК");
                    return;
                }
            
                updateCodeModel = getShipmentProduct.Value;
            
                if (!updateCodeModel.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", updateCodeModel.ErrorMessage, "ОК");
                    return;
                }
            
                if (Products.All(s => s.ProductId != updateCodeModel.ProductId))
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Товар в списке не найден", "ОК");
                    return;
                }
            }
            else if (InterfaceTypes == InterfaceTypes.WriteOff)
            {
                var getWriteOffProduct = 
                    RestContext.ExecuteScalar<UpdateCodeModel>($"WriteOffApi/GetWriteOffByCode/{SelectedDocument.Id}/{barcode.Data}", null, Method.GET);
                if (getWriteOffProduct.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getWriteOffProduct.Value.ErrorMessage, "ОК");
                    return;
                }
            
                updateCodeModel = getWriteOffProduct.Value;
            
                if (!updateCodeModel.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", updateCodeModel.ErrorMessage, "ОК");
                    return;
                }
            }
            else
            {
                var getInventoryProduct = 
                    RestContext.ExecuteScalar<UpdateCodeModel>($"InventoryApi/GetInventoryByCode/{SelectedDocument.Id}/{RestContext.UserModel.WarehouseId}/{barcode.Data}", null, Method.GET);
                if (getInventoryProduct.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getInventoryProduct.Value.ErrorMessage, "ОК");
                    return;
                }
            
                updateCodeModel = getInventoryProduct.Value;
            
                if (!updateCodeModel.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", updateCodeModel.ErrorMessage, "ОК");
                    return;
                }
            
                if (Products.All(s => s.ProductId != updateCodeModel.ProductId))
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Товар в списке не найден", "ОК");
                    return;
                }
            }
            
            updateCodeModel.ProductName =
                InterfaceTypes == InterfaceTypes.WriteOff ? updateCodeModel.ProductName :
                Products.First(s => s.ProductId == updateCodeModel.ProductId).ProductName;
            updateCodeModel.Sscc = barcode.Data;
            updateCodeModel.ReportId = SelectedDocument.Id;
            
            var productDetail = new ProductDetailsView(Navigation, InterfaceTypes, updateCodeModel);
            productDetail.Model.OnAccept += OnAccept;
            try
            {
                mUIContext.Post(_ =>
                {
                    Navigation.PushAsync(productDetail);    
                }, null);
                
            }
            catch (Exception exception)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", exception.Message, "ОК");
            }
        }

        private void OnAccept(UpdateCodeModel model)
        {
            GetData();
        }

        private string RemoveUnicodeCharacters(string value)
        {
            _unicodeCharacters.ForEach(s =>
            {
                value = value.Replace(s, string.Empty);
            });
            return value;
        }
    }
}
