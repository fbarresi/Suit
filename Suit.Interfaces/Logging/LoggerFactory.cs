using log4net;

namespace Suit.Interfaces.Logging
{
	public static class LoggerFactory
	{
		public static ILog GetLogger()
		{
			return LogManager.GetLogger(Constants.LoggingRepositoryName);
		}
	}
}