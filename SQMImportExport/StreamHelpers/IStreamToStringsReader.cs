using System.Collections.Generic;
using System.IO;

namespace SQMImportExport.StreamHelpers
{
    internal interface IStreamToStringsReader
    {
        List<string> Read(Stream stream);
    }
}