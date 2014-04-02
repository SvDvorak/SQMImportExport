using SQMImportExport.Import.Context;

namespace SQMImportExport.Import
{
    internal interface IParser<TParseResult>
    {
        bool IsCorrectContext(SqmContext context);
        TParseResult ParseContext(SqmContext context);
    }
}