using System.Collections.Generic;

namespace SQMImportExport.Import.DataSetters.Effects
{
    internal class EffectsParserFactory : IItemParserFactory<List<string>>
    {
        public IParser<List<string>> CreateParser()
        {
            return new EffectsParser();
        }
    }
}