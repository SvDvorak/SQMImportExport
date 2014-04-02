using System.IO;

namespace SQMImportExport.Import.FileVersion
{
    internal interface IFileVersionRetriever
    {
        FileVersion GetVersion(Stream stream);
    }
}