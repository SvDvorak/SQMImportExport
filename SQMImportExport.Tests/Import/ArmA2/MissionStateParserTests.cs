using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import.ArmA2.MissionState;
using SQMImportExport.Import.Context;

namespace SQMImportExport.Tests.Import.ArmA2
{
    [TestFixture]
    public class MissionStateParserTests
    {
        private readonly MissionStateParser _missionStateParser = new MissionStateParser("Mission");

        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _contextCreator = new SqmContextCreator();
        }

        [Test]
        public void Expect_empty_mission_to_return_empty_result()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var missionState = _missionStateParser.ParseContext(context);

            Assert.AreEqual(0, missionState.Groups.Count);
        }

        [Test]
        public void Expect_intel_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "class Intel\n",
                    "{\n",
                    "};\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var missionState = _missionStateParser.ParseContext(context);

            Assert.IsNotNull(missionState.Intel);
        }

        [Test]
        public void Expect_groups_to_be_parsed_in_mission()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "class Groups\n",
                    "{\n",
                    "items=1;\n",
                    "class Item0\n",
                    "{\n",
                    "side=\"LOGIC\";\n",
                    "};\n",
                    "};\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var missionState = _missionStateParser.ParseContext(context);

            Assert.AreEqual(1, missionState.Groups.Count);
        }

        [Test]
        public void Expect_groups_to_be_parsed_irregardless_of_mission_content_order()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "addOns[]=\n",
                    "{\n",
                    "\"zargabad\",\n",
                    "};\n",
                    "class Groups\n",
                    "{\n",
                    "items=1;\n",
                    "class Item0\n",
                    "{\n",
                    "side=\"LOGIC\";\n",
                    "};\n",
                    "};\n",
                    "randomSeed=4931020;\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var missionState = _missionStateParser.ParseContext(context);

            Assert.AreEqual(1, missionState.Groups.Count);
        }

        [Test]
        public void Expect_vehicles_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "class Vehicles\n",
                    "{\n",
                    "items=3;\n",
                    "class Item0\n",
                    "{\n",
                    "text=\"SupplyTruck\";\n",
                    "};\n",
                    "class Item1\n",
                    "{\n",
                    "text=\"AmmoBox1\";\n",
                    "};\n",
                    "class Item2\n",
                    "{\n",
                    "text=\"AmmoBox2\";\n",
                    "};\n",
                    "};\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var missionState = _missionStateParser.ParseContext(context);

            Assert.AreEqual(3, missionState.Vehicles.Count);

            Assert.AreEqual("SupplyTruck", missionState.Vehicles[0].Text);
            Assert.AreEqual("AmmoBox1", missionState.Vehicles[1].Text);
            Assert.AreEqual("AmmoBox2", missionState.Vehicles[2].Text);
        }

        [Test]
        public void Expect_markers_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "class Markers\n",
                    "{\n",
                    "items=3;\n",
                    "class Item0\n",
                    "{\n",
                    "a=1;\n",
                    "};\n",
                    "class Item1\n",
                    "{\n",
                    "a=2;\n",
                    "};\n",
                    "class Item2\n",
                    "{\n",
                    "a=3;\n",
                    "};\n",
                    "};\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var missionState = _missionStateParser.ParseContext(context);

            Assert.AreEqual(3, missionState.Markers.Count);

            Assert.AreEqual(1, missionState.Markers[0].A);
            Assert.AreEqual(2, missionState.Markers[1].A);
            Assert.AreEqual(3, missionState.Markers[2].A);
        }

        [Test]
        public void Expect_sensors_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "class Sensors\n",
                    "{\n",
                    "items=2;\n",
                    "class Item0\n",
                    "{\n",
                    "type=\"SWITCH1\";\n",
                    "};\n",
                    "class Item1\n",
                    "{\n",
                    "type=\"SWITCH2\";\n",
                    "};\n",
                    "};\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var missionState = _missionStateParser.ParseContext(context);

            Assert.AreEqual(2, missionState.Sensors.Count);

            Assert.AreEqual("SWITCH1", missionState.Sensors[0].Type);
            Assert.AreEqual("SWITCH2", missionState.Sensors[1].Type);
        }

        [Test]
        public void Expect_all_mission_properties_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "addOns[]=\n",
                    "{\n",
                    "\"cacharacters_e\",\n",
                    "\"zargabad\",\n",
                    "\"ca_highcommand\",\n",
                    "\"cacharacters2\",\n",
                    "\"CAWheeled_E\"\n",
                    "};\n",
                    "addOnsAuto[]=\n",
                    "{\n",
                    "\"ca_modules_functions\",\n",
                    "\"cacharacters_e\",\n",
                    "\"CAWheeled_E\",\n",
                    "};\n",
                    "randomSeed=4931020);\n",
                    "}\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            var missionState = _missionStateParser.ParseContext(context);

            Assert.AreEqual(5, missionState.AddOns.Count);
            Assert.AreEqual(3, missionState.AddOnsAuto.Count);

            Assert.AreEqual("cacharacters_e", missionState.AddOns[0]);
            Assert.AreEqual("zargabad", missionState.AddOns[1]);
            Assert.AreEqual("ca_highcommand", missionState.AddOns[2]);
            Assert.AreEqual("cacharacters2", missionState.AddOns[3]);
            Assert.AreEqual("CAWheeled_E", missionState.AddOns[4]);
            
            Assert.AreEqual("ca_modules_functions", missionState.AddOnsAuto[0]);
            Assert.AreEqual("cacharacters_e", missionState.AddOnsAuto[1]);
            Assert.AreEqual("CAWheeled_E", missionState.AddOnsAuto[2]);

            Assert.AreEqual(4931020, missionState.RandomSeed);
        }
    }
}
