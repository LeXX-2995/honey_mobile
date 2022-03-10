using System.Collections.ObjectModel;
using BarcodeReaderSample;
using TraceIQ.Expeditor.Models;
using Xamarin.Forms;

namespace TraceIQ.Expeditor.PageModels
{
    public class OrderProductsPageViewModel : BaseViewModel
    {
        public ObservableCollection<ProductsModel> Products { get; set; }
        public OrderProductsPageViewModel(INavigation navigation, HoneywellBarcodeReader scanner)
        {
            Scanner = scanner;
            Navigation = navigation;

            Products = new ObservableCollection<ProductsModel>
            {
                new ProductsModel
                {
                    ProductName = "Alcohol",
                    Amount = 12,
                    Price = 12000,
                    AssembledAmount = 10
                },
                new ProductsModel
                {
                    ProductName = "Beer",
                    Amount = 7,
                    Price = 7536,
                    AssembledAmount = 3
                },
                new ProductsModel
                {
                    ProductName = "Wine",
                    Amount = 9,
                    Price = 8960,
                    AssembledAmount = 9
                },
                new ProductsModel
                {
                    ProductName = "Whiskey",
                    Amount = 18,
                    Price = 4800,
                    AssembledAmount = 14
                },
            };

        }
    }
}
