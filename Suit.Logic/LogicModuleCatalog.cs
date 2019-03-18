using System;
using Ninject.Modules;
using Suit.Interfaces.Services;
using Suit.Logic.Services;

namespace Suit.Logic
{
	public class LogicModuleCatalog : NinjectModule
	{
		public override void Load()
		{
			Bind<IFileParserService>().To<FileParserService>();
			Bind<ICommandLineArgumentsService>().To<CommandLineArgumentsService>().InSingletonScope();
		}
	}
}
