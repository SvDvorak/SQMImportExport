namespace SQMImportExport.Import.ArmA3.Vehicle
{
    internal class VehicleItemParserFactory : IItemParserFactory<SQMImportExport.ArmA3.Vehicle>
    {
        public IParser<SQMImportExport.ArmA3.Vehicle> CreateParser()
        {
            return new VehicleItemParser();
        }
    }
}