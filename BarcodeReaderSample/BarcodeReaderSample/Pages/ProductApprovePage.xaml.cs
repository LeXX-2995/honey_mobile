using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Models;
using BarcodeReaderSample.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductApprovePage : ContentPage
	{
		public ProductApprovePage (ProductApproveModel productApproveModel, INavigation navigation, IDbService dbService)
		{
			InitializeComponent();

            BindingContext = new ProductApprovePageViewModel(productApproveModel, navigation, dbService);
		}


	}
}