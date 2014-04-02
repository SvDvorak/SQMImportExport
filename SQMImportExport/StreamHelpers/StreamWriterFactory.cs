using System.IO;

namespace SQMImportExport.StreamHelpers
{
    internal class StreamWriterFactory : IStreamWriterFactory
    {
        public IStreamWriterAdapter Create(Stream stream)
        {
            return new StreamWriterAdapter(stream);
        }
    }
}