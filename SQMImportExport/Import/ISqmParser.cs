using SQMImportExport.Common;
using SQMImportExport.Import.Context;

namespace SQMImportExport.Import
{
    internal interface ISqmParser
    {
        SqmContentsBase ParseContext(SqmContext context);
    }
}