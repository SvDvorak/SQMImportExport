using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMImportExport.Import.ArmA2.Intel;
using SQMImportExport.Import.ArmA2.Marker;
using SQMImportExport.Import.ArmA2.Sensor;
using SQMImportExport.Import.ArmA2.Vehicle;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import.ArmA2.MissionState
{
    internal class MissionStateParser : ParserBase<SQMImportExport.ArmA2.MissionState>
    {
        private readonly Regex _missionStateHeaderRegex;

        public MissionStateParser(string missionStateHeader)
        {
            _missionStateHeaderRegex = new Regex(@"class\s+" + missionStateHeader, RegexOptions.Compiled);

            var groupsParser = new ItemListParser<SQMImportExport.ArmA2.Vehicle>(new VehicleItemParserFactory(), "Groups");
            var vehiclesParser = new ItemListParser<SQMImportExport.ArmA2.Vehicle>(new VehicleItemParserFactory(), "Vehicles");
            var markersParser = new ItemListParser<SQMImportExport.ArmA2.Marker>(new MarkerItemParserFactory(), "Markers");
            var sensorsParser = new ItemListParser<SQMImportExport.ArmA2.Sensor>(new SensorItemParserFactory(), "Sensors");

            ContextSetters.Add(new ContextSetter<SQMImportExport.ArmA2.Intel>(new IntelParser(), x => ParseResult.Intel = x));
            ContextSetters.Add(new ContextSetter<List<SQMImportExport.ArmA2.Vehicle>>(groupsParser, x => ParseResult.Groups = x));
            ContextSetters.Add(new ContextSetter<List<SQMImportExport.ArmA2.Vehicle>>(vehiclesParser, x => ParseResult.Vehicles = x));
            ContextSetters.Add(new ContextSetter<List<SQMImportExport.ArmA2.Marker>>(markersParser, x => ParseResult.Markers = x));
            ContextSetters.Add(new ContextSetter<List<SQMImportExport.ArmA2.Sensor>>(sensorsParser, x => ParseResult.Sensors = x));

            ContextSetters.Add(new MultiLineStringListPropertySetter("addOns", x => ParseResult.AddOns = x));
            ContextSetters.Add(new MultiLineStringListPropertySetter("addOnsAuto", x => ParseResult.AddOnsAuto = x));

            LineSetters.Add(new IntegerPropertySetter("randomSeed", x => ParseResult.RandomSeed = x));
        }

        protected override Regex HeaderRegex
        {
            get { return _missionStateHeaderRegex; }
        }
    }
}