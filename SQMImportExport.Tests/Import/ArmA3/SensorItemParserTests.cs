using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import.ArmA3.Sensor;
using SQMImportExport.Import.Context;

namespace SQMImportExport.Tests.Import.ArmA3
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
                "activationBy=\"ANY\";\n",
                "activationType=\"NOT PRESENT\";\n",
                "interruptable=1;\n",
                "type=\"SWITCH\";\n",
                "age=\"UNKNOWN\";\n",
                "expCond=\"!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)\";\n",
                "expActiv=\"myEnd = [1] execVM \"f\\server\\f_mpEndBroadcast.sqf\";\";\n",
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
            Assert.AreEqual(16, sensorResult.Position.Y);
            Assert.AreEqual(413, sensorResult.Position.Z);
            Assert.AreEqual(4.4, sensorResult.A);
            Assert.AreEqual(3.3, sensorResult.B);
            Assert.AreEqual("ANY", sensorResult.ActivationBy);
            Assert.AreEqual("NOT PRESENT", sensorResult.ActivationType);
            Assert.AreEqual(1, sensorResult.Interruptable);
            Assert.AreEqual("SWITCH", sensorResult.Type);
            Assert.AreEqual("UNKNOWN", sensorResult.Age);
            Assert.AreEqual(@"!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)",
                sensorResult.ExpCond);
            Assert.AreEqual(@"myEnd = [1] execVM ""f\server\f_mpEndBroadcast.sqf"";", sensorResult.ExpActiv);
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