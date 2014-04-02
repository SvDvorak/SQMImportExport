using System.Collections.Generic;
using SQMImportExport.Import.DataSetters;
using SQMImportExport.Import.DataSetters.Effects;

namespace SQMImportExport.Import.ArmA3.Sensor
{
    internal class SensorItemParser : ItemParserBase<SQMImportExport.ArmA3.Sensor>
    {
        public SensorItemParser()
        {
            var effectsParser = new EffectsParser();
            ContextSetters.Add(new ContextSetter<List<string>>(effectsParser, x => ParseResult.Effects = x));

            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new DoublePropertySetter("a", x => ParseResult.A = x));
            LineSetters.Add(new DoublePropertySetter("b", x => ParseResult.B = x));
            LineSetters.Add(new StringPropertySetter("activationBy", x => ParseResult.ActivationBy = x));
            LineSetters.Add(new StringPropertySetter("activationType", x => ParseResult.ActivationType = x));
            LineSetters.Add(new IntegerPropertySetter("interruptable", x => ParseResult.Interruptable = x));
            LineSetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            LineSetters.Add(new StringPropertySetter("age", x => ParseResult.Age = x));
            LineSetters.Add(new StringPropertySetter("expCond", x => ParseResult.ExpCond = x));
            LineSetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
        }
    }
}
