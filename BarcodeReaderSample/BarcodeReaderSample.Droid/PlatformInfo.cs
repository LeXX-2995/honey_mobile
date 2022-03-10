using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeReaderSample.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(BarcodeReaderSample.Droid.PlatformInfo))]

namespace BarcodeReaderSample.Droid
{
    public class PlatformInfo : IPlatformInfo
    {
        public object AndroidContext { get; set; }
        public string GetPath()
        {
            Java.IO.File sdCard = Android.OS.Environment.ExternalStorageDirectory;
            if (sdCard != null)
                return sdCard.AbsolutePath;
            return "";
        }
        public object GetImgResource()
        {
            //Android.Graphics.Bitmap bitmap = Android.Graphics.BitmapFactory.DecodeResource((Android.Content.Res.Resources)AndroidResource, Resource.I);
            return null;
        }
        public async Task<object> GetImgResourceAsync()
        {
            //Android.Graphics.Bitmap bitmap = Android.Graphics.BitmapFactory.DecodeResource((Android.Content.Res.Resources)AndroidResource, Resource.Drawable.SatoLogo);
            //return bitmap;
            return null;
        }

        public object AndroidResource { get; set; }


    }
}