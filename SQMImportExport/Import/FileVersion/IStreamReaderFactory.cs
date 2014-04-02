using System.IO;

namespace SQMImportExport.Import.FileVersion
{
    internal interface IStreamReaderFactory
    {
        IStreamReaderAdapter Create(Stream stream);
    }
}