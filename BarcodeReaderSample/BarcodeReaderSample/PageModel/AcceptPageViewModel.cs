using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Models;
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

            var getOrderDetails = DbService.GetOrderDetails(_orderId);
            if (getOrderDetails.Result != OperationStatus.Success)
            {
                Application.Current.MainPage.DisplayAlert("Ошибка", getOrderDetails.ErrorMessage, "ОК");
            }
            else
            {
                Total = (int)getOrderDetails.Value.Sum(
                    t => t.AssembledAmount * t.AggregationQuantity * t.Price);
            }
        }

        public void Recalculate(bool isCash = true)
        {
            if (isCash && Cash != 0)
            {
                Terminal = Total - Cash;
            }
            else if(Terminal != 0)
            {
                Cash = Total - Terminal;
            }
        }

        private async void Confirm()
        {
            if (Total == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Сумма нулевая", "ОК");
                return;
            }

            if (Cash + Terminal != Total)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Сумма не верная", "ОК");
                return;
            }

            var getDataMatrixCode = DbService.GetOrderDetailBill(_orderId);
            if(getDataMatrixCode.Result != OperationStatus.Success)
                await Application.Current.MainPage.DisplayAlert("Ошибка", getDataMatrixCode.ErrorMessage, "ОК");
            else
            {
                var model = new ConfirmOrderModel
                {
                    Cash = Cash,
                    Terminal = Terminal,
                    OrderId = _orderId,
                    ConfirmProductsModels = getDataMatrixCode.Value.Select(s => new ConfirmProductsModel
                    {
                        OrderDetailId = s.OrderDetailId,
                        Codes = s.Codes,
                        Amount = s.Quantity
                    }).ToList()
                };

                var sendAccept = BaseApiService.SendOrderConfirmation(model);
                if(sendAccept.Result != OperationStatus.Success)
                    await Application.Current.MainPage.DisplayAlert("Ошибка", sendAccept.Value.ErrorMessage, "ОК");
                else
                {
                    var updateOrder = DbService.UpdateOrderWaiting(_orderId, Cash, Terminal);
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
