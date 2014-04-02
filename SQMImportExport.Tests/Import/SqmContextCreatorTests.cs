using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import.Context;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class SqmContextCreatorTests
    {
        private SqmContextCreator _contextCreator;

        [SetUp]
        public void Setup()
        {
            _contextCreator = new SqmContextCreator();
        }

        [Test]
        public void Expect_no_exception_on_correct_context_syntax()
        {
            var inputText = new List<string>() { "header\n", "{\n", "property=5;\n", "};\n" };

            _contextCreator.CreateContext(inputText);
        }

        [Test]
        public void Expect_only_header_on_simple_empty_context()
        {
            var inputText = new List<string>() { "header\n", "{\n", "};\n" };

            var context = _contextCreator.CreateContext(inputText);

            Assert.AreEqual(0, context.Lines.Count);
            Assert.AreEqual(0, context.SubContexts.Count);
            Assert.AreEqual("header\n", context.Header);
        }

        [Test]
        public void Expect_normal_lines_in_context_to_be_added()
        {
            var inputText = new List<string>() { "header\n", "{\n", "poorBoy=1;\n", "poorFamily=2;\n", "willYouDo=\"Fandango\";\n", "};\n" };

            var context = _contextCreator.CreateContext(inputText);

            Assert.AreEqual("poorBoy=1;\n", context.Lines[0].ToString());
            Assert.AreEqual("poorFamily=2;\n", context.Lines[1].ToString());
            Assert.AreEqual("willYouDo=\"Fandango\";\n", context.Lines[2].ToString());
        }

        [Test]
        public void Expect_empty_sub_context_to_be_added()
        {
            var inputText = new List<string>()
                {
                    "header\n",
                    "{\n",
                    "subContext\n",
                    "{\n",
                    "};\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            Assert.AreEqual(1, context.SubContexts.Count);
        }

        [Test]
        public void Expect_sub_context_with_lines_to_be_added_with_lines()
        {
            var inputText = new List<string>()
                {
                    "header\n",
                    "{\n",
                    "subContext\n",
                    "{\n",
                    "thunderstorms=\"lightning\";\n",
                    "veryVery=\"frightening\";\n",
                    "};\n",
                    "};\n"
                };

            var context = _contextCreator.CreateContext(inputText);

            Assert.AreEqual("thunderstorms=\"lightning\";\n", context.SubContexts[0].Lines[0].ToString());
            Assert.AreEqual("veryVery=\"frightening\";\n", context.SubContexts[0].Lines[1].ToString());
        }

        [Test]
        public void Expect_empty_root_context_to_give_empty_context()
        {
            var context = _contextCreator.CreateRootContext(new List<string>());

            Assert.AreEqual(null, context.Header);
            Assert.AreEqual(0, context.Lines.Count);
            Assert.AreEqual(0, context.SubContexts.Count);
        }

        [Test]
        public void Expect_root_context_with_lines_and_subcontexts_to_be_read_correctly()
        {
            var inputText = new List<string>()
                {
                    "version=11;\n",
                    "class Best\n",
                    "{\n",
                    "player=1;\n",
                    "};\n",
                    "class Worst\n",
                    "{\n",
                    "player=2;\n",
                    "};\n"
                };

            var contextOutput = _contextCreator.CreateRootContext(inputText);

            var rootContext = new SqmContext();
            rootContext.Header = null;
            rootContext.Lines.Add(new SqmLine("version=11;\n"));

            var bestContext = new SqmContext();
            bestContext.Header = "class Best\n";
            bestContext.Lines.Add(new SqmLine("player=1;\n"));

            var worstContext = new SqmContext();
            worstContext.Header = "class Worst\n";
            worstContext.Lines.Add(new SqmLine("player=2;\n"));

            rootContext.SubContexts.Add(bestContext);
            rootContext.SubContexts.Add(worstContext);

            AssertSameContexts(rootContext, contextOutput);
        }

        [Test]
        public void Expect_complex_context_with_lines_and_subcontexts_to_be_read_correctly()
        {
            var inputText = new List<string>()
                {
                    "music\n",
                    "{\n",
                        "bob\n",
                        "{\n",
                            "rainbow=\"country\";\n",
                            "marley\n",
                            "{\n",
                                "sunIsShining=\"theWeatherIsSweet\";\n",
                                "wantYouToKnow=\"imARainbowToo\";\n",
                            "};\n",
                            "gotMyOwn=\"promiseLand\";\n",
                        "};\n",
                        "funkstar\n",
                        "{\n",
                            "de\n",
                            "{\n",
                                "giveMeDays=\"allMydays\";\n",
                                "luxe\n",
                                "{\n",
                                    "sound=11;\n",
                                "};\n",
                            "};\n",
                            "beats=14;\n",
                        "};\n",
                    "};\n"
                };

            var contextOutput = _contextCreator.CreateContext(inputText);

            var musicContext = new SqmContext();
            musicContext.Header = "music\n";

            var bobContext = new SqmContext();
            bobContext.Header = "bob\n";
            bobContext.Lines.Add(new SqmLine("rainbow=\"country\";\n"));
            bobContext.Lines.Add(new SqmLine("gotMyOwn=\"promiseLand\";\n"));

            var marleyContext = new SqmContext();
            marleyContext.Header = "marley\n";
            marleyContext.Lines.Add(new SqmLine("sunIsShining=\"theWeatherIsSweet\";\n"));
            marleyContext.Lines.Add(new SqmLine("wantYouToKnow=\"imARainbowToo\";\n"));

            var funkstarContext = new SqmContext();
            funkstarContext.Header = "funkstar\n";
            funkstarContext.Lines.Add(new SqmLine("beats=14;\n"));

            var deContext = new SqmContext();
            deContext.Header = "de\n";
            deContext.Lines.Add(new SqmLine("giveMeDays=\"allMydays\";\n"));

            var luxeContext = new SqmContext();
            luxeContext.Header = "luxe\n";
            luxeContext.Lines.Add(new SqmLine("sound=11;\n"));

            musicContext.SubContexts.Add(bobContext);
            musicContext.SubContexts.Add(funkstarContext);
            bobContext.SubContexts.Add(marleyContext);
            funkstarContext.SubContexts.Add(deContext);
            deContext.SubContexts.Add(luxeContext);

            AssertSameContexts(musicContext, contextOutput);
        }

        private void AssertSameContexts(SqmContext expectedContext, SqmContext actualContext)
        {
            Assert.AreEqual(expectedContext.Header, actualContext.Header);
            Assert.AreEqual(expectedContext.Lines.Count, actualContext.Lines.Count);
            Assert.AreEqual(expectedContext.SubContexts.Count, actualContext.SubContexts.Count);

            CollectionAssert.AreEqual(expectedContext.Lines, actualContext.Lines);

            for (int i = 0; i < expectedContext.SubContexts.Count; i++)
            {
                AssertSameContexts(expectedContext.SubContexts[i], actualContext.SubContexts[i]);
            }
        }
    }
}
