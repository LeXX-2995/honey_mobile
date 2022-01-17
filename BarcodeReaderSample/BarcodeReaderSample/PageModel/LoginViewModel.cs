using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;
using BarcodeReaderSample.API;
using NobelXamarin.Models;
using RestSharp;
using Xamarin.Forms;

namespace NobelXamarin.PageModel
{
    public class LoginViewModel : BaseViewModel
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
        
        public Command LoginCommand { get; set; }
        private readonly INavigation _navigation;
        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            LoginCommand = new Command(LoginToSystem);

#if DEBUG
            Login = "Admin";
            Password = "123";
            Port = "8800";
#endif
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

            if (string.IsNullOrWhiteSpace(Port))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не указан Порт", "ОК");
                return;
            }

            RestContext.Url = RestContext.Ip + Port + RestContext.ApiResource;
            
            var login =
                RestContext.ExecuteScalar<OperationResult<UserModel>>($"AccountApi/Login/{Login}/{Password}",
                    null, Method.GET);

            if (login.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", login.ErrorMessage, "ОК");
                return;
            }

            if (login.Value.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", login.Value.ErrorMessage, "ОК");
                return;
            }

            RestContext.UserModel = login.Value.Value;

            await _navigation.PopAsync();
        }
    }
}
