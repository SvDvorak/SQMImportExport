using System;
using SQMImportExport.Import.HelperFunctions;

namespace SQMImportExport.Import.DataSetters
{
    internal class IntegerPropertySetter : SingleValuePropertySetterBase<int>
    {
        public IntegerPropertySetter(string propertyName, Action<int> propertySetter)
            : base(propertyName, CommonRegexPatterns.IntegerPattern, propertySetter)
        {
        }

        protected override void SetValue(string value)
        {
            PropertySetter(Convert.ToInt32(value));
        }
    }
}