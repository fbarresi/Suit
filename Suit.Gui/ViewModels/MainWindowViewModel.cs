using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Dragablz;
using log4net.Core;
using Microsoft.Win32;
using ReactiveUI;
using Suit.Gui.Extensions;
using Suit.Interfaces.Commons;
using Suit.Interfaces.Extensions;

namespace Suit.Gui.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IViewModelFactory viewModelFactory;
		private ViewModelBase selectedView;
		public MainWindowViewModel(IViewModelFactory viewModelFactory)
		{
			this.viewModelFactory = viewModelFactory;
		}

		public ReactiveCommand<Unit, Unit> OpenFileCommand { get; set; }

		public ItemActionCallback ClosingTabItemHandler => ClosingTabItemHandlerImpl;

		public ObservableCollection<ViewModelBase> Views { get; } = new ObservableCollection<ViewModelBase>();

		public ViewModelBase SelectedView
		{
			get => selectedView;
			set
			{
				if (Equals(value, selectedView)) return;
				selectedView = value;
				raisePropertyChanged();
			}
		}

		public ReactiveCommand<object, Unit> DropCommand { get; set; }

		public override void Init()
		{
			Logger.Debug("Initialize main window view model");

			OpenFileCommand = ReactiveCommand.CreateFromTask(OpenFile)
				.SetupErrorHandling(Logger, Disposables);

			DropCommand = ReactiveCommand.CreateFromTask<object, Unit>(OpenDroppedFile)
				.SetupErrorHandling(Logger, Disposables);

		}

		private Task<Unit> OpenDroppedFile(object arg)
		{
			IDataObject ido = arg as IDataObject;
			if (null == ido) return Task.FromResult(Unit.Default);
 
			// Get all the possible format
			var formats = ido.GetFormats();
			if (formats.Contains("FileDrop"))
			{
				var files = ido.GetData("FileDrop") as string[];
				OpenFiles(files);
			}
			return Task.FromResult(Unit.Default);
		}

		private Task<Unit> OpenFile()
		{
			// open dialog to select file [get rid of this shit and create a material design file selector]
			Logger.Info("opening file dialog");

			var dialog = new OpenFileDialog {Filter = "All files (*.*)|*.*"};
			var result = dialog.ShowDialog();
			if (result != true) return Task.FromResult(Unit.Default);

			OpenFile(new FileInfo(dialog.FileName));
			return Task.FromResult(Unit.Default);
		}

		public void OpenFiles(IEnumerable<string> files=null)
		{
			if (files == null) return;

			foreach (var file in files)
				OpenFile(new FileInfo(file));
		}

		private void OpenFile(FileInfo file)
		{
			try
			{
				Logger.Info($"Opening file: {file}");
				var plotViewModel = viewModelFactory.CreateViewModel<FileInfo, DataPlotViewModel>(file);
				plotViewModel.Title = file.Name;
				plotViewModel.AddDisposableTo(Disposables);
				Views.Add(plotViewModel);
			}
			catch (Exception ex)
			{
				Logger.Error("error while opening file", ex);
			}
		}

		private void ClosingTabItemHandlerImpl(ItemActionCallbackArgs<TabablzControl> args)
		{
			var container = (ViewModelBase)args.DragablzItem.DataContext;
			if (container.Equals(SelectedView))
			{
				SelectedView = Views.FirstOrDefault(vc => vc != container);
			}
			container.Dispose();
		}
	}
}