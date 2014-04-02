using System.IO;

namespace SQMImportExport.StreamHelpers
{
    internal interface IStreamWriterFactory
    {
        IStreamWriterAdapter Create(Stream stream);
    }
}