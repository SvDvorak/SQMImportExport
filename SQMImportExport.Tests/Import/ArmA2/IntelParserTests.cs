using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import;
using SQMImportExport.Import.ArmA2.Intel;
using SQMImportExport.Import.Context;

namespace SQMReorderer.Tests.Import.ArmA2
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
                    "briefingDescription=\"Destroy stolen ammocrates and truck\";\n",
                    "resistanceWest=0;",
                    "resistanceEast=1;",
                    "startWeather=0.19207704;\n",
                    "startFog=0.6482;\n",
                    "forecastWeather=0.25;\n",
                    "forecastFog=0.8379;\n",
                    "year=2008;\n",
                    "month=10;\n",
                    "day=11;\n",
                    "hour=16;\n",
                    "minute=0;\n",
                    "};\n",
                };

            var context = _contextCreator.CreateContext(inputText);

            var intelResult = _parser.ParseContext(context);

            Assert.AreEqual("[co04]local_hostility_v2_oa", intelResult.BriefingName);
            Assert.AreEqual("Destroy stolen ammocrates and truck", intelResult.BriefingDescription);
            Assert.AreEqual(0, intelResult.ResistanceWest);
            Assert.AreEqual(1, intelResult.ResistanceEast);
            Assert.AreEqual(0.19207704, intelResult.StartWeather);
            Assert.AreEqual(0.6482, intelResult.StartFog);
            Assert.AreEqual(0.25, intelResult.ForecastWeather);
            Assert.AreEqual(0.8379, intelResult.ForecastFog);
            Assert.AreEqual(2008, intelResult.Year);
            Assert.AreEqual(10, intelResult.Month);
            Assert.AreEqual(11, intelResult.Day);
            Assert.AreEqual(16, intelResult.Hour);
            Assert.AreEqual(0, intelResult.Minute);
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