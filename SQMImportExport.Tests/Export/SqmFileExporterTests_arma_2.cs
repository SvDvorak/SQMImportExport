using NSubstitute;
using NUnit.Framework;
using SQMImportExport.ArmA2;

namespace SQMReorderer.Tests.Export
{
    [TestFixture]
    public class SqmFileExporterTests_arma_2 : SqmFileExporterTestsBase
    {
        [Test]
        public void Uses_sqm_element_visitor_to_convert_contents_to_string()
        {
            var contents = new SqmContents();
            Exporter.Visit(contents);

            Arma2Visitor.Received().Visit("", contents);
        }

        [Test]
        public void Writes_converted_and_indented_string_to_stream_using_stream_writer()
        {
            var contents = new SqmContents();
            const string convertedString = "Text!";
            const string indentedString = "Indented Text!";

            Arma2Visitor.Visit("", contents).Returns(convertedString);
            ContextIndenter.Indent(convertedString).Returns(indentedString);

            Exporter.Visit(contents);

            StreamWriter.Received().Write(indentedString);
        }

        [Test]
        public void Flushes_stream_writer_when_finished()
        {
            var contents = new SqmContents();

            Exporter.Visit(contents);

            StreamWriter.Received().Flush();
        }
    }
}