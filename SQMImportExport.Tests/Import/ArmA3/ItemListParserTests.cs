using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.ArmA3;
using SQMImportExport.Import;
using SQMImportExport.Import.ArmA3;
using SQMImportExport.Import.ArmA3.Vehicle;
using SQMImportExport.Import.Context;

namespace SQMReorderer.Tests.Import.ArmA3
{
    [TestFixture]
    public class ItemListParserTests
    {
        private ItemListParser<Vehicle> _itemListParser;

        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _itemListParser = new ItemListParser<Vehicle>(new VehicleItemParserFactory(), "Vehicles");

            _contextCreator = new SqmContextCreator();
        }

        [Test]
        public void Expect_is_list_element_to_be_true_on_correct_element_syntax()
        {
            var context = _contextCreator.CreateContext(new List<string> {"class Vehicles\n", "{\n", "};\n"});

            var isVehiclesElement = _itemListParser.IsCorrectContext(context);

            Assert.IsTrue(isVehiclesElement);
        }

        [Test]
        public void Expect_is_list_element_to_be_false_on_incorrect_element_syntax()
        {
            var context = _contextCreator.CreateContext(new List<string> {"class Item0", "{", "};"});

            var isVehiclesElement = _itemListParser.IsCorrectContext(context);

            Assert.IsFalse(isVehiclesElement);
        }

        [Test]
        public void Expect_parse_exception_given_empty_list()
        {
            var inputText = new List<string>
                {
                    "class Vehicles",
                    "{",
                    "items=0",
                    "};"
                };

            var context = _contextCreator.CreateContext(inputText);

            Assert.Throws<SqmParseException>(() => _itemListParser.ParseContext(context));
        }

        [Test]
        public void Expect_parse_exception_given_incorrect_item_count()
        {
            var inputText = new List<string>
                {
                    "class Vehicles",
                    "{",
                    "items=0",
                    "class Item0",
                    "{",
                    "side=\"EAST\"",
                    "};",
                    "};"
                };

            var context = _contextCreator.CreateContext(inputText);

            Assert.Throws<SqmParseException>(() => _itemListParser.ParseContext(context));
        }

        [Test]
        public void Expect_parser_to_return_one_item_with_correct_data_given_one_list_item()
        {
            var inputText = new List<string>
                {
                    "class Vehicles",
                    "{",
                    "items=1",
                    "class Item0",
                    "{",
                    "text=\"TestString\"",
                    "};",
                    "};"
                };

            var context = _contextCreator.CreateContext(inputText);

            var items = _itemListParser.ParseContext(context);

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("TestString", items[0].Text);
        }

        [Test]
        public void Expect_parser_to_return_three_items_given_three_list_items()
        {
            var inputText = new List<string>
                {
                    "class Vehicles",
                    "{",
                    "items=3",
                    "class Item0",
                    "{",
                    "side=\"EAST\";",
                    "};",
                    "class Item1",
                    "{",
                    "side=\"EAST\";",
                    "};",
                    "class Item2",
                    "{",
                    "side=\"EAST\";",
                    "};",
                    "};"
                };

            var context = _contextCreator.CreateContext(inputText);

            var items = _itemListParser.ParseContext(context);

            Assert.AreEqual(3, items.Count);
        }
    }
}
