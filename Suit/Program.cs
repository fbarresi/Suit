﻿using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using Ninject;
using Suit.Gui;
using Suit.Gui.ViewModels;
using Suit.Gui.Views;
using Suit.Interfaces.Commons;
using Suit.Logic;

namespace Suit
{
	public class Program
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool FreeConsole();

		[STAThread]
		public static void Main(string[] args)
		{
			FreeConsole();

			using (IKernel kernel = new StandardKernel())
			{
				LoadModules(kernel);

				var viewModelFactory = kernel.Get<ViewModelLocator>();
				var application = CreateApplication(viewModelFactory);

				var mainWindowViewModel = viewModelFactory.CreateViewModel<MainWindowViewModel>();

				var mainWindow = kernel.Get<MainWindow>();
				mainWindow.DataContext = mainWindowViewModel;

				application.Run(mainWindow);
				application.Shutdown();
			}
		}

		private static void LoadModules(IKernel kernel)
		{
			kernel.Load<GuiModuleCatalog>();
			kernel.Load<LogicModuleCatalog>();
		}

		private static Application CreateApplication(IViewModelFactory viewModelLocator)
		{
			var application = new App() { ShutdownMode = ShutdownMode.OnLastWindowClose };

			application.InitializeComponent();
			application.ReplaceViewModelLocator(viewModelLocator);

			return application;
		}
	}

	public static class ApplicationExtensions
	{
		public static void ReplaceViewModelLocator(this Application application, IViewModelFactory viewModelLocator, string locatorKey = "Locator")
		{
			if (application.Resources.Contains(locatorKey))
				application.Resources.Remove(locatorKey);
			application.Resources.Add(locatorKey, viewModelLocator);
		}
	}
}
