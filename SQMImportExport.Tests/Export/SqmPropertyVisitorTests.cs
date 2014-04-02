using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Common;
using SQMImportExport.Export;

namespace SQMImportExport.Tests.Export
{
    [TestFixture]
    public class SqmPropertyVisitorTests
    {
        [Test]
        public void Expect_all_property_visitors_to_print_correctly()
        {
            var propertyVisitor = new SqmPropertyVisitor();

            var stringPropertyText = propertyVisitor.Visit("side", "WEST");
            var vectorPropertyText = propertyVisitor.Visit("position", new Vector(1, 2, 3));
            var intPropertyText = propertyVisitor.Visit("leader", 1);
            var doublePropertyText = propertyVisitor.Visit("skill", 0.60000002);
            var intListPropertyText = propertyVisitor.Visit("synchronizations", new List<int>() { 1, 2, 3 });
            var stringListPropertyText = propertyVisitor.Visit("addOns", new List<string>() { "brown", "blur" });
            var markerArrayPropertyText = propertyVisitor.Visit("markers",
                new MarkersArray() { Items = new List<string>() { "m1", "m2" } });
            var effectsPropertyText = propertyVisitor.VisitEffects(new List<string>() { "effect1", "effect2" });

            const string correctStringListText = 
                "addOns[]=\n" +
                "{\n" +
                "\"brown\",\n" +
                "\"blur\"\n" +
                "};\n";

            const string correctMarkerArrayText =
               "markers[]=\n" +
                "{\n" +
                "\"m1\",\n" +
                "\"m2\"\n" +
                "};\n";

            const string correctEffectsText =
               "class Effects\n" +
                "{\n" +
                "effect1\n" +
                "effect2\n" +
                "};\n";

            Assert.AreEqual("side=\"WEST\";\n", stringPropertyText);
            Assert.AreEqual("position[]={1,2,3};\n", vectorPropertyText);
            Assert.AreEqual("leader=1;\n", intPropertyText);
            Assert.AreEqual("skill=0.60000002;\n", doublePropertyText);
            Assert.AreEqual("synchronizations[]={1,2,3};\n", intListPropertyText);
            Assert.AreEqual(correctStringListText, stringListPropertyText);
            Assert.AreEqual(correctMarkerArrayText, markerArrayPropertyText);
            Assert.AreEqual(correctEffectsText, effectsPropertyText);
        }

        [Test]
        public void Expect_empty_strings_when_passed_missing_property_value()
        {
            var propertyVisitor = new SqmPropertyVisitor();

            var stringPropertyText = propertyVisitor.Visit("side", (string)null);
            var vectorPropertyText = propertyVisitor.Visit("position", (Vector)null);
            var intPropertyText = propertyVisitor.Visit("leader", (int?)null);
            var doublePropertyText = propertyVisitor.Visit("skill", (double?)null);
            var intListPropertyText = propertyVisitor.Visit("synchronizations", (List<int>)null);
            var stringListPropertyText = propertyVisitor.Visit("Effects", (List<string>)null);
            var markerArrayPropertyText = propertyVisitor.Visit("markers", (MarkersArray)null);
            var effectsPropertyText = propertyVisitor.VisitEffects(null);

            Assert.AreEqual("", stringPropertyText);
            Assert.AreEqual("", vectorPropertyText);
            Assert.AreEqual("", intPropertyText);
            Assert.AreEqual("", doublePropertyText);
            Assert.AreEqual("", intListPropertyText);
            Assert.AreEqual("", stringListPropertyText);
            Assert.AreEqual("", markerArrayPropertyText);
            Assert.AreEqual("", effectsPropertyText);
        }

        [Test]
        public void Expect_empty_strings_when_passed_empty_lists()
        {
            var propertyVisitor = new SqmPropertyVisitor();

            var intListPropertyText = propertyVisitor.Visit("synchronizations", new List<int>());
            var stringListPropertyText = propertyVisitor.Visit("Effects", new List<int>());

            Assert.AreEqual("", intListPropertyText);
            Assert.AreEqual("", stringListPropertyText);
        }

        [Test]
        public void Expect_single_line_marker_array_to_be_written_as_single_line()
        {
            var propertyVisitor = new SqmPropertyVisitor();

            var markerArrayPropertyText = propertyVisitor.Visit("markers",
                new MarkersArray() { IsMarkersSingleLine = true, Items = new List<string>() { "mark", "merk" } });

            const string correctMarkerArrayText = "markers[]={\"mark\",\"merk\"};\n";

            Assert.AreEqual(correctMarkerArrayText, markerArrayPropertyText);
        }
    }
}
