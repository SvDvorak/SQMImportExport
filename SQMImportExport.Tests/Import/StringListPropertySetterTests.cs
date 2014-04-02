using System.Collections.Generic;
using NUnit.Framework;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Tests.Import
{
    [TestFixture]
    public class StringListPropertySetterTests
    {
        [Test]
        public void Property_is_not_matching_when_name_is_different()
        {
            var actualList = new List<string>();
            var sut = new StringListPropertySetter("empty", x => actualList = x);

            var setResult = sut.SetValueIfLineMatches(new SqmLine("property[]={\"stuff\"};"));

            Assert.AreEqual(Result.Failure, setResult);
            Assert.IsEmpty(actualList);
        }

        [Test]
        public void Property_is_not_matching_when_contents_is_incorrect()
        {
            var actualList = new List<string>();
            var sut = new StringListPropertySetter("property", x => actualList = x);

            var setResult = sut.SetValueIfLineMatches(new SqmLine("property[]={aa,bbb,c};"));

            Assert.AreEqual(Result.Failure, setResult);
            Assert.IsEmpty(actualList);
        }

        [Test]
        public void Values_are_set_when_property_name_is_matching()
        {
            var actualList = new List<string>();
            var sut = new StringListPropertySetter("property", x => actualList = x);

            var setResult = sut.SetValueIfLineMatches(new SqmLine("property[]={\"1\",\"2\",\"3\"};"));

            Assert.AreEqual(Result.Success, setResult);
            Assert.AreEqual("1", actualList[0]);
            Assert.AreEqual("2", actualList[1]);
            Assert.AreEqual("3", actualList[2]);
        }
    }
}
