using System;
using BarcodeReaderSample.Interface;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrdersPage : ContentPage
	{
		public OrdersPage (INavigation navigation, HoneywellBarcodeReader scanner, Guid supplierId, IDbService dbService)
		{
			InitializeComponent();
            BindingContext = new OrdersPageViewModel(navigation, scanner, supplierId, dbService);
		}
	}
}