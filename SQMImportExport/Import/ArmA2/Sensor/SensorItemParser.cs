using System.Collections.Generic;
using SQMImportExport.Import.DataSetters;
using SQMImportExport.Import.DataSetters.Effects;

namespace SQMImportExport.Import.ArmA2.Sensor
{
    internal class SensorItemParser : ItemParserBase<SQMImportExport.ArmA2.Sensor>
    {
        public SensorItemParser()
        {
            var effectsParser = new EffectsParser();
            ContextSetters.Add(new ContextSetter<List<string>>(effectsParser, x => ParseResult.Effects = x));

            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new DoublePropertySetter("a", x => ParseResult.A = x));
            LineSetters.Add(new DoublePropertySetter("b", x => ParseResult.B = x));
            LineSetters.Add(new DoublePropertySetter("angle", x => ParseResult.Angle = x));
            LineSetters.Add(new IntegerPropertySetter("rectangular", x => ParseResult.Rectangular = x));
            LineSetters.Add(new StringPropertySetter("activationBy", x => ParseResult.ActivationBy = x));
            LineSetters.Add(new StringPropertySetter("activationType", x => ParseResult.ActivationType = x));
            LineSetters.Add(new IntegerPropertySetter("repeating", x => ParseResult.Repeating = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMin", x => ParseResult.TimeoutMin = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMid", x => ParseResult.TimeoutMid = x));
            LineSetters.Add(new IntegerPropertySetter("timeoutMax", x => ParseResult.TimeoutMax = x));
            LineSetters.Add(new IntegerPropertySetter("interruptable", x => ParseResult.Interruptable = x));
            LineSetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            LineSetters.Add(new StringPropertySetter("age", x => ParseResult.Age = x));
            LineSetters.Add(new StringPropertySetter("text", x => ParseResult.Text = x));
            LineSetters.Add(new StringPropertySetter("name", x => ParseResult.Name = x));
            LineSetters.Add(new IntegerPropertySetter("idStatic", x => ParseResult.IdStatic = x));
            LineSetters.Add(new IntegerPropertySetter("idVehicle", x => ParseResult.IdVehicle = x));
            LineSetters.Add(new IntegerPropertySetter("idObject", x => ParseResult.IdObject = x));
            LineSetters.Add(new StringPropertySetter("expCond", x => ParseResult.ExpCond = x));
            LineSetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
            LineSetters.Add(new StringPropertySetter("expDesactiv", x => ParseResult.ExpDesactiv = x));
            LineSetters.Add(new IntegerListPropertySetter("synchronizations", x => ParseResult.Synchronizations = x));
        }
    }
}