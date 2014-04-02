using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Common;
using SQMImportExport.Export;

namespace SQMImportExport.Tests.Export
{
	[TestFixture]
    public class SqmExporterTests
    {
        private Stream _stream;
        private ISqmFileExporterFactory _exporterFactory;
        private ISqmContentsVisitor _exportVisitor;
        private SqmContentsBase _sqmContents;

        [SetUp]
        public void Setup()
        {
            _stream = Substitute.For<Stream>();

            _exporterFactory = Substitute.For<ISqmFileExporterFactory>();
            _exportVisitor = Substitute.For<ISqmContentsVisitor>();

            _sqmContents = Substitute.For<SqmContentsBase>();
        }

        [Test]
        public void Contents_accept_visitor_created_from_exporter()
        {
            _exporterFactory.Create(_stream).Returns(_exportVisitor);

            var sut = new SqmExporter(_exporterFactory);
            sut.Export(_stream, _sqmContents);

            _sqmContents.Received().Accept(_exportVisitor);
        }
    }
}
