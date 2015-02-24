using System;
using System.IO;
using SQMImportExport.Common;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.FileVersion;
using SQMImportExport.StreamHelpers;

namespace SQMImportExport.Import
{
    public class SqmImporter : ISqmImporter
    {
        private readonly IFileVersionRetriever _fileVersionRetriever;
        private readonly ISqmFileImporter _arma2Importer;
        private readonly ISqmFileImporter _arma3Importer;

        internal SqmImporter(IFileVersionRetriever fileVersionRetriever,
            ISqmFileImporter arma2Importer,
            ISqmFileImporter arma3Importer)
        {
            _fileVersionRetriever = fileVersionRetriever;
            _arma2Importer = arma2Importer;
            _arma3Importer = arma3Importer;
        }

        public SqmImporter()
            : this(
                new FileVersionRetriever(new StreamReaderFactory()),
                new SqmFileImporter(new StreamToStringsReader(), new SqmContextCreator(), new ArmA2.SqmParser()),
                new SqmFileImporter(new StreamToStringsReader(), new SqmContextCreator(), new ArmA3.SqmParser()))
        {
        }

        public SqmContentsBase Import(Stream stream)
        {
            try
            {
                var fileVersion = _fileVersionRetriever.GetVersion(stream);
                return fileVersion == FileVersion.FileVersion.ArmA2 ? _arma2Importer.Import(stream) : _arma3Importer.Import(stream);
            }
            catch (Exception exception)
            {
                throw new SqmContentsInvalidException(exception);
            }
        }
    }

    public class SqmContentsInvalidException : Exception
    {
        public SqmContentsInvalidException(Exception innerException) : base("SQM file contents are invalid", innerException)
        {
        }
    }
}
