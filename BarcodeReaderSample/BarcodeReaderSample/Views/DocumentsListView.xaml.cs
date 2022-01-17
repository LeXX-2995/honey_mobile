using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NobelXamarin.Models;
using NobelXamarin.PageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocumentsListView : ContentPage
	{
		public DocumentsListView(INavigation navigation, InterfaceTypes interfaceTypes, HoneywellBarcodeReader honeywellBarcodeReader)
		{
			InitializeComponent();
            BindingContext = new DocumentsListModel(navigation, interfaceTypes, honeywellBarcodeReader);
		}
	}
}