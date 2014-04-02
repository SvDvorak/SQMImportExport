using System.Collections.Generic;
using SQMImportExport.Common;
using SQMImportExport.Import.ArmA2.Waypoint;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import.ArmA2.Vehicle
{
    internal class VehicleItemParser : ItemParserBase<SQMImportExport.ArmA2.Vehicle>
    {
        public VehicleItemParser()
        {
            var vehiclesParser = new ItemListParser<SQMImportExport.ArmA2.Vehicle>(new VehicleItemParserFactory(), "Vehicles");
            ContextSetters.Add(new ContextSetter<List<SQMImportExport.ArmA2.Vehicle>>(vehiclesParser, x => ParseResult.Vehicles = x));

            var waypointsParser = new ItemListParser<SQMImportExport.ArmA2.Waypoint>(new WaypointItemParserFactory(), "Waypoints");
            ContextSetters.Add(new ContextSetter<List<SQMImportExport.ArmA2.Waypoint>>(waypointsParser, x => ParseResult.Waypoints = x));

            LineSetters.Add(new DoublePropertySetter("presence", x => ParseResult.Presence = x));
            LineSetters.Add(new StringPropertySetter("presenceCondition", x => ParseResult.PresenceCondition = x));
            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new IntegerPropertySetter("placement", x => ParseResult.Placement = x));
            LineSetters.Add(new DoublePropertySetter("azimut", x => ParseResult.Azimut = x));
            LineSetters.Add(new StringPropertySetter("special", x => ParseResult.Special = x));
            LineSetters.Add(new StringPropertySetter("age", x => ParseResult.Age = x));
            LineSetters.Add(new IntegerPropertySetter("id", x => ParseResult.Id = x));
            LineSetters.Add(new StringPropertySetter("side", x => ParseResult.Side = x));
            LineSetters.Add(new StringPropertySetter("vehicle", x => ParseResult.VehicleName = x));
            LineSetters.Add(new StringPropertySetter("player", x => ParseResult.Player = x));
            LineSetters.Add(new IntegerPropertySetter("leader", x => ParseResult.Leader = x));
            LineSetters.Add(new StringPropertySetter("rank", x => ParseResult.Rank = x));
            LineSetters.Add(new DoublePropertySetter("skill", x => ParseResult.Skill = x));
            LineSetters.Add(new StringPropertySetter("lock", x => ParseResult.Lock = x));
            LineSetters.Add(new DoublePropertySetter("health", x => ParseResult.Health = x));
            LineSetters.Add(new DoublePropertySetter("fuel", x => ParseResult.Fuel = x));
            LineSetters.Add(new DoublePropertySetter("ammo", x => ParseResult.Ammo = x));
            LineSetters.Add(new StringPropertySetter("text", x => ParseResult.Text = x));
            LineSetters.Add(new StringListPropertySetter("markers", SetSingleLineMarkers));
            LineSetters.Add(new StringPropertySetter("init", x => ParseResult.Init = x));
            LineSetters.Add(new StringPropertySetter("description", x => ParseResult.Description = x));
            LineSetters.Add(new IntegerListPropertySetter("synchronizations", x => ParseResult.Synchronizations = x));

            ContextSetters.Add(new MultiLineStringListPropertySetter("markers", SetMultiLineMarkers));
        }

        private void SetMultiLineMarkers(List<string> markers)
        {
            var markersArray = new MarkersArray
                {
                    Items = markers,
                    IsMarkersSingleLine = false
                };

            ParseResult.Markers = markersArray;
        }

        private void SetSingleLineMarkers(List<string> markers)
        {
            var markersArray = new MarkersArray
                {
                    Items = markers,
                    IsMarkersSingleLine = true
                };

            ParseResult.Markers = markersArray;
        }
    }
}
