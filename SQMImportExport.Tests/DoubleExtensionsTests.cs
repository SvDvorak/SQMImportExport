using System.Globalization;
using System.Threading;
using NUnit.Framework;
using SQMImportExport;

namespace SQMReorderer.Tests
{
    [TestFixture]
    public class DoubleExtensionsTests
    {
        [Test]
        public void Expect_string_returned_with_invariant_culture_when_having_invariant_culture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Assert.AreEqual("10.42", 10.42.ToStringInvariant());
        }

        [Test]
        public void Expect_string_returned_with_invariant_culture_given_other_culture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("sv");

            Assert.AreEqual("10.42", 10.42.ToStringInvariant());
        }
    }
}
