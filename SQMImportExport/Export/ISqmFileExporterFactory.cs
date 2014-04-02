using System.IO;
using SQMImportExport.Common;

namespace SQMImportExport.Export
{
    internal interface ISqmFileExporterFactory
    {
        ISqmContentsVisitor Create(Stream stream);
    }
}