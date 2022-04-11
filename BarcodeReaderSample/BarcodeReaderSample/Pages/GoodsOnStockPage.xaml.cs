using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeReaderSample.PageModel;
using DevExpress.XamarinForms.DataForm;
using DevExpress.XamarinForms.DataGrid;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GoodsOnStockPage : ContentPage
	{
		public GoodsOnStockPage(INavigation navigation)
		{
			InitializeComponent();

            BindingContext = new GoodsOnStockPageViewModel(navigation);
        }
        protected override void OnAppearing()
        {
            var viewModel = (GoodsOnStockPageViewModel)BindingContext;

            if (viewModel != null)
                Task.Run(viewModel.GetGoodsOnStock);

            base.OnAppearing();
        }
    }
}