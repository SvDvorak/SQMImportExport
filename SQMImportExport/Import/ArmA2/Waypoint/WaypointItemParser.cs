using System.Collections.Generic;
using SQMImportExport.Import.DataSetters;
using SQMImportExport.Import.DataSetters.Effects;

namespace SQMImportExport.Import.ArmA2.Waypoint
{
    internal class WaypointItemParser : ItemParserBase<SQMImportExport.ArmA2.Waypoint>
    {
        public WaypointItemParser()
        {
            var effectsParser = new EffectsParser();
            ContextSetters.Add(new ContextSetter<List<string>>(effectsParser, x => ParseResult.Effects = x));

            LineSetters.Add(new IntegerPropertySetter("id", x => ParseResult.Id = x));
            LineSetters.Add(new IntegerPropertySetter("idStatic", x => ParseResult.IdStatic = x));
            LineSetters.Add(new IntegerPropertySetter("idObject", x => ParseResult.IdObject = x));
            LineSetters.Add(new IntegerPropertySetter("housePos", x => ParseResult.HousePos = x));
            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new IntegerPropertySetter("placement", x => ParseResult.Placement = x));
            LineSetters.Add(new IntegerPropertySetter("completitionRadius", x => ParseResult.CompletitionRadius = x));
            LineSetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            LineSetters.Add(new StringPropertySetter("combatMode", x => ParseResult.CombatMode = x));
            LineSetters.Add(new StringPropertySetter("formation", x => ParseResult.Formation = x));
            LineSetters.Add(new StringPropertySetter("speed", x => ParseResult.Speed = x));
            LineSetters.Add(new StringPropertySetter("combat", x => ParseResult.Combat = x));
            LineSetters.Add(new StringPropertySetter("description", x => ParseResult.Description = x));
            LineSetters.Add(new IntegerPropertySetter("visible", x => ParseResult.Visible = x));
            LineSetters.Add(new StringPropertySetter("expCond", x => ParseResult.ExpCond = x));
            LineSetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
            LineSetters.Add(new IntegerListPropertySetter("synchronizations", x => ParseResult.Synchronizations = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMin", x => ParseResult.TimeoutMin = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMid", x => ParseResult.TimeoutMid = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMax", x => ParseResult.TimeoutMax = x));
            LineSetters.Add(new StringPropertySetter("showWP", x => ParseResult.ShowWp = x));
        }
    }
}