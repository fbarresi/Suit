using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Suit.Interfaces.Services;

namespace Suit.Logic.Services
{
	public class CommandLineArgumentsService : ICommandLineArgumentsService
	{
		private readonly BehaviorSubject<string[]> argumentsSubject = new BehaviorSubject<string[]>(null);
		public IObservable<string[]> Arguments => argumentsSubject.AsObservable();
		public void SetArguments(string[] args)
		{
			argumentsSubject.OnNext(args);
		}
	}
}