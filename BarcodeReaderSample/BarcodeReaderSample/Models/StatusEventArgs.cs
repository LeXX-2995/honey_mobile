using System;

namespace TraceIQ.Expeditor.Models
{
    public class StatusEventArgs : EventArgs
    {
        public StatusEventArgs(string dataIn, string barcodeTypeIn)
        {
            Data = dataIn;
            BarcodeType = barcodeTypeIn;
        }

        public string Data { get; }

        public string BarcodeType { get; }
    }
}
