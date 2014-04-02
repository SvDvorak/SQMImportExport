using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Common;
using SQMImportExport.Import;
using SQMImportExport.Import.FileVersion;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class SqmImporterTests
    {
        private SqmImporter _sut;
        private IFileVersionRetriever _fileVersionRetriever;
        private ISqmFileImporter _arma2Importer;
        private ISqmFileImporter _arma3Importer;

        [SetUp]
        public void Setup()
        {
            _fileVersionRetriever = Substitute.For<IFileVersionRetriever>();
            _arma2Importer = Substitute.For<ISqmFileImporter>();
            _arma3Importer = Substitute.For<ISqmFileImporter>();

            _sut = new SqmImporter(_fileVersionRetriever, _arma2Importer, _arma3Importer);
        }

        [Test]
        public void Throws_incorrect_file_contents_when_missing_version()
        {
            var stream = Substitute.For<Stream>();
            _fileVersionRetriever.GetVersion(stream).Returns(x => { throw new SqmMissingVersionException(); });

            Assert.Throws<SqmContentsInvalidException>(() => _sut.Import(stream));
        }

        [Test]
        public void Uses_arma_2_parser_when_file_version_indicates_arma_2_version()
        {
            var stream = Substitute.For<Stream>();
            _fileVersionRetriever.GetVersion(stream).Returns(FileVersion.ArmA2);

            var expectedContents = Substitute.For<SqmContentsBase>();

            _arma2Importer.Import(stream).Returns(expectedContents);

            var sqmContents = _sut.Import(stream);

            Assert.AreEqual(expectedContents, sqmContents);
        }

        [Test]
        public void Uses_arma_3_parser_when_file_version_indicates_arma_3_version()
        {
            var stream = Substitute.For<Stream>();
            _fileVersionRetriever.GetVersion(stream).Returns(FileVersion.ArmA3);

            var expectedContents = Substitute.For<SqmContentsBase>();

            _arma3Importer.Import(stream).Returns(expectedContents);

            var sqmContents = _sut.Import(stream);

            Assert.AreEqual(expectedContents, sqmContents);
        }
    }
}
