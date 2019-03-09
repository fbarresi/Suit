using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using Suit.Interfaces.Services;

namespace Suit.Gui.ViewModels
{
	public class DataPlotViewModel : ViewModelBase
	{
		private readonly FileInfo file;
		private readonly IFileParserService fileParserService;
		private PlotModel plotModel;

		public DataPlotViewModel(FileInfo model, IFileParserService fileParserService)
		{
			this.file = model;
			this.fileParserService = fileParserService;
		}

		public override void Init()
		{
			var parsedFile = fileParserService.ParseFileForPlot(file);
			var scatterSerie = new ScatterSeries(){MarkerType = MarkerType.Circle};
			scatterSerie.Points.AddRange(parsedFile.Points
				.Select(coords => new ScatterPoint(coords[0], coords[1])).ToArray());
			var model = new PlotModel(){Title = parsedFile.Name};
			model.Series.Add(scatterSerie);
			PlotModel = model;
		}


		public PlotModel PlotModel
		{
			get => plotModel;
			set
			{
				if (Equals(value, plotModel)) return;
				plotModel = value;
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