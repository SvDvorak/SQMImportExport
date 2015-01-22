using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import.ArmA2.Sensor;
using SQMImportExport.Import.Context;

namespace SQMImportExport.Tests.Import.ArmA2
{
    [TestFixture]
    public class SensorItemParserTests
    {
        private SensorItemParser _parser;

        private readonly List<string> completeSimpleSensorItemText = new List<string>
            {
                "class Item0\n",
                "{\n",
                "position[]={414,16,413};\n",
                "a=4.4;\n",
                "b=3.3;\n",
                "angle=20.8573;\n",
                "rectangular=1;\n",
                "activationBy=\"ANY\";\n",
                "activationType=\"NOT PRESENT\";\n",
                "repeating=1\n",
                "timeoutMin=30;\n",
                "timeoutMid=31;\n",
                "timeoutMax=32;\n",
                "interruptable=1;\n",
                "type=\"SWITCH\";\n",
                "age=\"UNKNOWN\";\n",
                "text=\"targetClear\";\n",
                "name=\"END\";\n",
                "idStatic=859;\n",
                "idVehicle=795;\n",
                "idObject=-127;\n",
                "expCond=\"!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)\";\n",
                "expActiv=\"myEnd = [1] execVM \"f\\server\\f_mpEndBroadcast.sqf\";\";\n",
                "expDesactiv=\"a whole bunch of text\";\n",
                "synchronizations[]={0,1};",
                "};"
            };

        private SqmContext _completeSimpleSensorItemContext;
        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _parser = new SensorItemParser();
            _contextCreator = new SqmContextCreator();

            _completeSimpleSensorItemContext = _contextCreator.CreateContext(completeSimpleSensorItemText);
        }

        [Test]
        public void Expect_parser_to_parse_all_sensor_item_properties()
        {
            var sensorResult = _parser.ParseContext(_completeSimpleSensorItemContext);

            Assert.AreEqual(0, sensorResult.Number);
            Assert.AreEqual(414, sensorResult.Position.X);
            Assert.AreEqual(413, sensorResult.Position.Y);
            Assert.AreEqual(16, sensorResult.Position.Z);
            Assert.AreEqual(4.4, sensorResult.A);
            Assert.AreEqual(3.3, sensorResult.B);
            Assert.AreEqual(20.8573, sensorResult.Angle);
            Assert.AreEqual(1, sensorResult.Rectangular);
            Assert.AreEqual("ANY", sensorResult.ActivationBy);
            Assert.AreEqual("NOT PRESENT", sensorResult.ActivationType);
            Assert.AreEqual(1, sensorResult.Repeating);
            Assert.AreEqual(30, sensorResult.TimeoutMin);
            Assert.AreEqual(31, sensorResult.TimeoutMid);
            Assert.AreEqual(32, sensorResult.TimeoutMax);
            Assert.AreEqual(1, sensorResult.Interruptable);
            Assert.AreEqual("SWITCH", sensorResult.Type);
            Assert.AreEqual("UNKNOWN", sensorResult.Age);
            Assert.AreEqual("targetClear", sensorResult.Text);
            Assert.AreEqual("END", sensorResult.Name);
            Assert.AreEqual(859, sensorResult.IdStatic);
            Assert.AreEqual(795, sensorResult.IdVehicle);
            Assert.AreEqual(-127, sensorResult.IdObject);
            Assert.AreEqual(@"!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)",
                sensorResult.ExpCond);
            Assert.AreEqual(@"myEnd = [1] execVM ""f\server\f_mpEndBroadcast.sqf"";", sensorResult.ExpActiv);
            Assert.AreEqual(@"a whole bunch of text", sensorResult.ExpDesactiv);
            Assert.AreEqual(0, sensorResult.Synchronizations[0]);
            Assert.AreEqual(1, sensorResult.Synchronizations[1]);
        }

        [Test]
        public void Effects_are_parsed_in_sensor()
        {
            var context = _contextCreator.CreateContext(new List<string>
                {
                    "class Item0",
                    "{\n",
                    "class Effects\n",
                    "{\n",
                    "};\n",
                    "};\n"
                });

            var sensor = _parser.ParseContext(context);
            
            Assert.IsEmpty(sensor.Effects);
        }
    }
}