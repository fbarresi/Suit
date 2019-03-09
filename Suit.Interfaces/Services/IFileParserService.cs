using System.IO;
using Suit.Interfaces.Models;

namespace Suit.Interfaces.Services
{
	public interface IFileParserService
	{
		ParsedFile ParseFileForPlot(FileInfo fileInfo);
	}
}