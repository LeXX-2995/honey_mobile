using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientsPage : ContentPage
	{
		public ClientsPage(INavigation navigation, HoneywellBarcodeReader scanner)
		{
			InitializeComponent();

            BindingContext = new ClientPageViewModel(navigation, scanner);
		}
	}
}