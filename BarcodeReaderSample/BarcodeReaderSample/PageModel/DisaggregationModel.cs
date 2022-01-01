using System;
using System.Collections.Generic;
using System.Text;
using BarcodeReaderSample.API;
using FreshMvvm;
using NobelXamarin.Models;
using RestSharp;
using Xamarin.Forms;

namespace NobelXamarin.PageModel
{
    public class DisaggregationModel : BaseViewModel
    {
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

        public Command DisAggregateCommand { get; set; }
        private readonly INavigation _navigation;
        public DisaggregationModel(INavigation navigation)
        {
            _navigation = navigation;

            DisAggregateCommand = new Command(DisAggregate);
        }

        private async void DisAggregate()
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Вы не отсканировали SSCC код","ОК");
                return;
            }

            var sendDisAggregate =
                RestContext.ExecuteScalar<OperationResult>($"ArrivalApi/DisAggregate/{Code}",
                    null, Method.GET);
            if (sendDisAggregate.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", sendDisAggregate.ErrorMessage, "ОК");
                return;
            }

            if (sendDisAggregate.Value.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", sendDisAggregate.Value.ErrorMessage, "ОК");
                return;
            }

            await _navigation.PopAsync();
        }

        public void ScannedDataCollected(object sender, StatusEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
                return;

            Code = e.Data;
        }
    }
}
