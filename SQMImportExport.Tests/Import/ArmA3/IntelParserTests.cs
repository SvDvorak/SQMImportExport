using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import;
using SQMImportExport.Import.ArmA3.Intel;
using SQMImportExport.Import.Context;

namespace SQMImportExport.Tests.Import.ArmA3
{
    [TestFixture]
    public class IntelParserTests
    {
        private IntelParser _parser;
        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _parser = new IntelParser();

            _contextCreator = new SqmContextCreator();
        }

        [Test]
        public void Expect_is_intel_to_return_true_on_correct_intel_element_syntax()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Intel\n", "{\n", "};\n" });

            var isItemElement = _parser.IsCorrectContext(context);

            Assert.IsTrue(isItemElement);
        }

        [Test]
        public void Expect_is_intel_to_return_false_on_incorrect_intel_element_syntax()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Markers", "{\n", "};\n" });

            var isItemElement = _parser.IsCorrectContext(context);

            Assert.IsFalse(isItemElement);
        }

        [Test]
        public void Expect_parser_to_parse_all_properties()
        {
            var inputText = new List<string>
                {
                    "class Intel\n",
                    "{\n",
                    "briefingName=\"[co04]local_hostility_v2_oa\";\n",
                    "overviewText=\"Destroy stolen ammocrates and truck\";",
                    "timeOfChanges=1800.0002;",
                    "startWeather=0.19207704;\n",
                    "startWind=1.14;",
                    "startWindDir=20;",
                    "startWaves=0.1;",
                    "startGust=0.099999994;",
                    "forecastWeather=0.25;\n",
                    "forecastWind=2.01;",
                    "forecastWaves=0.5;",
                    "forecastGust=0.099999994;",
                    "forecastWindDir=360;",
                    "forecastLightnings=0.1;",
                    "rainForced=1;",
                    "lightningsForced=1;",
                    "wavesForced=1;",
                    "windForced=1;",
                    "year=2008;\n",
                    "month=10;\n",
                    "day=11;\n",
                    "hour=16;\n",
                    "minute=0;\n",
                    "startFogBase=0.001;",
                    "forecastFogBase=0.002;",
                    "startFogDecay=0.0049333;",
                    "forecastFogDecay=0.0048333;",
                    "};\n",
                };

            var context = _contextCreator.CreateContext(inputText);

            var intelResult = _parser.ParseContext(context);

            Assert.AreEqual("[co04]local_hostility_v2_oa", intelResult.BriefingName);
            Assert.AreEqual("Destroy stolen ammocrates and truck", intelResult.OverviewText);
            Assert.AreEqual(1800.0002, intelResult.TimeOfChanges);
            Assert.AreEqual(0.19207704, intelResult.StartWeather);
            Assert.AreEqual(1.14, intelResult.StartWind);
            Assert.AreEqual(20, intelResult.StartWindDir);
            Assert.AreEqual(0.1, intelResult.StartWaves);
            Assert.AreEqual(0.099999994, intelResult.StartGust);
            Assert.AreEqual(0.25, intelResult.ForecastWeather);
            Assert.AreEqual(2.01, intelResult.ForecastWind);
            Assert.AreEqual(0.5, intelResult.ForecastWaves);
            Assert.AreEqual(0.099999994, intelResult.ForecastGust);
            Assert.AreEqual(360, intelResult.ForecastWindDir);
            Assert.AreEqual(0.1, intelResult.ForecastLightnings);
            Assert.AreEqual(1, intelResult.RainForced);
            Assert.AreEqual(1, intelResult.LightningsForced);
            Assert.AreEqual(1, intelResult.WavesForced);
            Assert.AreEqual(1, intelResult.WindForced);
            Assert.AreEqual(2008, intelResult.Year);
            Assert.AreEqual(10, intelResult.Month);
            Assert.AreEqual(11, intelResult.Day);
            Assert.AreEqual(16, intelResult.Hour);
            Assert.AreEqual(0, intelResult.Minute);
            Assert.AreEqual(0.001, intelResult.StartFogBase);
            Assert.AreEqual(0.002, intelResult.ForecastFogBase);
            Assert.AreEqual(0.0049333, intelResult.StartFogDecay);
            Assert.AreEqual(0.0048333, intelResult.ForecastFogDecay);
        }

        [Test]
        public void Expect_exception_if_property_not_found()
        {
            var inputText = new List<string>
                {
                    "class Intel",
                    "{",
                    "derpderp=\"herpderp\"",
                    "};"
                };

            var context = _contextCreator.CreateContext(inputText);

            Assert.Throws<SqmParseException>(() => _parser.ParseContext(context));
        }
    }
}