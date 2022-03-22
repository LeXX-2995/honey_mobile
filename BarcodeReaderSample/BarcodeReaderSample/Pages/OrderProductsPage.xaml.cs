using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeReaderSample.Interface;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderProductsPage : ContentPage
	{
		public OrderProductsPage(INavigation navigation, HoneywellBarcodeReader scanner, IDbService dbService, Guid orderId)
		{
			InitializeComponent();

            BindingContext = new OrderProductsPageViewModel(navigation, scanner, dbService, orderId);
		}

        protected override bool OnBackButtonPressed()
        {
            var viewModel = (OrderProductsPageViewModel)BindingContext;

            viewModel?.Scanner.EnableScanner(false);
            if (viewModel != null) 
                viewModel.Scanner.OnBarcodeRead -= viewModel.ScannerOnBarcodeRead;

            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            var viewModel = (OrderProductsPageViewModel)BindingContext;

            viewModel?.Scanner.EnableScanner(false);
            if (viewModel != null) 
                viewModel.Scanner.OnBarcodeRead -= viewModel.ScannerOnBarcodeRead;

            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            var viewModel = (OrderProductsPageViewModel)BindingContext;

            viewModel?.Scanner.EnableScanner(true);
            if (viewModel != null) 
                viewModel.Scanner.OnBarcodeRead += viewModel.ScannerOnBarcodeRead;

            if(viewModel != null)
                Task.Run(viewModel.GetOrderDetails);

            base.OnAppearing();
        }
	}
}