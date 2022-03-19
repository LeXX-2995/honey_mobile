using System.IO;
using BarcodeReaderSample.Droid;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(DbPath))]
namespace BarcodeReaderSample.Droid
{
    
    public class DbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
        }
    }
}