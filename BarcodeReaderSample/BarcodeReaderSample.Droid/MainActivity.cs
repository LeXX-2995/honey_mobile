using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using BarcodeReaderSample.Interface;
using Xamarin.Forms;
using Application = Android.App.Application;

namespace BarcodeReaderSample.Droid
{
    [Activity(Label = "Expeditor", Icon = "@drawable/tracking", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            DevExpress.XamarinForms.Editors.Initializer.Init();

            var platform = DependencyService.Get<IPlatformInfo>();
            if ((platform as PlatformInfo) != null)
            {
                platform.AndroidContext = this;
                platform.AndroidResource = Resources;
            }

            LoadApplication(new App());

            //lock application orientation
            ScreenLayout screenSize = Application.Context.ApplicationContext.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask;
            switch (screenSize)
            {
                case ScreenLayout.SizeXlarge:
                case ScreenLayout.SizeLarge:
                    this.RequestedOrientation = ScreenOrientation.Landscape;
                    break;
                default:
                    this.RequestedOrientation = ScreenOrientation.Portrait;
                    break;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

