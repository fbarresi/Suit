using System;
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
			var parsed = new ParsedFile {Name = fileInfo.Name, Points = File.ReadAllLines(fileInfo.FullName).Select(s => s.Split(';').Select(double.Parse).ToArray()).ToArray()};
			return parsed;
		}
	}
}