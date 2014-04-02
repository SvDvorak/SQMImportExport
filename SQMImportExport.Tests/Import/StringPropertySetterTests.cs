using NUnit.Framework;
using SQMImportExport;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.DataSetters;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class StringPropertySetterTests
    {
        private string _value;
        private StringPropertySetter _stringPropertySetter;

        [SetUp]
        public void Setup()
        {
            _stringPropertySetter = new StringPropertySetter("camelot", x => _value = x);
        }

        [Test]
        public void Expect_property_setter_to_set_property_on_match()
        {
            var inputText = "camelot=\"bravesirrobin\"";

            var matchResult = _stringPropertySetter.SetValueIfLineMatches(new SqmLine(inputText));

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual("bravesirrobin", _value);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_property()
        {
            var inputText = "model=32.42";

            var matchResult = _stringPropertySetter.SetValueIfLineMatches(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreNotEqual(32.42, _value);
        }

        [Test]
        public void Expect_to_not_set_property_and_return_failure_on_incorrect_value()
        {
            var inputText = "camelot=itsonlyamodel";

            var matchResult = _stringPropertySetter.SetValueIfLineMatches(new SqmLine(inputText));

            Assert.AreEqual(Result.Failure, matchResult);
            Assert.AreNotEqual("itsonlyamodel", _value);
        }

        [Test]
        public void Expect_empty_value_when_line_is_matching_but_with_empty_value()
        {
            var inputText = "camelot=\"\"";

            var matchResult = _stringPropertySetter.SetValueIfLineMatches(new SqmLine(inputText));

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual("", _value);
        }

        [Test]
        public void Whitespace_is_trimmed_from_value_endings()
        {
            var inputText = "camelot=\"  aaa   \"";

            var matchResult = _stringPropertySetter.SetValueIfLineMatches(new SqmLine(inputText));

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual("aaa", _value);
        }
    }
}
