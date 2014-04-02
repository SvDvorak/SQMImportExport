using SQMImportExport.Import.Context;

namespace SQMImportExport.Import.DataSetters
{
    internal interface IContextSetter
    {
        Result SetContextIfMatch(SqmContext context);
    }
}