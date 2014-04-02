using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SQMImportExport.Common;
using SQMImportExport.Export;
using SQMImportExport.Export.ArmA2;
using SQMImportExport.Import;
using SQMImportExport.Import.ArmA2;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.FileVersion;
using SQMImportExport.StreamHelpers;

namespace SQMReorderer.Tests.Import
{
    [Category("AcceptanceTest")]
    [TestFixture]
    public class SqmFileTests
    {
        private string _armaVersion;

        [TestCase(2)]
        [TestCase(3)]
        [Test]
        public void Expect_SqmParser_to_successfully_parse_testFile(int armaVersion)
        {
            _armaVersion = armaVersion.ToString();
            CleanupPreviousTest();
            var importStream = GetImportStream();
            var importResults = Import(importStream);
            importStream.Seek(0, SeekOrigin.Begin);

            Export(importResults, GetTestExportPath());

            var verifyExportStream = GetExportedFileStream(GetTestExportPath());
            Assert.AreEqual(CombineToSingleString(importStream), CombineToSingleString(verifyExportStream));

            verifyExportStream.Close();
        }

        private void CleanupPreviousTest()
        {
            var testExportPath = GetTestExportPath();

            if (File.Exists(testExportPath))
            {
                File.Delete(testExportPath);
            }
        }

        private string GetTestExportPath()
        {
            return "test_arma" + _armaVersion + "_export.sqm";
        }

        private Stream GetImportStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetName().Name + ".Import.ArmA" + _armaVersion + ".mission.sqm";
            var importStream = assembly.GetManifestResourceStream(resourcePath);

            return importStream;
        }

        private SqmContentsBase Import(Stream importStream)
        {
            var streamToStringsReader = new StreamToStringsReader();
            var sqmContextCreator = new SqmContextCreator();
            var sqmFileImporter = new SqmImporter(new FileVersionRetriever(new StreamReaderFactory()),
                new SqmFileImporter(streamToStringsReader, sqmContextCreator,
                    new SqmParser()),
                new SqmFileImporter(streamToStringsReader, sqmContextCreator,
                    new SQMImportExport.Import.ArmA3.SqmParser()));

            var importResults = sqmFileImporter.Import(importStream);

            return importResults;
        }

        private void Export(SqmContentsBase importResults, string path)
        {
            var contextIndenter = new ContextIndenter();
            var exportStream = new FileStream(path, FileMode.OpenOrCreate);
            var streamWriter = new StreamWriterAdapter(exportStream);
            var fileExporter = new SqmFileExporter(
                streamWriter,
                new SqmElementExportVisitor(),
                new SQMImportExport.Export.ArmA3.SqmElementExportVisitor(), 
                contextIndenter);

            importResults.Accept(fileExporter);
            exportStream.Close();
        }

        private FileStream GetExportedFileStream(string path)
        {
            var verifyExportStream = new FileStream(path, FileMode.Open);

            return verifyExportStream;
        }

        private string CombineToSingleString(Stream fileStream)
        {
            var fileContents = new StreamToStringsReader().Read(fileStream);
            var testFileStringBuilder = new StringBuilder();

            foreach (var row in fileContents)
            {
                testFileStringBuilder.Append(row + "\n");
            }

            return testFileStringBuilder.ToString();
        }
    }
}