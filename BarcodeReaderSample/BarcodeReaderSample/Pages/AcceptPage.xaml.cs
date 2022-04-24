using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PropertyChangingEventArgs = Xamarin.Forms.PropertyChangingEventArgs;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AcceptPage : ContentPage
	{
		public AcceptPage (INavigation navigation, IDbService dbService, Guid orderId)
		{
			InitializeComponent ();

            BindingContext = new AcceptPageViewModel(navigation, dbService, orderId);
        }

        private void Cash_OnUnfocused(object sender, FocusEventArgs e)
        {
            var viewModel = (AcceptPageViewModel) BindingContext;
            viewModel?.Recalculate();
        }

        private void Terminal_OnUnfocused(object sender, FocusEventArgs e)
        {
            var viewModel = (AcceptPageViewModel)BindingContext;
            viewModel?.Recalculate(false);
        }

        private void Cash_OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            var viewModel = (AcceptPageViewModel)BindingContext;
            viewModel?.Recalculate();
        }

        private void Terminal_OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            var viewModel = (AcceptPageViewModel)BindingContext;
            viewModel?.Recalculate(false);
        }
    }
}