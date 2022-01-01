using System;
using System.Collections.Generic;
using System.Text;

namespace NobelXamarin.Models
{
    public class Barcode
    {
        public string Data { get; set; }

        public string Type { get; set; }

        public Barcode() { }

        public Barcode(string aData, string aType)
        {
            Data = aData;
            Type = aType;
        }
    }
}
