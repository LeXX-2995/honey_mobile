using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(INavigation navigation, IDbService dbService)
        {
            InitializeComponent();

            BindingContext = new SettingsPageViewModel(navigation, dbService);
        }

        protected override bool OnBackButtonPressed()
        {

            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            var viewModel = (SettingsPageViewModel)BindingContext;

            if (viewModel != null)
                Task.Run(viewModel.GetSetting);

            base.OnAppearing();
        }
    }
}