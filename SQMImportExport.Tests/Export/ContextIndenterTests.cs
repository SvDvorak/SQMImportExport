using NUnit.Framework;
using SQMImportExport.Export;

namespace SQMReorderer.Tests.Export
{
    [TestFixture]
    public class ContextIndenterTests
    {
        [Test]
        public void Returns_same_when_everything_is_in_same_context()
        {
            const string inputText =
                "position[]={10,12,14};\n" +
                "a=45;\n" +
                "b=55;\n" +
                "activationBy=\"ANY\";\n" +
                "interruptable=1;\n" +
                "type=\"EMPTY\";\n" +
                "age=\"UNKNOWN\";";

            var contextIndenter = new ContextIndenter();

            var actualText = contextIndenter.Indent(inputText);

            Assert.AreEqual(inputText, actualText);
        }

        [Test]
        public void Indents_content_inside_context()
        {
            const string inputText =
                "class Item1\n" +
                "{\n" +
                "a=45;\n" +
                "b=55;\n" +
                "activationBy=\"ANY\";\n" +
                "age=\"UNKNOWN\";" +
                "};";

            const string expectedText =
                "class Item1\n" +
                "{\n" +
                "\ta=45;\n" +
                "\tb=55;\n" +
                "\tactivationBy=\"ANY\";\n" +
                "\tage=\"UNKNOWN\";" +
                "};";

            var contextIndenter = new ContextIndenter();

            var actualText = contextIndenter.Indent(inputText);

            Assert.AreEqual(expectedText, actualText);
        }

        [Test]
        public void Returns_to_previous_indentation_when_leaving_context()
        {
            const string inputText =
                "class Item1\n" +
                "{\n" +
                "a=45;\n" +
                "};\n" +
                "class Item2\n" +
                "{\n" +
                "b=55;\n" +
                "};";


            const string expectedText =
                "class Item1\n" +
                "{\n" +
                "\ta=45;\n" +
                "};\n" +
                "class Item2\n" +
                "{\n" +
                "\tb=55;\n" +
                "};";

            var contextIndenter = new ContextIndenter();

            var actualText = contextIndenter.Indent(inputText);

            Assert.AreEqual(expectedText, actualText);
        }

        [Test]
        public void Uses_correct_indentation_for_deeper_contexts()
        {
            const string inputText =
                "class Item3\n" +
                "{\n" +
                "id=4;\n" +
                "class Vehicles\n" +
                "{\n" +
                "items=2;\n" +
                "class Item4\n" +
                "{\n" +
                "id=5;\n" +
                "};\n" +
                "class Item5\n" +
                "{\n" +
                "id=6;\n" +
                "};\n" +
                "};\n" +
                "class Waypoints\n" +
                "{\n" +
                "items=2;\n" +
                "class Item0\n" +
                "{\n" +
                "class Effects\n" +
                "{\n" +
                "};\n" +
                "};\n" +
                "class Item1\n" +
                "{\n" +
                "class Effects\n" +
                "{\n" +
                "};\n" +
                "};\n" +
                "};\n" +
                "};";


            const string expectedText =
                "class Item3\n" +
                "{\n" +
                "\tid=4;\n" +
                "\tclass Vehicles\n" +
                "\t{\n" +
                "\t\titems=2;\n" +
                "\t\tclass Item4\n" +
                "\t\t{\n" +
                "\t\t\tid=5;\n" +
                "\t\t};\n" +
                "\t\tclass Item5\n" +
                "\t\t{\n" +
                "\t\t\tid=6;\n" +
                "\t\t};\n" +
                "\t};\n" +
                "\tclass Waypoints\n" +
                "\t{\n" +
                "\t\titems=2;\n" +
                "\t\tclass Item0\n" +
                "\t\t{\n" +
                "\t\t\tclass Effects\n" +
                "\t\t\t{\n" +
                "\t\t\t};\n" +
                "\t\t};\n" +
                "\t\tclass Item1\n" +
                "\t\t{\n" +
                "\t\t\tclass Effects\n" +
                "\t\t\t{\n" +
                "\t\t\t};\n" +
                "\t\t};\n" +
                "\t};\n" +
                "};";

            var contextIndenter = new ContextIndenter();

            var actualText = contextIndenter.Indent(inputText);

            Assert.AreEqual(expectedText, actualText);
        }

        [Test]
        public void Indentation_ignores_single_line_property_with_brackets()
        {
            const string inputText =
                "class Item1\n" +
                "{\n" +
                "a=45;\n" +
                "position[]={10,12,14};\n" +
                "init=\"this addEventHandler [\"\"HandleDamage\"\", {}];\";\n" +
                "id=5;\n" +
                "};";

            const string expectedText =
                "class Item1\n" +
                "{\n" +
                "\ta=45;\n" +
                "\tposition[]={10,12,14};\n" +
                "\tinit=\"this addEventHandler [\"\"HandleDamage\"\", {}];\";\n" +
                "\tid=5;\n" +
                "};";

            var contextIndenter = new ContextIndenter();

            var actualText = contextIndenter.Indent(inputText);

            Assert.AreEqual(expectedText, actualText);
        }
    }
}