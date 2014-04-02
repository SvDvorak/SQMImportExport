using SQMImportExport.Import.Context;

namespace SQMImportExport.Import.DataSetters
{
    internal interface ILineSetter
    {
        Result SetValueIfLineMatches(SqmLine line);
    }
}