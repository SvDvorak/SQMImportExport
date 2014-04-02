using NUnit.Framework;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Tests.Import
{
    [TestFixture]
    public class StringLineSetterTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Empty_string_pattern_sets_empty_string()
        {
            var actualValue = "";
            var stringLineSetter = new StringLineSetter("", x => actualValue = x);

            stringLineSetter.SetValueIfLineMatches(new SqmLine("aoeu"));

            Assert.AreEqual("", actualValue);
        }

        [Test]
        public void Sets_entire_string_to_property_when_matching_pattern_is_empty()
        {
            var actualValue = "";
            var stringLineSetter = new StringLineSetter(@"(?<value>.*)", x => actualValue = x);

            stringLineSetter.SetValueIfLineMatches(new SqmLine("aoeu htns"));

            Assert.AreEqual("aoeu htns", actualValue);
        }

        [Test]
        public void Ignores_parts_of_input_string_when_not_part_of_value()
        {
            var actualValue = "";
            var stringLineSetter = new StringLineSetter(@"--(?<value>.*);;", x => actualValue = x);

            stringLineSetter.SetValueIfLineMatches(new SqmLine("--aoeu htns;;"));

            Assert.AreEqual("aoeu htns", actualValue);
        }

        [Test]
        public void Ignores_whitespace_at_beginning()
        {
            var actualValue = "";
            var stringLineSetter = new StringLineSetter(@"(?<value>.*)", x => actualValue = x);

            stringLineSetter.SetValueIfLineMatches(new SqmLine("    aoeu htns   "));

            Assert.AreEqual("aoeu htns   ", actualValue);
        }
    }
}
