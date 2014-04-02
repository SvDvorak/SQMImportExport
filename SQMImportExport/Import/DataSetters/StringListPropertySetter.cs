using System;
using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Import.HelperFunctions;

namespace SQMImportExport.Import.DataSetters
{
    internal class StringListPropertySetter : MultiValuePropertySetterBase<List<string>>
    {
        public StringListPropertySetter(string propertyName, Action<List<string>> propertySetter)
            : base(propertyName, "\"" + CommonRegexPatterns.NonSpacedTextPattern + "\"", propertySetter)
        {
        }

        protected override void SetPropertyValues(List<string> values)
        {
            PropertySetter(values.Select(x => x.TrimStart('"').TrimEnd('"')).ToList());
        }
    }
}