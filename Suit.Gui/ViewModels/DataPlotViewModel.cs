using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using DynamicData;
using DynamicData.Binding;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using OxyPlot;
using Suit.Interfaces.Services;
using ScatterPoint = OxyPlot.Series.ScatterPoint;

namespace Suit.Gui.ViewModels
{
	public class DataPlotViewModel : ViewModelBase
	{
		private readonly FileInfo file;
		private readonly IFileParserService fileParserService;
		private SeriesCollection series = new SeriesCollection();

		public DataPlotViewModel(FileInfo model, IFileParserService fileParserService)
		{
			this.file = model;
			this.fileParserService = fileParserService;
		}

		public override void Init()
		{
			var parsedFile = fileParserService.ParseFileForPlot(file);
			var serie = new ScatterSeries()
			{
				Values = parsedFile.Points.Select(p => new ObservablePoint(p[0], p[1])).AsChartValues(),
				Title = parsedFile.Name
			};
			Series.Add(serie);
		}

		public SeriesCollection Series
		{
			get => series;
			set
			{
				if (Equals(value, series)) return;
				series = value;
				raisePropertyChanged();
			}
		}
	}

	internal sealed class DesignDataPlotViewModel : DataPlotViewModel
	{
		public DesignDataPlotViewModel() : base(null, null)
		{
			Init();
		}
	}
}