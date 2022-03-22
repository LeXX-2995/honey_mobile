using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Models;
using NobelXamarin.Helpers;
using TraceIQ.Expeditor.Models;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;

namespace BarcodeReaderSample.PageModel
{
    public class ProductApprovePageViewModel : BaseViewModel
    {
        public readonly ProductApproveModel ProductApproveModel;
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

        private string _productName;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

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

        private string _code;

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }


        public Command AcceptCommand { get; set; }
        public Command DenyCommand { get; set; }
        public ProductApprovePageViewModel(ProductApproveModel productApproveModel, INavigation navigation, IDbService dbService)
        {
            ProductApproveModel = productApproveModel;
            Navigation = navigation;
            DbService = dbService;

            AcceptCommand = new Command(Accept);
            DenyCommand = new Command(Deny);

            IsAcceptVisible = !productApproveModel.IsChecked;
            IsDeleteVisible = productApproveModel.IsChecked;

            ProductName = productApproveModel.ProductName;
            UnitOfMeasurement = productApproveModel.UnitOfMeasurement.GetDisplayName();
            Code = productApproveModel.Code;
        }

        private async void Deny()
        {
            await Navigation.PopAsync();
        }

        private async void Accept()
        {
            var approve = DbService.ApproveOrderDetail(ProductApproveModel);
            if(approve.Result != OperationStatus.Success)
                await Application.Current.MainPage.DisplayAlert("Ошибка", approve.ErrorMessage, "ОК");
            else
                await Navigation.PopAsync();
        }
    }
}
