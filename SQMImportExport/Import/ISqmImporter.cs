using System.IO;
using SQMImportExport.Common;

namespace SQMImportExport.Import
{
    public interface ISqmImporter
    {
        SqmContentsBase Import(Stream stream);
    }
}