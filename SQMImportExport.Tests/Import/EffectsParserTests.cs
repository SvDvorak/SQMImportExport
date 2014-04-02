using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.DataSetters.Effects;

namespace SQMImportExport.Tests.Import
{
    [TestFixture]
    public class EffectsParserTests
    {
        private EffectsParser _sut;
        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _sut = new EffectsParser();
            _contextCreator = new SqmContextCreator();
        }

        [Test]
        public void Context_is_correct_when_passed_effects()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Effects", "{\n", "};\n" });
            var isCorrectContext = _sut.IsCorrectContext(context);

            Assert.IsTrue(isCorrectContext);
        }

        [Test]
        public void Context_is_incorrect_when_passed_something_other_than_effects()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Markers", "{\n", "};\n" });
            var isCorrectContext = _sut.IsCorrectContext(context);

            Assert.IsFalse(isCorrectContext);
        }

        [Test]
        public void Empty_list_when_effects_is_empty()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Effects", "{\n", "};\n" });
            var parsedEffects = _sut.ParseContext(context);

            Assert.IsEmpty(parsedEffects);
        }

        [Test]
        public void Reads_all_items_in_effects()
        {
            var context = _contextCreator.CreateContext(new List<string>
                {
                    "class Effects",
                    "{\n",
                    "one line with text",
                    "and another one",
                    "text=otherText",
                    "};\n"
                });

            var parsedEffects = _sut.ParseContext(context);

            Assert.AreEqual("one line with text", parsedEffects[0]);
            Assert.AreEqual("and another one", parsedEffects[1]);
            Assert.AreEqual("text=otherText", parsedEffects[2]);
        }
    }
}
