using System.IO;

namespace SQMImportExport.Import.FileVersion
{
    internal class StreamReaderFactory : IStreamReaderFactory
    {
        public IStreamReaderAdapter Create(Stream stream)
        {
            return new StreamReaderAdapter(stream);
        }
    }
}