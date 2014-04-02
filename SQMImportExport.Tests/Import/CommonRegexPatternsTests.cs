using System.Text.RegularExpressions;
using NUnit.Framework;
using SQMImportExport.Import.HelperFunctions;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class CommonRegexPatternsTests
    {
        private Regex _doubleRegex = new Regex(CommonRegexPatterns.DoublePattern);
        private Regex _intRegex = new Regex(CommonRegexPatterns.IntegerPattern);
        private Regex _stringRegex = new Regex(CommonRegexPatterns.NonSpacedTextPattern);

        [Test]
        public void Expect_non_double_value_to_not_match_double_pattern()
        {
            Assert.IsFalse(_doubleRegex.IsMatch("shrubbery"));
        }

        [Test]
        public void Expect_integer_value_to_match_double_pattern()
        {
            Assert.IsTrue(_doubleRegex.IsMatch("15"));
        }

        [Test]
        public void Expect_simple_decimal_double_to_match_double_pattern()
        {
            Assert.IsTrue(_doubleRegex.IsMatch("1.5"));
        }

        [Test]
        public void Expect_string_with_decimal_but_no_significant_number_to_not_match_double_pattern()
        {
            Assert.IsTrue(_doubleRegex.IsMatch(".15"));
        }

        [Test]
        public void Expect_negative_double_to_match_double_pattern()
        {
            Assert.IsTrue(_doubleRegex.IsMatch("-1.5"));
        }

        [Test]
        public void Expect_positive_whole_numbers_to_match_int_pattern()
        {
            Assert.AreEqual("1", _intRegex.Match("1").Value);
        }

        [Test]
        public void Expect_negative_whole_numbers_to_match_int_pattern()
        {
            Assert.AreEqual("-1", _intRegex.Match("-1").Value);
        }

        [Test]
        public void Expect_simple_string_without_spaces_to_match_string_pattern()
        {
            Assert.IsTrue(_stringRegex.IsMatch("tisIsJustAFleshwound"));
        }

        [Test]
        public void Expect_complex_string_to_match_pattern()
        {
            Assert.IsTrue(_stringRegex.IsMatch("23then_have_a_nap___ZENFIREZEMISSILES11111"));
        }

        [Test]
        public void Expect_complex_string_with_surrounding_trash_to_return_correct_string()
        {
            var match = _stringRegex.Match(@"##""  _hello0oo0o0_--//");

            Assert.AreEqual("_hello0oo0o0_", match.Value);
        }
    }
}
