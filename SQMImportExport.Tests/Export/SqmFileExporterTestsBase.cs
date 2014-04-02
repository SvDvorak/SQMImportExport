using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Export;
using SQMImportExport.Export.ArmA2;
using SQMImportExport.StreamHelpers;

namespace SQMReorderer.Tests.Export
{
    public class SqmFileExporterTestsBase
    {
        internal SqmFileExporter Exporter;
        internal ISqmElementVisitor Arma2Visitor;
        internal SQMImportExport.Export.ArmA3.ISqmElementVisitor Arma3Visitor;
        internal IStreamWriterAdapter StreamWriter;
        internal IContextIndenter ContextIndenter;

        [SetUp]
        public void Setup()
        {
            Arma2Visitor = Substitute.For<ISqmElementVisitor>();
            Arma3Visitor = Substitute.For<SQMImportExport.Export.ArmA3.ISqmElementVisitor>();
            StreamWriter = Substitute.For<IStreamWriterAdapter>();
            ContextIndenter = Substitute.For<IContextIndenter>();
            Exporter = new SqmFileExporter(StreamWriter, Arma2Visitor, Arma3Visitor, ContextIndenter);
        }
    }
}