using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using BarcodeReaderSample.Database;
using Entities;
using NobelXamarin.Helpers;
using TraceIQ.Expeditor.Models;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;

namespace BarcodeReaderSample.PageModel
{
    public class GoodsOnStockPageViewModel : BaseViewModel
    {
        public ObservableCollection<OrderDetailsModel> GoodsOnStock { get; set; }
        private readonly SynchronizationContext _mUiContext = SynchronizationContext.Current;
        public GoodsOnStockPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            DbService = new DbService();
        }

        public async void GetGoodsOnStock()
        {
            _mUiContext.Post(s =>
            {
                GoodsOnStock.Clear();
            }, null);

            var getGoodsOnStock = DbService.GetGoodsOnStock();
            if (getGoodsOnStock.Result != OperationStatus.Success)
            {
                async void SendOrPostCallback(object o)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getGoodsOnStock.ErrorMessage, "ОК");
                }

                _mUiContext.Post(SendOrPostCallback, null);
                return;
            }

            _mUiContext.Post(s =>
            {
                getGoodsOnStock.Value.ForEach(c =>
                {
                    c.UnitOfMeasurementText = c.UnitOfMeasurement.GetDisplayName();
                    GoodsOnStock.Add(c);
                });

            }, null);
        }
    }
}
