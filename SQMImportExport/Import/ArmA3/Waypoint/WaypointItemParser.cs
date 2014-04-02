using System.Collections.Generic;
using SQMImportExport.Import.DataSetters;
using SQMImportExport.Import.DataSetters.Effects;

namespace SQMImportExport.Import.ArmA3.Waypoint
{
    internal class WaypointItemParser : ItemParserBase<SQMImportExport.ArmA3.Waypoint>
    {
        public WaypointItemParser()
        {
            var effectsParser = new EffectsParser();
            ContextSetters.Add(new ContextSetter<List<string>>(effectsParser, x => ParseResult.Effects = x));

            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new IntegerPropertySetter("placement", x => ParseResult.Placement = x));
            LineSetters.Add(new IntegerPropertySetter("completitionRadius", x => ParseResult.CompletitionRadius = x));
            LineSetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            LineSetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMin", x => ParseResult.TimeoutMin = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMid", x => ParseResult.TimeoutMid = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMax", x => ParseResult.TimeoutMax = x));
            LineSetters.Add(new StringPropertySetter("showWP", x => ParseResult.ShowWp = x));
        }
    }
}