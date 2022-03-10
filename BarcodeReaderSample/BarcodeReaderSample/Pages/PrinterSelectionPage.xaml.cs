using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.SatoMpXamarinSDK;
using Plugin.SatoMpXamarinSDK.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrinterSelectionPage : ContentPage
	{
        public PrinterSelectionPage()
        {
            InitializeComponent();
        }

        public async void GetConnectionInfo(int selectedIndex)
        {
            IPrinterLookup lookup = SPVPrinterLookupUtil.Current;
            var interfaceType = InterfaceType.BLUETOOTH;
            if (selectedIndex != 0)
                interfaceType = InterfaceType.WIFI;
            
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
            var result = await lookup.refreshDeivcesList((int)interfaceType, 1 /*second*/);
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;

            if (result == (int)ResultCode.SUCCESS)
            {
                var deviceList = lookup.getDeviceList((int)interfaceType);
                lstView.ItemsSource = deviceList;
            }
            else
            {
                lstView.ItemsSource = null;
                await DisplayAlert("No Items", "No Device found", "OK");
            }
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerCtl != null && PickerCtl.SelectedIndex <= PickerCtl.Items.Count)
                GetConnectionInfo(PickerCtl.SelectedIndex);
        }

        async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var info = e.SelectedItem as PrinterConnectionInformation;
            if (info != null)
            {
                var prevPage = BindingContext as Page;
                prevPage.BindingContext = info;
            }
            await Navigation.PopAsync();
        }
    }
}