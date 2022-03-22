using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeReaderSample.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SyncPage : ContentPage
	{
		public SyncPage (INavigation navigation)
		{
			InitializeComponent();
            BindingContext = new SyncPageViewModel(navigation);
        }
	}
}