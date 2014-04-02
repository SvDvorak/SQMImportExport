using System.IO;
using SQMImportExport.Common;

namespace SQMImportExport.Import
{
    internal interface ISqmFileImporter
    {
        SqmContentsBase Import(Stream fileStream);
    }
}