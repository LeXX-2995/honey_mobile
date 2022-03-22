using System;
using System.Collections.Generic;
using System.Text;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Database;
using BarcodeReaderSample.Interface;
using RestSharp;
using TraceIQ.Expeditor.API;
using TraceIQ.Expeditor.Models;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;

namespace BarcodeReaderSample.PageModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string _login;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));

            }
        }

        public Command LoginCommand { get; set; }
        private readonly INavigation _navigation;

        public LoginPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            LoginCommand = new Command(LoginToSystem);
            BaseApiService = new BaseApiService();
            DbService = new DbService();
        }


        private async void LoginToSystem()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не указан Логин", "ОК");
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не указан Пароль", "ОК");
                return;
            }

            var loginLocal = DbService.Login(Login, Password);
            if (loginLocal.Result != OperationStatus.Success)
            {
                if (string.IsNullOrWhiteSpace(loginLocal.ErrorMessage))
                {
                    var login = BaseApiService.Authorize(Login, Password);
                    if (login.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", login.ErrorMessage, "ОК");
                        return;
                    }

                    var addUser = DbService.AddUser(login.Value);
                    if (addUser.Result != OperationStatus.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ошибка", addUser.ErrorMessage, "ОК");
                        return;
                    }

                    RestContext.User = login.Value;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", loginLocal.ErrorMessage, "ОК");
                    return;
                }
            }
            else
            {
                RestContext.User = loginLocal.Value;
            }

            await _navigation.PopAsync();
        }
    }
}
