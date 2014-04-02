using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SQMReorderer.Tests
{
	[TestFixture]
    public class IEnumerableExtensionsTests
    {
	    [Test]
	    public void Returns_second_element_when_it_exists()
	    {
	        var ints = new List<int>() { 1, 2, 3 };

			Assert.AreEqual(2, ints.Second());
	    }

	    [Test]
	    public void Throws_exception_when_list_does_not_contain_two_items()
	    {
	        var ints = new List<int>() { 1 };

	        Assert.Throws<InvalidOperationException>(() => ints.Second());
	    }
    }
}
