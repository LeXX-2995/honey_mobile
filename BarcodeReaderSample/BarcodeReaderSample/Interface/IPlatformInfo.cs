using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeReaderSample.Interface
{
    public interface IPlatformInfo
    {
        object AndroidContext { get; set; }
        object AndroidResource { get; set; }
        string GetPath();
        object GetImgResource();    // Image Resource Type return
        Task<object> GetImgResourceAsync();
    }
}
