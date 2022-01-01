﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BarcodeReaderSample
{
    public partial class App : Application
    {
        public MainPage MainPageApp { get; }
        public App ()
        {
            InitializeComponent();

            MainPageApp = new MainPage();
            
            MainPage = new NavigationPage( MainPageApp);
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
            MainPageApp.HoneywellBarcodeReader.OpenBarcodeReader();
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
            MainPageApp.HoneywellBarcodeReader.CloseBarcodeReader();
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
            MainPageApp.HoneywellBarcodeReader.OpenBarcodeReader();
        }
    }
}
