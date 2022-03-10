using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BarcodeReaderSample.Interface;
using Plugin.SatoMpXamarinSDK;
using Plugin.SatoMpXamarinSDK.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LabelPrinterPage : ContentPage
	{
        private ILabelPrinterDevice _printer = null;
        private PrinterConnectionInformation _connectionInfo = null;
        private static SemaphoreSlim _printSemaphore = new SemaphoreSlim(1, 1);

        public LabelPrinterPage()
        {
            InitializeComponent();
        }
        void PrinterStatusEvent(uint status)
        {
            // Any view interaction or any UI interaction should always be done in the main thread.
            Device.BeginInvokeOnMainThread(() =>
            {
                lblStatus.Text = "";
                if (status == (uint)LabelPrinterStatus.IDLE)
                {
                    lblStatus.Text = "NORMAL ";
                    return;
                }
                if ((status & (uint)LabelPrinterStatus.PAPER_EMPTY) != 0) lblStatus.Text += " PAPER EMPTY ";                  // Paper Empty
                if ((status & (uint)LabelPrinterStatus.COVER_OPEN) != 0) lblStatus.Text += " COVER OPEN ";                   // Cover Open
                if ((status & (uint)LabelPrinterStatus.TPH_OVERHEAT) != 0) lblStatus.Text += " TPH OVERHEAT ";                 // Thermal Head Overheat
                if ((status & (uint)LabelPrinterStatus.GAP_ERROR) != 0) lblStatus.Text += " GAP ERROR ";                    // Gap Detection Error
                if ((status & (uint)LabelPrinterStatus.MOTOR_OVERHEAT) != 0) lblStatus.Text += " MOTOR OVERHEAT ";               // Motor Overheat
                if ((status & (uint)LabelPrinterStatus.BOARD_OVERHEAT) != 0) lblStatus.Text += " BOARD OVERHEAT ";               // Board Overheat
                if ((status & (uint)LabelPrinterStatus.BUILDING_LABEL_TO_BE_PRINTED) != 0) lblStatus.Text += " BUIDING LABEL ";                // On building label to be printed in image buffer
                if ((status & (uint)LabelPrinterStatus.PRINTING_LABEL) != 0) lblStatus.Text += " PRINTING ";                     // On printing label
                if ((status & (uint)LabelPrinterStatus.ISSUED_LABEL_ISPAUSED) != 0) lblStatus.Text += " PAUSED IN PEELER ";             // label is paused in peeler unit
                if ((status & (uint)LabelPrinterStatus.WAIT_FOR_PAPER_TO_BE_TAKEN) != 0) lblStatus.Text += " WAIT FOR LABEL TO BE TAKEN ";   // WAIT FOR LABEL TO BE TAKEN
            });
        }
        async void OnPrintLabelClicked(object sender, EventArgs e)
        {
            _printer = await OpenPrinterService(_connectionInfo) as ILabelPrinterDevice;
            if (_printer == null)
                return;

            try
            {
                await _printSemaphore.WaitAsync();

                await _printer.setTransaction((int)TransactionMode.TRANSACTION_IN);
                await _printer.setDensity(14);
                await _printer.setSpeed(3 /*4 ips*/);
                await _printer.setMargin(0, 0);
                await _printer.setOrientation((char)LabelOrientation.TOP_TO_BOTTOM);

                // 203 DPI : 1 mm is about 7.99 dots 
                // Max width of PV3 is 576 dots, Max width of PV4 : 832 dots
                const int MAX_WIDTH = 576;
                await _printer.setWidth(MAX_WIDTH);

                int xPos = 16;
                int yPos = 32;

                await _printer.setLength(1000, 0, 'C', 0);

                // Bitmap Font
                await _printer.setCharacterset((int)LabelCodePage.WPC1252, (int)ICS.USA);
                await _printer.drawTextDeviceFont("Bitmap Font: Hello", MAX_WIDTH - 24, yPos, (char)LabelBitmapFont.DEVICE_FONT_8PT, 1, 1, 0, 0, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;
                await _printer.drawTextDeviceFont("Bitmap Font: 안녕하세요", MAX_WIDTH - 24, yPos, (char)LabelBitmapFont.DEVICE_FONT_KOREAN3, 1, 1, 0, 0, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;
                await _printer.drawTextDeviceFont("Bitmap Font: こんにちは", MAX_WIDTH - 24, yPos, (char)LabelBitmapFont.DEVICE_FONT_SHIFT_JIS, 1, 1, 0, 0, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;
                await _printer.drawTextDeviceFont("Bitmap Font: 中国 你好", xPos, yPos, (char)LabelBitmapFont.DEVICE_FONT_GB2312, 1, 1, 0, 0, false, false, false, (int)LabelAlignment.LEFT);
                yPos += 35;
                await _printer.drawTextDeviceFont("Bitmap Font: 中國 你好", xPos, yPos, (char)LabelBitmapFont.DEVICE_FONT_BIG5, 1, 1, 0, 0, false, false, false, (int)LabelAlignment.LEFT);
                yPos += 35;

                // Vector Font
                await _printer.drawTextVectorFont("Vector Font: Hello", MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;
                await _printer.drawTextVectorFont("Vector Font: 안녕하세요", MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_KS5601, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;
                await _printer.drawTextVectorFont("Vector Font: こんにちは", MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_SHIFT_JIS, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;
                await _printer.drawTextVectorFont("Vector Font: 中国 你好", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_GB2312, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);
                yPos += 35;
                await _printer.drawTextVectorFont("Vector Font: 中國 你好", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_BIG5, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);
                yPos += 35;
                // 1d-Barcode
                await _printer.drawBarcode1D("1234567890", xPos, yPos, (int)Label1dCodeType.CODE128, 2, 4, 50, (int)LabelHRI.TEXTBELOW, 0, 0);
                yPos += 90;
                // PDF417
                await _printer.drawBarcodePDF417("1234567890", xPos, yPos, 30, 5, 0, 0, false, (int)LabelPDF41StartPosition.ORIGIN_POINT_UPPER, 3, 20, 0);
                yPos += 100;
                // QR Code
                await _printer.drawBarcodeQRCode("0123456789", xPos, yPos, 3, (int)LabelQRCodeModel.MODEL_1, (int)LabelQRCodeECL.ECCLEVEL_L, 0);
                yPos += 90;
                // Graphic Image
                await _printer.drawImage(await DependencyService.Get<IPlatformInfo>().GetImgResourceAsync(),
                                        xPos,
                                        yPos,
                                        200,                // Image Width
                                        20,                 // brightness
                                        true,               // Image Dithering
                                        true);              // Image Compress
                yPos += 50;
                await _printer.drawBlock(8, 8, MAX_WIDTH - 8, yPos, (char)LabelBlock.BOX, 2);
                // 'printBuffer' method must be called at the end.
                await _printer.printBuffer(1);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");
            }
            finally
            {
                // Printer starts printing by calling "setTransaction" function with "TRANSACTION_OUT"
                await _printer.setTransaction((int)TransactionMode.TRANSACTION_OUT);
                // If there's nothing to do with the printer, call "closeService" method to disconnect the communication between Host and Printer.
                _printSemaphore.Release();
            }
        }
        protected override void OnDisappearing()
        {
            if (_printer != null)
                OnDeviceCloseClicked(this, null);
        }
        protected override void OnBindingContextChanged()
        {
            if (BindingContext == null)
                return;

            var connectionInfo = BindingContext as PrinterConnectionInformation;

            if (connectionInfo != null)
            {
                _connectionInfo = connectionInfo;

                switch (_connectionInfo.IntefaceType)
                {
                    case InterfaceType.BLUETOOTH:
                    case InterfaceType.WIFI:
                    case InterfaceType.ETHERNET:
                        lblAddress.Text = connectionInfo.Address;
                        btnOpenService.IsEnabled = true;
                        break;
                    default:
                        return;
                }
            }
        }
        async void OnDeviceOpenClicked(object sender, EventArgs e)
        {
            // Prepares to communicate with the printer 
            _printer = await OpenPrinterService(_connectionInfo) as ILabelPrinterDevice;
            if (_printer != null)
            {
                btnOpenService.IsEnabled = false;
                btnCloseService.IsEnabled = true;
            }
        }

        async void OnDeviceCloseClicked(object sender, EventArgs e)
        {
            await _printSemaphore.WaitAsync();

            if (_printer != null)
            {
                // If there's nothing to do with the printer, call "closeService" method to disconnect the communication between Host and Printer.
                await _printer.closeService();
                btnCloseService.IsEnabled = false;
                btnOpenService.IsEnabled = true;
                _printer = null;
            }

            _printSemaphore.Release();
        }

        async void OnDeviceSelectionClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrinterSelectionPage() { BindingContext = this });
        }

        async void OnCheckPrinterStatusClicked(object sender, EventArgs e)
        {
            // Prepares to communicate with the printer 
            _printer = await OpenPrinterService(_connectionInfo) as ILabelPrinterDevice;
            if (_printer == null)
                return;

            try
            {
                await _printSemaphore.WaitAsync();
                PrinterStatusEvent(await _printer.checkPrinterStatus());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");
            }
            finally
            {
                _printSemaphore.Release();
            }
        }

        async Task<IPrinterDevice> OpenPrinterService(PrinterConnectionInformation connectionInfo)
        {
            if (connectionInfo == null)
                return null;
            if (_printer != null)
                return _printer;

            _printer = SPVPrinterDeviceFactory.Current.createDevice(DeviceType.LABEL_PRINTER) as ILabelPrinterDevice;
            switch (connectionInfo.IntefaceType)
            {
                case InterfaceType.BLUETOOTH:
                case InterfaceType.WIFI:
                case InterfaceType.ETHERNET:
                    _printer.selectInterface((int)connectionInfo.IntefaceType, connectionInfo.Address);
                    break;
                default:
                    await DisplayAlert("Connection Fail", "Not Supported Interface", "OK");
                    _printer = null;
                    return null;
            }

            await _printSemaphore.WaitAsync();

            try
            {
                var result = await _printer.openService();
                if (result != (int)ResultCode.SUCCESS)
                {
                    _printer = null;
                    await DisplayAlert("Connection Fail", "openService failed. (" + result.ToString() + ")", "OK");
                }
            }
            finally
            {
                _printSemaphore.Release();
            }

            return _printer;
        }
    }
}