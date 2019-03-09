namespace Suit.Interfaces.Models
{
	public class ParsedFile
	{
		public string Name { get; set; }
		public string XLabel { get; set; }
		public string YLabel { get; set; }
		public double[][] Points { get; set; }
	}
}