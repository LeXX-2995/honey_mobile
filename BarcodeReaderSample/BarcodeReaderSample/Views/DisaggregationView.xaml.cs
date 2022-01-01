using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NobelXamarin.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DisaggregationView : ContentPage
	{
		public DisaggregationView(INavigation navigation)
		{
			InitializeComponent();

            BindingContext = new DisaggregationModel(navigation);
		}
	}
}