using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Graphics;
using NobelXamarin.Models;
using NobelXamarin.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductListView : ContentPage
	{
		public ProductListModel Model { get; set; }
		public ProductListView (INavigation navigation, DocumentsModel selectedDocument, InterfaceTypes interfaceTypes, HoneywellBarcodeReader honeywellBarcodeReader)
		{
			InitializeComponent();

			Model = new ProductListModel(navigation, selectedDocument, interfaceTypes, honeywellBarcodeReader);

			BindingContext = Model;
		}
		
		protected override bool OnBackButtonPressed()
		{
			var viewModel = (ProductListModel) BindingContext;

			viewModel.HoneywellBarcodeReader.EnableScanner(false);
			viewModel.HoneywellBarcodeReader.OnBarcodeRead -= viewModel.ScannedDataCollected;

			return base.OnBackButtonPressed();
		}

		protected override void OnDisappearing()
		{
			var viewModel = (ProductListModel)BindingContext;

			viewModel.HoneywellBarcodeReader.EnableScanner(false);
			viewModel.HoneywellBarcodeReader.OnBarcodeRead -= viewModel.ScannedDataCollected;

			base.OnDisappearing();
		}

		protected override void OnAppearing()
		{
			var viewModel = (ProductListModel)BindingContext;

			viewModel.HoneywellBarcodeReader.EnableScanner(true);
			viewModel.HoneywellBarcodeReader.OnBarcodeRead += viewModel.ScannedDataCollected;

			base.OnAppearing();
		}
	}
}