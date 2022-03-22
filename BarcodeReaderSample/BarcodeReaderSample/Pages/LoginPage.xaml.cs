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
    public partial class LoginPage : ContentPage
    {
        public LoginPage(INavigation navigation)
        {
            InitializeComponent();

            BindingContext = new LoginPageViewModel(navigation);
        }
    }
}