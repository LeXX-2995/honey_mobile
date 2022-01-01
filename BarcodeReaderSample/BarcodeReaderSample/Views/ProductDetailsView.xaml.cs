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
	public partial class ProductDetailsView : ContentPage
	{
		public ProductDetailsModel Model;
		public ProductDetailsView(INavigation navigation, InterfaceTypes interfaceTypes, UpdateCodeModel model)
		{
			InitializeComponent();

			Model = new ProductDetailsModel(navigation, interfaceTypes, model);

			BindingContext = Model;
		}
	}
}