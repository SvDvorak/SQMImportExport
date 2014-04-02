using System;

namespace SQMImportExport.Import.DataSetters
{
    internal class StringLineSetter : LineSetterBase
    {
        private readonly Action<string> _lineSetter;

        public StringLineSetter(string linePattern, Action<string> lineSetter) : base(linePattern)
        {
            _lineSetter = lineSetter;
        }

        protected override void SetValue(string value)
        {
            _lineSetter(value);
        }
    }
}