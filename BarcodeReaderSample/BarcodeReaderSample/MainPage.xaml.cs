using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Honeywell.AIDC.CrossPlatform;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Essentials;

namespace BarcodeReaderSample
{
    public partial class MainPage : ContentPage
    {
        public HoneywellBarcodeReader HoneywellBarcodeReader { get; }
        public MainPage()
        {
            InitializeComponent();

            HoneywellBarcodeReader = new HoneywellBarcodeReader();

            BindingContext = new MainPageViewModel(Navigation, HoneywellBarcodeReader);
        }
    }
}
