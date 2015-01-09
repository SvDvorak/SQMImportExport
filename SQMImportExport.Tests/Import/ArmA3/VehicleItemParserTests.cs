﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SQMImportExport.Import;
using SQMImportExport.Import.ArmA3.Vehicle;
using SQMImportExport.Import.Context;

namespace SQMImportExport.Tests.Import.ArmA3
{
    [TestFixture]
    public class VehicleItemParserTests
    {
        private VehicleItemParser _parser;

        private readonly List<string> completeSimpleGroupItemText = new List<string>
            {
                "class Item5\n",
                "{\n",
                "presence=0.256;",
                "position[]={5533.8467,143.18413,6350.1045};\n",
                "placement=60;\n",
                "azimut=17.206261;\n",
                "offsetY=0.5;\n",
                "special=\"NONE\";\n",
                "id=4;\n",
                "side=\"WEST\";\n",
                "vehicle=\"US_Soldier_TL_EP1\";\n",
                "isUAV=1;\n",
                "player=\"PLAY CDG\";\n",
                "forceHeadlessClient=1;\n",
                "leader=1;\n",
                "rank=\"CORPORAL\";\n",
                "skill=0.60000002;\n",
                "lock=\"UNLOCKED\";\n",
                "health=0.99000001;",
                "ammo=1.1;\n",
                "text=\"UnitUS_Alpha_FTL\";\n",
                "init=\"GrpUS_Alpha = group this; nul = [\"ftl\",this] execVM \"f\\common\\folk_assignGear.sqf\";\";\n",
                "description=\"US Army Alpha Fireteam Leader\";\n",
                "synchronizations[]={116,117};\n",
                "};"
            };

        private readonly List<string> completeComplexGroupItemText = new List<string>
            {
                "class Item4",
                "{",
                "side=\"WEST\";",
                "class Vehicles",
                "{",
                "items=4;",
                "class Item0",
                "{",
                "text=\"UnitUS_Bravo_FTL\";",
                "};",
                "class Item1",
                "{",
                "text=\"UnitUS_Bravo_AR\";",
                "};",
                "class Item2",
                "{",
                "text=\"UnitUS_Bravo_AAR\";",
                "};",
                "class Item3",
                "{",
                "text=\"UnitUS_Bravo_Eng\";",
                "};",
                "};"
            };

        private readonly List<string> itemWithWaypointsText = new List<string>
            {
                "class Item0",
                "{",
                "side=\"EAST\";",
                "class Waypoints",
                "{",
                "items=2;",
                "class Item0",
                "{",
                "showWP=\"DISMISS\";",
                "};",
                "class Item1",
                "{",
                "showWP=\"STOP\";",
                "};",
                "};",
            };

        private SqmContextCreator _contextCreator;

        private SqmContext _completeSimpleGroupItemContext;
        private SqmContext _completeComplexGroupItemContext;
        private SqmContext _itemWithWaypointsContext;

        [SetUp]
        public void Setup()
        {
            _parser = new VehicleItemParser();

            _contextCreator = new SqmContextCreator();

            _completeSimpleGroupItemContext = _contextCreator.CreateContext(completeSimpleGroupItemText);
            _completeComplexGroupItemContext = _contextCreator.CreateContext(completeComplexGroupItemText);
            _itemWithWaypointsContext = _contextCreator.CreateContext(itemWithWaypointsText);
        }

        [Test]
        public void Expect_is_item_to_return_true_on_correct_item_element_syntax()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Item0", "{\n", "};\n" });

            var isItemElement = _parser.IsCorrectContext(context);

            Assert.IsTrue(isItemElement);
        }

        [Test]
        public void Expect_is_item_to_return_false_on_incorrect_item_element_syntax()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Markers", "{\n", "};\n" });

            var isItemElement = _parser.IsCorrectContext(context);

            Assert.IsFalse(isItemElement);
        }

        [Test]
        public void Expect_parser_to_parse_all_group_item_properties()
        {
            var itemResult = _parser.ParseContext(_completeSimpleGroupItemContext);

            Assert.AreEqual(5, itemResult.Number);
            Assert.AreEqual(0.256, itemResult.Presence);
            Assert.AreEqual(5533.8467, itemResult.Position.X);
            Assert.AreEqual(143.18413, itemResult.Position.Y);
            Assert.AreEqual(6350.1045, itemResult.Position.Z);
            Assert.AreEqual(60, itemResult.Placement);
            Assert.AreEqual(17.206261, itemResult.Azimut);
            Assert.AreEqual(0.5, itemResult.OffsetY);
            Assert.AreEqual("NONE", itemResult.Special);
            Assert.AreEqual(4, itemResult.Id);
            Assert.AreEqual("WEST", itemResult.Side);
            Assert.AreEqual("US_Soldier_TL_EP1", itemResult.VehicleName);
            Assert.AreEqual(1, itemResult.IsUAV);
            Assert.AreEqual("PLAY CDG", itemResult.Player);
            Assert.AreEqual(1, itemResult.ForceHeadlessClient);
            Assert.AreEqual(1, itemResult.Leader);
            Assert.AreEqual("CORPORAL", itemResult.Rank);
            Assert.AreEqual(0.60000002, itemResult.Skill);
            Assert.AreEqual("UNLOCKED", itemResult.Lock);
            Assert.AreEqual(0.99000001, itemResult.Health);
            Assert.AreEqual(1.1, itemResult.Ammo);
            Assert.AreEqual("UnitUS_Alpha_FTL", itemResult.Text);
            Assert.AreEqual(@"GrpUS_Alpha = group this; nul = [""ftl"",this] execVM ""f\common\folk_assignGear.sqf"";",
                itemResult.Init);
            Assert.AreEqual("US Army Alpha Fireteam Leader", itemResult.Description);
            Assert.AreEqual(116, itemResult.Synchronizations[0]);
            Assert.AreEqual(117, itemResult.Synchronizations[1]);
        }

        [Test]
        public void Expect_exception_if_property_not_found()
        {
            var inputText = new List<string>
                {
                    "class Item0",
                    "{",
                    "derpderp=\"herpderp\"",
                    "};"
                };

            var context = _contextCreator.CreateContext(inputText);

            Assert.Throws<SqmParseException>(() => _parser.ParseContext(context));
        }

        [Test]
        public void Expect_parser_to_parse_sub_items()
        {
            var inputText = new List<string>
                {
                    "class Item0",
                    "{",
                    "side=\"WEST\";",
                    "class Vehicles",
                    "{",
                    "items=1;",
                    "class Item0",
                    "{",
                    "text=\"SomeText\";",
                    "};",
                    "};",
                    "};"
                };

            var context = _contextCreator.CreateContext(inputText);

            var itemResult = _parser.ParseContext(context);

            Assert.AreEqual("SomeText", itemResult.Vehicles.First().Text);
        }

        [Test]
        public void Expect_parser_to_parse_complex_item_with_sub_items()
        {
            var itemResult = _parser.ParseContext(_completeComplexGroupItemContext);
            var vehicles = itemResult.Vehicles.ToList();

            Assert.AreEqual(4, vehicles.Count);
            Assert.AreEqual("UnitUS_Bravo_FTL", vehicles[0].Text);
            Assert.AreEqual("UnitUS_Bravo_Eng", vehicles[3].Text);
        }

        [Test]
        public void Empty_waypoints_when_waypoints_are_missing()
        {
            var itemResult = _parser.ParseContext(_completeComplexGroupItemContext);

            Assert.IsEmpty(itemResult.Waypoints);
        }

        [Test]
        public void Expect_parser_to_parse_waypoints()
        {
            var itemResult = _parser.ParseContext(_itemWithWaypointsContext);

            Assert.AreEqual("DISMISS", itemResult.Waypoints[0].ShowWp);
            Assert.AreEqual("STOP", itemResult.Waypoints[1].ShowWp);
        }
    }
}