using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Honeywell.AIDC.CrossPlatform;

namespace BarcodeReaderSample
{
    public class HoneywellBarcodeReader
    {
        private const string DEFAULT_READER_KEY = "dcs.scanner.imager";
        private Dictionary<string, BarcodeReader> mBarcodeReaders;
        private BarcodeReader Reader = null;
        public List<string> Readers { get; set; } = new List<string>();

        public delegate void BarcodeDataReady(object sender, BarcodeDataArgs e);

        public event BarcodeDataReady OnBarcodeRead;

        public HoneywellBarcodeReader()
        {
            
        }

        public async Task<string> PopulateReader()
        {
            try
            {
                // Queries the list of readers that are connected to the mobile computer.
                var readerList = await BarcodeReader.GetConnectedBarcodeReaders();
                if (readerList.Count > 0)
                {
                    foreach (var reader in readerList)
                    {
                        Readers.Add(reader.ScannerName);
                    }
                }
                else
                {
                    Readers.Add(DEFAULT_READER_KEY);
                }
            }
            catch (Exception ex)
            {
                Readers.Add(DEFAULT_READER_KEY);
                return ex.Message;
            }

            return string.Empty;
        }

        private void MBarcodeReader_BarcodeDataReady(object sender, BarcodeDataArgs e)
        {
            OnBarcodeRead?.Invoke(sender, e);
        }

        /// <summary>
        /// Opens the barcode reader. This method should be called from the
        /// main UI thread because it also updates the button states.
        /// </summary>
        public async Task<string> OpenBarcodeReader()
        {
            try
            {
                await PopulateReader();
                Reader = new BarcodeReader(DEFAULT_READER_KEY);

                if (Reader != null)
                    Reader.BarcodeDataReady += MBarcodeReader_BarcodeDataReady;
                
                if (Reader.IsReaderOpened) 
                    return string.Empty;
            
                var result = await Reader.OpenAsync();

                if (result.Code == BarcodeReader.Result.Codes.SUCCESS ||
                    result.Code == BarcodeReader.Result.Codes.READER_ALREADY_OPENED)
                {
                    SetScannerAndSymbologySettings();
                    return await EnableScanner(false);
                }
                else
                {
                    return result.Message;
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Closes the barcode reader. This method should be called from the
        /// main UI thread because it also updates the button states.
        /// </summary>
        public async Task<string> CloseBarcodeReader()
        {
            if (!(Reader is {IsReaderOpened: true}))
                return string.Empty;

            var result = await Reader.CloseAsync();
            if (result.Code == BarcodeReader.Result.Codes.SUCCESS)
            {
            }
            else
            {
                return result.Message;
            }

            return string.Empty;
        }

        private async Task<string> SetScannerAndSymbologySettings()
        {
            try
            {
                if (Reader.IsReaderOpened)
                {
                    var settings = new Dictionary<string, object>()
                    {
                        {
                            Reader.SettingKeys.TriggerScanMode,
                            Reader.SettingValues.TriggerScanMode_OneShot
                        },
                        {Reader.SettingKeys.Code128Enabled, true},
                        {Reader.SettingKeys.DatamatrixEnabled, true},
                        {Reader.SettingKeys.Code39Enabled, true},
                        {Reader.SettingKeys.Ean8Enabled, true},
                        {Reader.SettingKeys.Ean8CheckDigitTransmitEnabled, true},
                        {Reader.SettingKeys.Ean13Enabled, true},
                        {Reader.SettingKeys.Ean13CheckDigitTransmitEnabled, true},
                        {Reader.SettingKeys.Interleaved25Enabled, true},
                        {Reader.SettingKeys.Interleaved25MaximumLength, 100},
                        {Reader.SettingKeys.Postal2DMode, Reader.SettingValues.Postal2DMode_Usps}
                    };

                    var result = await Reader.SetAsync(settings);
                    if (result.Code != BarcodeReader.Result.Codes.SUCCESS)
                    {
                        return result.Message;
                    }
                }
            }
            catch (Exception exp)
            {
                return exp.Message;
            }

            return string.Empty;
        }

        public async Task<string> EnableScanner(bool isEnable)
        {
            if (!(Reader is {IsReaderOpened: true})) 
                return string.Empty;
            try
            {
                BarcodeReader.Result result = await Reader.EnableAsync(isEnable); // Enables or disables barcode reader
                return result.Code != BarcodeReader.Result.Codes.SUCCESS ? string.Empty : result.Message;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}