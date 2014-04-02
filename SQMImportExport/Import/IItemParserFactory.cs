namespace SQMImportExport.Import
{
    // The item parser factory is required because we need to instantiate item parsers when they're used.
    // If they are instatiated in the constructor we will run into infinite loops.
    internal interface IItemParserFactory<TParseResult>
    {
        IParser<TParseResult> CreateParser();
    }
}
