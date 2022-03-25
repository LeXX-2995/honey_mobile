using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BarcodeReaderSample.Interface;
using TraceIQ.Expeditor.Models;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;

namespace BarcodeReaderSample.PageModel
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private string _url;

        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        private string _port;

        public string Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyChanged(nameof(Port));
            }
        }

        private readonly SynchronizationContext _mUiContext = SynchronizationContext.Current;
        public Command SaveCommand { get; set; }    
        public SettingsPageViewModel(INavigation navigation, IDbService dbService)
        {
            DbService = dbService;
            Navigation = navigation;

            SaveCommand = new Command(Save);
        }

        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Укажите адрес сервера", "ОК");
                return;
            }

            if (string.IsNullOrWhiteSpace(Port))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Укажите порт", "ОК");
                return;
            }

            var isPortParseable = int.TryParse(Port, out var port);
            if (!isPortParseable)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Неверный формат порта", "ОК");
                return;
            }

            var saveSetting = DbService.AddSetting(Url, port);
            if (saveSetting.Result != OperationStatus.Success)
                await Application.Current.MainPage.DisplayAlert("Ошибка", saveSetting.ErrorMessage, "ОК");

            await Navigation.PopAsync(true);
        }

        public async void GetSetting()
        {
            var getSetting = DbService.GetSetting();
            if (getSetting.Result != OperationStatus.Success)
            {
                _mUiContext.Post(async s =>
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getSetting.ErrorMessage, "ОК");
                }, null);

                return;
            }

            _mUiContext.Post(s =>
            {
                Url = getSetting.Value.Url;
                Port = getSetting.Value.Port.ToString();
            }, null);
        }
    }
}
