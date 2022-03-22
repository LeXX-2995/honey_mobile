﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeReaderSample.Interface;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReaderSample.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientsPage : ContentPage
	{
		public ClientsPage(INavigation navigation, HoneywellBarcodeReader scanner, IDbService dbService)
		{
			InitializeComponent();

            BindingContext = new ClientPageViewModel(navigation, scanner, dbService);
		}
	}
}