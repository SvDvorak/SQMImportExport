using NUnit.Framework;
using SQMImportExport.Import.HelperFunctions;

namespace SQMImportExport.Tests.Import
{
    [TestFixture]
    public class ParsingHelperFunctionsTests
    {
        private ParsingHelperFunctions _parsingHelperFunctions;

        [SetUp]
        public void SetUp()
        {
            _parsingHelperFunctions = new ParsingHelperFunctions();
        }

        [Test]
        public void Expect_line_to_be_start_of_context_given_start_bracket()
        {
            var isLineStartOfContext = _parsingHelperFunctions.IsLineStartOfContext("{\n");

            Assert.IsTrue(isLineStartOfContext);
        }

        [Test]
        public void Expect_line_to_be_end_of_context_given_end_bracket()
        {
            var isLineEndOfContext = _parsingHelperFunctions.IsLineEndOfContext("};\n");

            Assert.IsTrue(isLineEndOfContext);
        }

        [Test]
        public void Expect_line_to_be_start_of_context_given_start_bracket_with_whitespace_noise()
        {
            var isLineStartOfContext = _parsingHelperFunctions.IsLineStartOfContext("       {\n             ");

            Assert.IsTrue(isLineStartOfContext);
        }

        [Test]
        public void Expect_line_to_be_start_of_context_given_end_bracket_with_whitespace_noise()
        {
            var isLineStartOfContext = _parsingHelperFunctions.IsLineEndOfContext("       };\n             ");

            Assert.IsTrue(isLineStartOfContext);
        }
    }
}
