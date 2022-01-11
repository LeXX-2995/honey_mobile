using System;
using System.Collections.Generic;
using System.Text;
using BarcodeReaderSample.API;
using NobelXamarin.Helpers;
using NobelXamarin.Models;
using RestSharp;
using Xamarin.Forms;

namespace NobelXamarin.PageModel
{
    public class ProductDetailsModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _serialShippingContainerCode;

        public string SerialShippingContainerCode
        {
            get => _serialShippingContainerCode;
            set
            {
                _serialShippingContainerCode = value;
                OnPropertyChanged(nameof(SerialShippingContainerCode));
            }
        }

        private bool _isAcceptVisible;

        public bool IsAcceptVisible
        {
            get => _isAcceptVisible;
            set
            {
                _isAcceptVisible = value;
                OnPropertyChanged(nameof(IsAcceptVisible));
            }
        }
        
        private bool _isDeleteVisible;

        public bool IsDeleteVisible
        {
            get => _isDeleteVisible;
            set
            {
                _isDeleteVisible = value;
                OnPropertyChanged(nameof(IsDeleteVisible));
            }
        }
        
        public delegate void OnAcceptEventHandler(UpdateCodeModel model);

        public event OnAcceptEventHandler OnAccept;

        private string _unitOfMeasurement;

        public string UnitOfMeasurement
        {
            get => _unitOfMeasurement;
            set
            {
                _unitOfMeasurement = value;
                OnPropertyChanged(nameof(UnitOfMeasurement));
            }
        }


        public Command AcceptCommand { get; set; }
        public Command DenyCommand { get; set; }
        public UpdateCodeModel UpdateCodeModel { get; set; }
        public INavigation Navigation { get; }
        
        private readonly InterfaceTypes _interfaceTypes;
        public ProductDetailsModel(INavigation navigation, InterfaceTypes interfaceTypes, UpdateCodeModel model)
        {
            AcceptCommand = new Command(Accept);
            DenyCommand = new Command(Deny);
            Navigation = navigation;

            UpdateCodeModel = model;

            Name = model.ProductName;

            SerialShippingContainerCode = model.Sscc;
            UnitOfMeasurement = model.UnitOfMeasurement.GetDisplayName();

            IsAcceptVisible = !model.IsItemChecked;
            IsDeleteVisible = model.IsItemChecked;
            
            _interfaceTypes = interfaceTypes;
        }

        private async void Deny()
        {
            await Navigation.PopAsync();
        }

        private async void Accept()
        {
            try
            {
                switch (_interfaceTypes)
                {
                    case InterfaceTypes.Arrival:
                        var sendArrival =
                            RestContext.ExecuteScalar<OperationResult<int>>(
                                $"ArrivalApi/ApproveArrival/{UpdateCodeModel.ItemId}/{UpdateCodeModel.ArrivalId}/{UpdateCodeModel.UnitOfMeasurement}/{UpdateCodeModel.IsItemChecked}",
                                null, Method.GET);
                        if (sendArrival.Result != OperationStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ошибка", sendArrival.ErrorMessage, "ОК");
                            return;
                        }
                        
                        if(sendArrival.Value.Result != OperationStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ошибка", sendArrival.Value.ErrorMessage, "ОК");
                            return;
                        }

                        UpdateCodeModel.AssembledAmount = sendArrival.Value.Value;
                        break;
                    case InterfaceTypes.Shipment:
                        var sendShipment =
                            RestContext.ExecuteScalar<OperationResult<int>>($"ShipmentApi/ApproveShipment/{UpdateCodeModel.ItemId}/{UpdateCodeModel.ShipmentId}/{UpdateCodeModel.UnitOfMeasurement}/{UpdateCodeModel.IsItemChecked}",
                                null, Method.GET);
                        if (sendShipment.Result != OperationStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ошибка", sendShipment.ErrorMessage, "ОК");
                            return;
                        }

                        if (sendShipment.Value.Result != OperationStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ошибка", sendShipment.Value.ErrorMessage, "ОК");
                            return;
                        }

                        UpdateCodeModel.AssembledAmount = sendShipment.Value.Value;
                        break;
                    case InterfaceTypes.Inventory:
                        var inventoryId = UpdateCodeModel.InventoryId ?? Guid.Empty;
                        var sendInventory =
                            RestContext.ExecuteScalar<OperationResult<int>>($"InventoryApi/ApproveInventory/{UpdateCodeModel.ItemId}/{UpdateCodeModel.ReportId}/{inventoryId}/{UpdateCodeModel.UnitOfMeasurement}/{UpdateCodeModel.IsItemChecked}",
                                null, Method.GET);
                        if (sendInventory.Result != OperationStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ошибка", sendInventory.ErrorMessage, "ОК");
                            return;
                        }

                        if (sendInventory.Value.Result != OperationStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ошибка", sendInventory.Value.ErrorMessage, "ОК");
                            return;
                        }
                        
                        UpdateCodeModel.AssembledAmount = sendInventory.Value.Value;
                        break;
                    case InterfaceTypes.WriteOff:
                        var writeOffId = UpdateCodeModel.WriteOffId ?? Guid.Empty;
                        var sendWriteOff =
                            RestContext.ExecuteScalar<OperationResult<int>>($"WriteOffApi/ApproveWriteOff/{UpdateCodeModel.ItemId}/{UpdateCodeModel.UnitOfMeasurement}/{UpdateCodeModel.ReportId}/{writeOffId}",
                                null, Method.GET);
                        if (sendWriteOff.Result != OperationStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ошибка", sendWriteOff.ErrorMessage, "ОК");
                            return;
                        }

                        if (sendWriteOff.Value.Result != OperationStatus.Success)
                        {
                            await Application.Current.MainPage.DisplayAlert("Ошибка", sendWriteOff.Value.ErrorMessage, "ОК");
                            return;
                        }
                        
                        UpdateCodeModel.AssembledAmount = sendWriteOff.Value.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                OnAccept?.Invoke(UpdateCodeModel);
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", e.Message, "ОК");
            }
            
        }
    }
}
