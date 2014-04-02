using System.Collections.Generic;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.ArmA2;
using SQMImportExport.Import;
using SQMImportExport.Import.Context;
using SQMImportExport.StreamHelpers;

namespace SQMImportExport.Tests.Import
{
    [TestFixture]
    public class SqmFileImporterTests
    {
        private SqmFileImporter _importer;

        private IStreamToStringsReader _streamToStringsReader;
        private ISqmContextCreator _sqmContextCreator;
        private ISqmParser _sqmParser;

        private SqmContents _expectedContents;
        private SqmContext _sqmFileContext;
        private List<string> _textLinesInStream;
        private MemoryStream _memoryStream;

        [SetUp]
        public void Setup()
        {
            _streamToStringsReader = Substitute.For<IStreamToStringsReader>();
            _sqmContextCreator = Substitute.For<ISqmContextCreator>();
            _sqmParser = Substitute.For<ISqmParser>();

            _importer = new SqmFileImporter(_streamToStringsReader, _sqmContextCreator, _sqmParser);

            _memoryStream = new MemoryStream();

            _textLinesInStream = new List<string>();
            _streamToStringsReader.Read(_memoryStream).Returns(_textLinesInStream);

            _sqmFileContext = new SqmContext();
            _sqmContextCreator.CreateRootContext(_textLinesInStream).Returns(_sqmFileContext);

            _expectedContents = new SqmContents();
            _sqmParser.ParseContext(_sqmFileContext).Returns(_expectedContents);
        }

        [Test]
        public void Uses_file_to_strings_reader_to_read_selected_file()
        {
            _importer.Import(_memoryStream);

            _streamToStringsReader.Received().Read(_memoryStream);
        }

        [Test]
        public void Creates_sqm_context_from_read_lines()
        {
            _importer.Import(_memoryStream);

            _sqmContextCreator.Received().CreateRootContext(_textLinesInStream);
        }

        [Test]
        public void Parses_sqm_context_from_created_context()
        {
            _importer.Import(_memoryStream);

            _sqmParser.Received().ParseContext(_sqmFileContext);
        }
    }
}