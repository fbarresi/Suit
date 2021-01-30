using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Suit.Interfaces.Models;
using Suit.Interfaces.Services;

namespace Suit.Logic.Services
{
	public class FileParserService : IFileParserService
	{
		public ParsedFile ParseFileForPlot(FileInfo fileInfo)
		{
			var parsed = new ParsedFile {Name = fileInfo.Name, Points = ReadAllLines(fileInfo.FullName)};
			return parsed;
		}

		private double[][] ReadAllLines(string file)
		{
			var points = new List<double[]>();
			using (StreamReader sr = File.OpenText(file))
			{
				string line = string.Empty;
				while ((line = sr.ReadLine()) != null)
				{
					points.Add(line.Split(';').Select(double.Parse).ToArray());
				}
			}

			return points.ToArray();
		}
	}
}