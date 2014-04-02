using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Tests.Import
{
    [TestFixture]
    public class MultiLineStringListPropertySetterTests
    {
        private List<string> _values;
        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _values = null;

            _contextCreator = new SqmContextCreator();
        }

        [Test]
        public void Expect_empty_list_with_incorrect_property()
        {
            var inputText = new List<string>()
                {
                    "thedarkknight[]=\n",
                    "{\n",
                    "\"IMBATMAN\",\n",
                    "\"NOIAM\",\n",
                    "\"SHUTUPSPARTACUS\"\n",
                    "};\n"
                };

            var stringListPropertySetter = new MultiLineStringListPropertySetter("shrubberies", x => _values = x);

            var context = _contextCreator.CreateContext(inputText);

            var matchResult = stringListPropertySetter.SetContextIfMatch(context);

            Assert.AreEqual(Result.Failure, matchResult);
        }

        [Test]
        public void Expect_single_item_list_of_property_with_single_item()
        {
            var inputText = new List<string>()
                                {
                                    "brave[]=\n",
                                    "{\n",
                                    "\"sirRobin\"\n",
                                    "};\n"
                                };

            var stringListPropertySetter = new MultiLineStringListPropertySetter("brave", x => _values = x);

            var context = _contextCreator.CreateContext(inputText);

            var matchResult = stringListPropertySetter.SetContextIfMatch(context);

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(1, _values.Count);
            Assert.AreEqual("sirRobin", _values[0]);
        }

        [Test]
        public void Expect_multiple_items_from_property_with_multiple_items()
        {
            var inputText = new List<string>()
                                {
                                    "smartCreatures[]=\n",
                                    "{\n",
                                    "\"mice\"\n",
                                    "\"dolphins\"\n",
                                    "\"humans\"\n",
                                    "};\n"
                                };

            var stringListPropertySetter = new MultiLineStringListPropertySetter("smartCreatures", x => _values = x);

            var context = _contextCreator.CreateContext(inputText);

            var matchResult = stringListPropertySetter.SetContextIfMatch(context);

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(3, _values.Count);
            Assert.AreEqual("mice", _values[0]);
            Assert.AreEqual("dolphins", _values[1]);
            Assert.AreEqual("humans", _values[2]);
        }

        [Test]
        public void Expect_empty_collection_on_empty_property()
        {
            var inputText = new List<string>()
                                {
                                    "space[]=\n",
                                    "{\n",
                                    "};\n"
                                };

            var stringListPropertySetter = new MultiLineStringListPropertySetter("space", x => _values = x);

            var context = _contextCreator.CreateContext(inputText);

            var matchResult = stringListPropertySetter.SetContextIfMatch(context);

            Assert.AreEqual(Result.Success, matchResult);
            Assert.AreEqual(0, _values.Count);
        }
    }
}
