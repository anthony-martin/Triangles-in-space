using NSubstitute;
using NUnit.Framework;

namespace TrianglesInSpace
{
	/// <summary>
	/// A base class for all testing 
	/// Intended to contain any helper functions and very basic setup to make testing easier
	/// </summary>
	[TestFixture]
	public class TestSpecification
	{
        public T Get<T>()
           where T : class
        {
            return Substitute.For<T>();
        }
	}
}
