using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMImportExport.Import.Context;

namespace SQMImportExport.Import.DataSetters
{
    internal class MultiLineStringListPropertySetter : IContextSetter
    {
        private readonly Regex _propertyNameRegex;
        private readonly Regex _listStringRegex;

        private readonly Action<List<string>> _propertySetter;

        public MultiLineStringListPropertySetter(string propertyName, Action<List<string>> propertySetter)
        {
            _propertyNameRegex = new Regex(propertyName + @"\[\]\=");
            _listStringRegex = new Regex(@"[\d\w_]+");

            _propertySetter = propertySetter;
        }

        public Result SetContextIfMatch(SqmContext context)
        {
            var isHeaderCorrectName = context.IsHeaderMatch(_propertyNameRegex);

            if(isHeaderCorrectName)
            {
                var propertyStrings = new List<string>();

                foreach (var line in context.Lines)
                {
                    line.Match(_listStringRegex, x => propertyStrings.Add(x.Value));
                }

                _propertySetter(propertyStrings);

                return Result.Success;
            }
            return Result.Failure;
        }
    }
}
