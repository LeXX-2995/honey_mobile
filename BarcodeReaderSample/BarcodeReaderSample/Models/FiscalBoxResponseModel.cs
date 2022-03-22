using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BarcodeReaderSample.Models
{
    public class FiscalBoxResponseModel
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("is_success")]
        public bool IsSuccess { get; set; }
    }

    public class Data
    {
        [JsonProperty("terminal_id")]
        public string TerminalId { get; set; }

        [JsonProperty("receipt_count")]
        public int ReceiptCount { get; set; }

        [JsonProperty("date_time")]
        public string DateTime { get; set; }

        [JsonProperty("fiscal_sign")]
        public string FiscalSign { get; set; }

        [JsonProperty("applet_version")]
        public string AppletVersion { get; set; }

        [JsonProperty("qr_url")]
        public string QrUrl { get; set; }

        [JsonProperty("cash_box_number")]
        public string CashBoxNumber { get; set; }
    }
}
