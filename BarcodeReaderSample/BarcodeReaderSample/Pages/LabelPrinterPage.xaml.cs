using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BarcodeReaderSample.Interface;
using BarcodeReaderSample.Models;
using Entities;
using Newtonsoft.Json;
using Plugin.SatoMpXamarinSDK;
using Plugin.SatoMpXamarinSDK.Abstractions;
using TraceIQ.Expeditor.API;
using TraceIQ.Expeditor.Models;
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
        private readonly Guid _orderId;
        private readonly IDbService _dbService;
        private readonly List<OrderDetailsBillModel> Orders;
        private double _total;
        private FiscalBoxDataModel FiscalBoxResponseModel;
        private readonly Order _order;
        public LabelPrinterPage(Guid orderId, IDbService dbService)
        {
            InitializeComponent();
            _orderId = orderId;
            _dbService = dbService;

            var getOrderBills = dbService.GetOrderDetailBill(orderId);

            Orders = getOrderBills.Result == OperationStatus.Success
                ? getOrderBills.Value
                : new List<OrderDetailsBillModel>();

            var getOrder = _dbService.GetOrder(orderId);
            if (getOrder.Result == OperationStatus.Success)
                _order = getOrder.Value;

            _total = getOrder.Result != OperationStatus.Success ? default : getOrder.Value.Total ?? default ;

            FiscalBoxResponseModel =
                JsonConvert.DeserializeObject<FiscalBoxDataModel>(getOrder.Value.FiscalBoxData);
        }
        async void OnPrintLabelClicked(object sender, EventArgs e)
        {
            if (!Orders.Any())
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Заказы не найдены", "ОК");
                return;
            }

            if(_total == default)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Сумма заказа равнятеся нулю.", "ОК");
                return;
            }

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

                await _printer.setLength(1000 + Orders.Count * 700, 0, 'C', 0);
                _printer.setTextEncoding(1251);

                // Bitmap Font
                await _printer.setCharacterset((int)LabelCodePage.WPC1251, (int)ICS.USA);

                //-------------------

                await _printer.drawTextVectorFont("ИНН: ", xPos , yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);
                
                await _printer.drawTextVectorFont("303054397", MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                //-------------------

                await _printer.drawTextVectorFont($"Дата: {DateTime.Now:dd.MM.yyyy}", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont($"Время: {DateTime.Now:HH:mm:ss}", MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                //-------------------
                await _printer.drawTextVectorFont($"Номер чека: ", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont($"№12", MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                //-------------------
                await _printer.drawTextVectorFont($"Кассир: ", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont($"{RestContext.User?.Name}", MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                var fontWidth = 16;
                var fontHeight = 16;
                foreach (var order in Orders)
                {
                    
                    await _printer.drawTextVectorFont($"-----------------------------------------", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                    var splitNames = Split(order.Name, 20);

                    yPos += 35;
                    var yPosName = yPos;
                    foreach (var name in splitNames)
                    {
                        await _printer.drawTextVectorFont(name, xPos, yPosName, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, true, false, false, (int)LabelAlignment.LEFT);
                        yPosName += 25;
                    }

                    await _printer.drawTextVectorFont(order.Quantity + " " + order.UnitOfMeasurement, (MAX_WIDTH / 2) - 10 , yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 14, 14, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                    await _printer.drawTextVectorFont(order.Price, MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                    yPos = yPosName;

                    await _printer.drawTextVectorFont("НДС(15%)", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                    await _printer.drawTextVectorFont(order.Vat, MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                    yPos += 35;

                    await _printer.drawTextVectorFont("ИКПУ:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                    await _printer.drawTextVectorFont(order.Ikpu, MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                    yPos += 35;

                    await _printer.drawTextVectorFont("КМ:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                    foreach (var code in order.Codes)
                    {
                        await _printer.drawTextVectorFont(code, MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 14, 14, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                        yPos += 20;
                    }
                }

                await _printer.drawTextVectorFont($"-----------------------------------------", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, 28, 28, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);
                yPos += 35;


                await _printer.drawTextVectorFont("ИТОГО:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, true, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont(_total.ToString("#,##0.00"), MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                await _printer.drawTextVectorFont("Всего НДС:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont((_total * 0.15).ToString("#,##0.00"), MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                await _printer.drawTextVectorFont("Наличные:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont(_order.Cash.ToString("#,##0.00"), MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                await _printer.drawTextVectorFont("Безналичные:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont(_order.Terminal.ToString("#,##0.00"), MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                await _printer.drawTextVectorFont("Скидка:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont("0", MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                await _printer.drawTextVectorFont("Фискаальные данные", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, true, false, false, (int)LabelAlignment.LEFT);
                yPos += 35;

                await _printer.drawTextVectorFont("ФМ ID:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont(FiscalBoxResponseModel?.TerminalId, MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                await _printer.drawTextVectorFont("Фискальная подпись:", xPos, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.LEFT);

                await _printer.drawTextVectorFont(FiscalBoxResponseModel?.FiscalSign, MAX_WIDTH - 24, yPos, (char)LabelVectorFont.VECTOR_FONT_ASCII, fontWidth, fontHeight, 0, 0, false, false, false, false, (int)LabelAlignment.RIGHT);
                yPos += 35;

                var url = FiscalBoxResponseModel?.QrUrl;

                // QR Code
                await _printer.drawBarcodeQRCode(url, xPos + 150, yPos, 4, (int)LabelQRCodeModel.MODEL_2, (int)LabelQRCodeECL.ECCLEVEL_Q, 0);
                    
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
                        //lblAddress.Text = connectionInfo.Address;
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
                btnPrintLabel.IsEnabled = false;
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
                //PrinterStatusEvent(await _printer.checkPrinterStatus());
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
                    await DisplayAlert("Connection Fail", "openService failed. (" + result + ")", "OK");
                }
                else
                {
                    btnPrintLabel.IsEnabled = true;
                }
            }
            finally
            {
                _printSemaphore.Release();
            }

            return _printer;
        }

        List<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize)).ToList();
        }
    }
}