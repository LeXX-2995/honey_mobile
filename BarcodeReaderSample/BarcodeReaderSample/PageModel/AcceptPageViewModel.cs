using System;
using System.Collections.Generic;
using System.Text;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Interface;
using TraceIQ.Expeditor.Models;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;

namespace BarcodeReaderSample.PageModel
{
    public class AcceptPageViewModel : BaseViewModel
    {
        private int _cash;

        public int Cash
        {
            get => _cash;
            set
            {
                _cash = value;
                OnPropertyChanged(nameof(Cash));
            }
        }

        private int _terminal;

        public int Terminal
        {
            get => _terminal;
            set
            {
                _terminal = value;
                OnPropertyChanged(nameof(Terminal));
            }
        }

        private int _total;

        public int Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }


        public Command ConfirmCommand { get; set; }

        private readonly Guid _orderId;
        public AcceptPageViewModel(INavigation navigation, IDbService dbService, Guid orderId)
        {
            _orderId = orderId;
            Navigation = navigation;
            DbService = dbService;
            BaseApiService = new BaseApiService();
            ConfirmCommand = new Command(Confirm);

            var getTotal = DbService.GetOrderTotal(orderId);
            if (getTotal.Result != OperationStatus.Success)
                Total = getTotal.Value;
        }

        private async void Confirm()
        {
            if (Cash + Terminal != Total)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Сумма не верная", "ОК");
                return;
            }

            var getDataMatrixCode = DbService.GetOrderDataMatrix(_orderId);
            if(getDataMatrixCode.Result != OperationStatus.Success)
                await Application.Current.MainPage.DisplayAlert("Ошибка", getDataMatrixCode.ErrorMessage, "ОК");
            else
            {
                var sendAccept = BaseApiService.SendOrderConfirmation(getDataMatrixCode.Value, _orderId, Cash, Terminal);
                if(sendAccept.Result != OperationStatus.Success)
                    await Application.Current.MainPage.DisplayAlert("Ошибка", sendAccept.Value.ErrorMessage, "ОК");
                else
                {
                    var updateOrder = DbService.UpdateOrderWaiting(_orderId);
                    if (updateOrder.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", sendAccept.Value.ErrorMessage, "ОК");
                        return;
                    }
                    await Navigation.PopAsync();
                }
            }
        }
    }
}
