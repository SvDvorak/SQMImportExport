namespace SQMImportExport.Import.ArmA2.Sensor
{
    internal class SensorItemParserFactory : IItemParserFactory<SQMImportExport.ArmA2.Sensor>
    {
        public IParser<SQMImportExport.ArmA2.Sensor> CreateParser()
        {
            return new SensorItemParser();
        }
    }
}