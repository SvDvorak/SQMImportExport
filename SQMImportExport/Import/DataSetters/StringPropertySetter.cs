using System;

namespace SQMImportExport.Import.DataSetters
{
    internal class StringPropertySetter : SingleValuePropertySetterBase<string>
    {
        public StringPropertySetter(string propertyName, Action<string> propertySetter)
            : base(propertyName, @""".*""", propertySetter)
        {
        }

        protected override void SetValue(string value)
        {
            value = value.Substring(1, value.Length - 2);

            PropertySetter(value.Trim());
        }
    }
}