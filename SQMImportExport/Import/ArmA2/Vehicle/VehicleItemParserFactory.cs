namespace SQMImportExport.Import.ArmA2.Vehicle
{
    internal class VehicleItemParserFactory : IItemParserFactory<SQMImportExport.ArmA2.Vehicle>
    {
        public IParser<SQMImportExport.ArmA2.Vehicle> CreateParser()
        {
            return new VehicleItemParser();
        }
    }
}