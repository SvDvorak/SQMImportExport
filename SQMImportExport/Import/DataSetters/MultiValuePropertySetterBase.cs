using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SQMImportExport.Import.DataSetters
{
    internal abstract class MultiValuePropertySetterBase<T> : LineSetterBase
    {
        protected Action<T> PropertySetter { get; private set; }

        private Regex _valueRegex;

        protected MultiValuePropertySetterBase(string propertyName, string valuePattern, Action<T> propertySetter)
            : base(propertyName + @"\[\]\=\{(?<value>(" + valuePattern + @",?)*)\}")
        {
            PropertySetter = propertySetter;

            _valueRegex = new Regex(valuePattern);
        }

        protected override void SetValue(string value)
        {
            var match = _valueRegex.Match(value);

            var values = new List<string>();

            while(match.Success)
            {
                values.Add(match.Value);

                match = match.NextMatch();
            }

            SetPropertyValues(values);
        }

        protected abstract void SetPropertyValues(List<string> values);
    }
}