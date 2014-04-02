namespace SQMImportExport.Import.ArmA3.Sensor
{
    internal class SensorItemParserFactory : IItemParserFactory<SQMImportExport.ArmA3.Sensor>
    {
        public IParser<SQMImportExport.ArmA3.Sensor> CreateParser()
        {
            return new SensorItemParser();
        }
    }
}