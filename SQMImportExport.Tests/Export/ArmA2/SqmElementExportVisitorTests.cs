﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQMImportExport.ArmA2;
using SQMImportExport.Common;
using SQMImportExport.Export.ArmA2;

namespace SQMImportExport.Tests.Export.ArmA2
{
    [TestFixture]
    public class SqmElementExportVisitorTests
    {
        private SqmElementExportVisitor _exportVisitor;

        [SetUp]
        public void Setup()
        {
            _exportVisitor = new SqmElementExportVisitor();
        }

        [Test]
        public void Expect_empty_string_on_empty_file()
        {
            var sqmContents = new SqmContents();

            var exportedParseResult = _exportVisitor.Visit("file", sqmContents);

            Assert.AreEqual("", exportedParseResult);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complete_simple_file()
        {
            var originalParseResultText = new StringBuilder();

            originalParseResultText.Append("version=11;\n");
            originalParseResultText.Append("class Mission\n");
            originalParseResultText.Append("{\n");
            originalParseResultText.Append("};\n");
            originalParseResultText.Append("class Intro\n");
            originalParseResultText.Append("{\n");
            originalParseResultText.Append("};\n");
            originalParseResultText.Append("class OutroWin\n");
            originalParseResultText.Append("{\n");
            originalParseResultText.Append("};\n");
            originalParseResultText.Append("class OutroLoose\n");
            originalParseResultText.Append("{\n");
            originalParseResultText.Append("};\n");

            var sqmContents = new SqmContents();

            sqmContents.Version = 11;
            sqmContents.Mission = new MissionState();
            sqmContents.Intro = new MissionState();
            sqmContents.OutroWin = new MissionState();
            sqmContents.OutroLose = new MissionState();

            var exportedParseResult = _exportVisitor.Visit("file", sqmContents);

            Assert.AreEqual(originalParseResultText.ToString(), exportedParseResult);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complete_simple_mission()
        {
            var originalMissionText = new StringBuilder();

            originalMissionText.Append("class Mission\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("addOns[]=\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("\"cacharacters_e\",\n");
            originalMissionText.Append("\"Takistan\"\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("addOnsAuto[]=\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("\"ca_modules_functions\",\n");
            originalMissionText.Append("\"camisc3\"\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("randomSeed=4931020;\n");
            originalMissionText.Append("class Intel\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("briefingName=\"missionBriefing\";\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("class Groups\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("items=1;\n");
            originalMissionText.Append("class Item0\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("side=\"itemSide\";\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("class Vehicles\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("items=1;\n");
            originalMissionText.Append("class Item0\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("id=1;\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("class Markers\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("items=1;\n");
            originalMissionText.Append("class Item0\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("a=10;\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("class Sensors\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("items=1;\n");
            originalMissionText.Append("class Item0\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("b=10;\n");
            originalMissionText.Append("class Effects\n");
            originalMissionText.Append("{\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("};\n");
            originalMissionText.Append("};\n");

            var mission = new MissionState();

            mission.AddOns = new List<string> { "cacharacters_e", "Takistan" };
            mission.AddOnsAuto = new List<string> { "ca_modules_functions", "camisc3" };
            mission.RandomSeed = 4931020;
            mission.Intel = new Intel { BriefingName = "missionBriefing" };

            var missionGroupItem = new Vehicle { Number = 0, Side = "itemSide" };
            mission.Groups = new List<Vehicle> { missionGroupItem };

            var missionVehicleItem = new Vehicle { Number = 0, Id = 1 };
            mission.Vehicles = new List<Vehicle> { missionVehicleItem };

            var missionMarkerItem = new Marker { A = 10 };
            mission.Markers = new List<Marker> { missionMarkerItem };

            var missionSensorItem = new Sensor { B = 10 };
            mission.Sensors = new List<Sensor> { missionSensorItem };

            var exportedMission = _exportVisitor.Visit("Mission", mission);

            Assert.AreEqual(originalMissionText.ToString(), exportedMission);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complete_intel()
        {
            var originalIntelText = new StringBuilder();

            originalIntelText.Append("class Intel\n");
            originalIntelText.Append("{\n");
            originalIntelText.Append("briefingName=\"rootbeer\";\n");
            originalIntelText.Append("briefingDescription=\"stuffs\";\n");
            originalIntelText.Append("resistanceWest=0;\n");
            originalIntelText.Append("resistanceEast=1;\n");
            originalIntelText.Append("startWeather=0.25;\n");
            originalIntelText.Append("startFog=0.35;\n");
            originalIntelText.Append("forecastWeather=0.25;\n");
            originalIntelText.Append("forecastFog=0.45;\n");
            originalIntelText.Append("year=2008;\n");
            originalIntelText.Append("month=10;\n");
            originalIntelText.Append("day=11;\n");
            originalIntelText.Append("hour=8;\n");
            originalIntelText.Append("minute=1;\n");
            originalIntelText.Append("};\n");

            var intel = new Intel();

            intel.BriefingName = "rootbeer";
            intel.BriefingDescription = "stuffs";
            intel.ResistanceWest = 0;
            intel.ResistanceEast = 1;
            intel.StartWeather = 0.25;
            intel.StartFog = 0.35;
            intel.ForecastWeather = 0.25;
            intel.ForecastFog = 0.45;
            intel.Year = 2008;
            intel.Month = 10;
            intel.Day = 11;
            intel.Hour = 8;
            intel.Minute = 1;

            var exportedIntel = _exportVisitor.Visit("Intel", intel);

            Assert.AreEqual(originalIntelText.ToString(), exportedIntel);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complete_simple_vehicle()
        {
            var originalVehicleText = new StringBuilder();

            originalVehicleText.Append("class Item3\n");
            originalVehicleText.Append("{\n");
            originalVehicleText.Append("presence=1.19;\n");
            originalVehicleText.Append("presenceCondition=\"(ns_Waves==(ns_WavesLimit-2));\";\n");
            originalVehicleText.Append("position[]={10,12,14};\n");
            originalVehicleText.Append("placement=30;\n");
            originalVehicleText.Append("azimut=3.14;\n");
            originalVehicleText.Append("special=\"CARGO\";\n");
            originalVehicleText.Append("age=\"ACTUAL\";\n");
            originalVehicleText.Append("id=4;\n");
            originalVehicleText.Append("side=\"GUER\";\n");
            originalVehicleText.Append("vehicle=\"TK_GUE_Soldier_2_EP1\";\n");
            originalVehicleText.Append("player=\"PLAY CDG\";\n");
            originalVehicleText.Append("leader=1;\n");
            originalVehicleText.Append("lock=\"UNLOCKED\";\n");
            originalVehicleText.Append("rank=\"CORPORAL\";\n");
            originalVehicleText.Append("skill=0.60000002;\n");
            originalVehicleText.Append("health=0.45;\n");
            originalVehicleText.Append("fuel=1.1;\n");
            originalVehicleText.Append("ammo=2.2;\n");
            originalVehicleText.Append("text=\"UnitGUE_MTR1_AG\";\n");
            originalVehicleText.Append("markers[]=\n");
            originalVehicleText.Append("{\n");
            originalVehicleText.Append("\"as_1\",\n");
            originalVehicleText.Append("\"as_2\"\n");
            originalVehicleText.Append("};\n");
            originalVehicleText.Append(
                "init=\"GrpGUE_MTR1 = group this; nul = [\"mtrag\",this] execVM \"f\\common\\folk_assignGear.sqf\";\";\n");
            originalVehicleText.Append("description=\"TK Local Mortar Team 1 Assistant Gunner\";\n");
            originalVehicleText.Append("synchronizations[]={1,2,3};\n");
            originalVehicleText.Append("};\n");

            var vehicle = new Vehicle();
            vehicle.Number = 3;
            vehicle.Presence = 1.19;
            vehicle.PresenceCondition = "(ns_Waves==(ns_WavesLimit-2));";
            vehicle.Position = new Vector(10, 12, 14);
            vehicle.Placement = 30;
            vehicle.Azimut = 3.14;
            vehicle.Special = "CARGO";
            vehicle.Age = "ACTUAL";
            vehicle.Id = 4;
            vehicle.Side = "GUER";
            vehicle.VehicleName = "TK_GUE_Soldier_2_EP1";
            vehicle.Player = "PLAY CDG";
            vehicle.Leader = 1;
            vehicle.Lock = "UNLOCKED";
            vehicle.Rank = "CORPORAL";
            vehicle.Skill = 0.60000002;
            vehicle.Health = 0.45;
            vehicle.Fuel = 1.1;
            vehicle.Ammo = 2.2;
            vehicle.Text = "UnitGUE_MTR1_AG";
            vehicle.Markers = new MarkersArray() { Items = new List<string> { "as_1", "as_2" } };
            vehicle.Init = "GrpGUE_MTR1 = group this; nul = [\"mtrag\",this] execVM \"f\\common\\folk_assignGear.sqf\";";
            vehicle.Description = "TK Local Mortar Team 1 Assistant Gunner";
            vehicle.Synchronizations = new List<int> { 1, 2, 3 };

            var actualVehicleText = _exportVisitor.Visit("Item", vehicle);

            Assert.AreEqual(originalVehicleText.ToString(), actualVehicleText);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complex_vehicle()
        {
            var originalItemsText = new StringBuilder();
            originalItemsText.Append("class Item3\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("id=4;\n");
            originalItemsText.Append("class Vehicles\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("items=2;\n");
            originalItemsText.Append("class Item4\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("id=5;\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("class Item5\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("id=6;\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("};\n");

            var exportVisitor = new SqmElementExportVisitor();

            var item1 = new Vehicle();
            item1.Number = 3;
            item1.Id = 4;

            var item1_1 = new Vehicle();
            item1_1.Number = 4;
            item1_1.Id = 5;
            var item1_2 = new Vehicle();
            item1_2.Number = 5;
            item1_2.Id = 6;

            var item1Vehicles = item1.Vehicles.ToList();
            item1Vehicles.Add(item1_1);
            item1Vehicles.Add(item1_2);
            item1.Vehicles = item1Vehicles;

            var actualItemsText = exportVisitor.Visit("Item", item1);

            Assert.AreEqual(originalItemsText.ToString(), actualItemsText);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_vehicle_with_waypoints()
        {
            var originalItemsText = new StringBuilder();
            originalItemsText.Append("class Item0\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("class Waypoints\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("items=2;\n");
            originalItemsText.Append("class Item0\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("class Effects\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("class Item1\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("class Effects\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("};\n");

            var exportVisitor = new SqmElementExportVisitor();

            var vehicle = new Vehicle();
            vehicle.Waypoints.Add(new Waypoint { Number = 0 });
            vehicle.Waypoints.Add(new Waypoint { Number = 1 });

            var actualItemsText = exportVisitor.Visit("Item", vehicle);

            Assert.AreEqual(originalItemsText.ToString(), actualItemsText);
        }

        [Test]
        public void Expect_exporter_to_successfull_export_complete_waypoint()
        {
            var originalItemsText = new StringBuilder();
            originalItemsText.Append("class Item0\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("position[]={4083.6555,25.784687,11750.772};\n");
            originalItemsText.Append("placement=100;\n");
            originalItemsText.Append("completitionRadius=150;\n");
            originalItemsText.Append("id=101;\n");
            originalItemsText.Append("idStatic=70594;\n");
            originalItemsText.Append("idObject=-1662;\n");
            originalItemsText.Append("housePos=5;\n");
            originalItemsText.Append("type=\"DISMISS\";\n");
            originalItemsText.Append("combatMode=\"RED\";\n");
            originalItemsText.Append("formation=\"FILE\";\n");
            originalItemsText.Append("speed=\"LIMITED\";\n");
            originalItemsText.Append("combat=\"SAFE\";\n");
            originalItemsText.Append("description=\"Secure the Compound\";\n");
            originalItemsText.Append("expCond=\"obj_done;\";\n");
            originalItemsText.Append("expActiv=\"op_h1;\";\n");
            originalItemsText.Append("visible=1;\n");
            originalItemsText.Append("synchronizations[]={3,4};\n");
            originalItemsText.Append("class Effects\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("titleEffect=\"PLAIN DOWN\";\n");
            originalItemsText.Append("otherText\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("timeoutMin=300;\n");
            originalItemsText.Append("timeoutMid=301;\n");
            originalItemsText.Append("timeoutMax=302;\n");
            originalItemsText.Append("showWP=\"NEVER\";\n");
            originalItemsText.Append("};\n");

            var exportVisitor = new SqmElementExportVisitor();

            var waypoint = new Waypoint
                {
                    Number = 0,
                    Position = new Vector(4083.6555, 25.784687, 11750.772),
                    Placement = 100,
                    CompletitionRadius = 150,
                    Id = 101,
                    IdStatic = 70594,
                    IdObject = -1662,
                    HousePos = 5,
                    Type = "DISMISS",
                    CombatMode = "RED",
                    Formation = "FILE",
                    Speed = "LIMITED",
                    Combat = "SAFE",
                    Description = "Secure the Compound",
                    ExpCond = "obj_done;",
                    ExpActiv = "op_h1;",
                    Visible = 1,
                    Synchronizations = new List<int> { 3, 4 },
                    Effects = new List<string> { "titleEffect=\"PLAIN DOWN\";", "otherText" },
                    TimeoutMin = 300,
                    TimeoutMid = 301,
                    TimeoutMax = 302,
                    ShowWp = "NEVER"
                };

            var actualItemsText = exportVisitor.Visit("Item", waypoint);

            Assert.AreEqual(originalItemsText.ToString(), actualItemsText);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complete_marker()
        {
            var originalMarkerText = new StringBuilder();

            originalMarkerText.Append("class Item4\n");
            originalMarkerText.Append("{\n");
            originalMarkerText.Append("position[]={10,12,14};\n");
            originalMarkerText.Append("name=\"mkrInsertion\";\n");
            originalMarkerText.Append("text=\"INSERTION\";\n");
            originalMarkerText.Append("markerType=\"RECTANGLE\";\n");
            originalMarkerText.Append("type=\"EMPTY\";\n");
            originalMarkerText.Append("colorName=\"ColorRed\";\n");
            originalMarkerText.Append("fillName=\"FDiagonal\";\n");
            originalMarkerText.Append("a=4.5;\n");
            originalMarkerText.Append("b=5.5;\n");
            originalMarkerText.Append("angle=2.42;\n");
            originalMarkerText.Append("drawBorder=1;\n");
            originalMarkerText.Append("};\n");

            var marker = new Marker();

            marker.Number = 4;
            marker.Position = new Vector(10, 12, 14);
            marker.Name = "mkrInsertion";
            marker.Text = "INSERTION";
            marker.MarkerType = "RECTANGLE";
            marker.Type = "EMPTY";
            marker.ColorName = "ColorRed";
            marker.FillName = "FDiagonal";
            marker.A = 4.5;
            marker.B = 5.5;
            marker.Angle = 2.42;
            marker.DrawBorder = 1;

            var actualMarkerText = _exportVisitor.Visit("Item", marker);

            Assert.AreEqual(originalMarkerText.ToString(), actualMarkerText);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complete_sensor()
        {
            var originalSensorText = new StringBuilder();

            originalSensorText.Append("class Item5\n");
            originalSensorText.Append("{\n");
            originalSensorText.Append("position[]={10,12,14};\n");
            originalSensorText.Append("a=4.5;\n");
            originalSensorText.Append("b=5.5;\n");
            originalSensorText.Append("angle=20.3;\n");
            originalSensorText.Append("rectangular=1;\n");
            originalSensorText.Append("activationBy=\"ANY\";\n");
            originalSensorText.Append("activationType=\"ANY TYPE\";\n");
            originalSensorText.Append("repeating=1;\n");
            originalSensorText.Append("timeoutMin=30;\n");
            originalSensorText.Append("timeoutMid=31;\n");
            originalSensorText.Append("timeoutMax=32;\n");
            originalSensorText.Append("interruptable=1;\n");
            originalSensorText.Append("type=\"EMPTY\";\n");
            originalSensorText.Append("age=\"UNKNOWN\";\n");
            originalSensorText.Append("idVehicle=795;\n");
            originalSensorText.Append("text=\"targetClear\";\n");
            originalSensorText.Append("name=\"END\";\n");
            originalSensorText.Append("idStatic=794;\n");
            originalSensorText.Append("idObject=796;\n");
            originalSensorText.Append("expCond=\"checkpoint3NrOfClearedDT == 7\";\n");
            originalSensorText.Append("expActiv=\"end = [1] execVM \"f\\server\\f_mpEndBroadcast.sqf\";\";\n");
            originalSensorText.Append("expDesactiv=\"some code stuffs\";\n");
            originalSensorText.Append("class Effects\n");
            originalSensorText.Append("{\n");
            originalSensorText.Append("titleEffect=\"PLAIN DOWN\";\n");
            originalSensorText.Append("otherText\n");
            originalSensorText.Append("};\n");
            originalSensorText.Append("synchronizations[]={5,4};\n");
            originalSensorText.Append("};\n");

            var sensor = new Sensor();

            sensor.Number = 5;
            sensor.Position = new Vector(10, 12, 14);
            sensor.A = 4.5;
            sensor.B = 5.5;
            sensor.Angle = 20.3;
            sensor.Rectangular = 1;
            sensor.ActivationBy = "ANY";
            sensor.ActivationType = "ANY TYPE";
            sensor.Repeating = 1;
            sensor.TimeoutMin = 30;
            sensor.TimeoutMid = 31;
            sensor.TimeoutMax = 32;
            sensor.Interruptable = 1;
            sensor.Type = "EMPTY";
            sensor.Age = "UNKNOWN";
            sensor.IdVehicle = 795;
            sensor.Text = "targetClear";
            sensor.Name = "END";
            sensor.IdStatic = 794;
            sensor.IdObject = 796;
            sensor.ExpCond = "checkpoint3NrOfClearedDT == 7";
            sensor.ExpActiv = "end = [1] execVM \"f\\server\\f_mpEndBroadcast.sqf\";";
            sensor.ExpDesactiv = "some code stuffs";
            sensor.Effects = new List<string> { "titleEffect=\"PLAIN DOWN\";", "otherText" };
            sensor.Synchronizations = new List<int> { 5, 4 };

            var actualSensorText = _exportVisitor.Visit("Item", sensor);

            Assert.AreEqual(originalSensorText.ToString(), actualSensorText);
        }
    }
}