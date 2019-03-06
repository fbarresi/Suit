using Ninject.Parameters;

namespace Suit.Interfaces.Commons
{
	public interface IInstanceCreator
	{
		T CreateInstance<T>(ConstructorArgument[] arguments);
	}
}