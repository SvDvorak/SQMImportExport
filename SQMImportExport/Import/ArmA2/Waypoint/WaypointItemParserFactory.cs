namespace SQMImportExport.Import.ArmA2.Waypoint
{
    internal class WaypointItemParserFactory : IItemParserFactory<SQMImportExport.ArmA2.Waypoint>
    {
        public IParser<SQMImportExport.ArmA2.Waypoint> CreateParser()
        {
            return new WaypointItemParser();
        }
    }
}