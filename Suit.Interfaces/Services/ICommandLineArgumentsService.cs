using System;

namespace Suit.Interfaces.Services
{
	public interface ICommandLineArgumentsService
	{
		IObservable<string[]> Arguments { get; }
		void SetArguments(string[] args);
	}
}