namespace SQMImportExport.Import.ArmA3.Waypoint
{
    internal class WaypointItemParserFactory : IItemParserFactory<SQMImportExport.ArmA3.Waypoint>
    {
        public IParser<SQMImportExport.ArmA3.Waypoint> CreateParser()
        {
            return new WaypointItemParser();
        }
    }
}