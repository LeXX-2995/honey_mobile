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
	public partial class AcceptPage : ContentPage
	{
		public AcceptPage (INavigation navigation, IDbService dbService, Guid orderId)
		{
			InitializeComponent ();

            BindingContext = new AcceptPageViewModel(navigation, dbService, orderId);
        }
	}
}