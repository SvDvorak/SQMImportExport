using System.IO;
using SQMImportExport.Common;

namespace SQMImportExport.Export
{
    public interface ISqmExporter
    {
        void Export(Stream stream, SqmContentsBase contents);
    }
}