namespace SQMImportExport.Import.ArmA3.Marker
{
    internal class MarkerItemParserFactory : IItemParserFactory<SQMImportExport.ArmA3.Marker>
    {
        public IParser<SQMImportExport.ArmA3.Marker> CreateParser()
        {
            return new MarkerItemParser();
        }
    }
}