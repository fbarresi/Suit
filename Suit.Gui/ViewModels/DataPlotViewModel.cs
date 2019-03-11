using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using DynamicData;
using DynamicData.Binding;
using OxyPlot;
using OxyPlot.Series;
using Suit.Interfaces.Services;

namespace Suit.Gui.ViewModels
{
	public class DataPlotViewModel : ViewModelBase
	{
		private readonly FileInfo file;
		private readonly IFileParserService fileParserService;
		private ObservableCollection<ScatterPoint> points = new ObservableCollection<ScatterPoint>();

		public DataPlotViewModel(FileInfo model, IFileParserService fileParserService)
		{
			this.file = model;
			this.fileParserService = fileParserService;
		}

		public override void Init()
		{
			var parsedFile = fileParserService.ParseFileForPlot(file);
			Points.AddRange(parsedFile.Points
				.Select(coords => new ScatterPoint(coords[0], coords[1])).ToArray());
			Title = parsedFile.Name;
		}

		public ObservableCollection<ScatterPoint> Points
		{
			get => points;
			set
			{
				if (Equals(value, points)) return;
				points = value;
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