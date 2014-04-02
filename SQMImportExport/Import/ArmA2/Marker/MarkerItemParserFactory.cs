namespace SQMImportExport.Import.ArmA2.Marker
{
    internal class MarkerItemParserFactory : IItemParserFactory<SQMImportExport.ArmA2.Marker>
    {
        public IParser<SQMImportExport.ArmA2.Marker> CreateParser()
        {
            return new MarkerItemParser();
        }
    }
}