using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQMImportExport.Common;

namespace SQMImportExport.Export
{
    internal class SqmPropertyVisitor : ISqmPropertyVisitor
    {
        public string Visit(string propertyName, string value)
        {
            if (value == null)
            {
                return "";
            }

            return propertyName + "=\"" + value + "\";\n";
        }

        public string Visit(string propertyName, Vector value)
        {
            if(value == null)
            {
                return "";
            }

            return propertyName + "[]={" +
                value.X.ToStringInvariant() + "," +
                value.Z.ToStringInvariant() + "," +
                value.Y.ToStringInvariant() + "};\n";
        }

        public string Visit(string propertyName, int? nullableValue)
        {
            if (!nullableValue.HasValue)
            {
                return "";
            }

            return propertyName + "=" + nullableValue.Value + ";\n";
        }

        public string Visit(string propertyName, double? nullableValue)
        {
            if (!nullableValue.HasValue)
            {
                return "";
            }

            return propertyName + "=" + nullableValue.Value.ToStringInvariant() + ";\n";
        }

        public string Visit(string propertyName, List<int> intItems)
        {
            if (intItems == null || intItems.Count == 0)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(propertyName);
            stringBuilder.Append("[]={");

            for (var i = 0; i < intItems.Count; i++)
            {
                stringBuilder.Append(intItems[i]);

                var isLastItem = i != intItems.Count - 1;
                if (isLastItem)
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }

        public string Visit(string propertyName, List<string> stringItems)
        {
            if (stringItems == null || stringItems.Count == 0)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(propertyName);
            stringBuilder.Append("[]=\n{\n");

            for (var i = 0; i < stringItems.Count; i++)
            {
                stringBuilder.Append("\"");
                stringBuilder.Append(stringItems[i]);
                stringBuilder.Append("\"");

                var isLastItem = i != stringItems.Count - 1;
                if (isLastItem)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append("\n");
            }

            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }

        public string Visit(string propertyName, MarkersArray markers)
        {
            if (markers == null)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(markers.IsMarkersSingleLine
                ? GetMarkersAsSingleString(markers.Items)
                : Visit("markers", markers.Items));

            return stringBuilder.ToString();
        }

        private string GetMarkersAsSingleString(List<string> markers)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("markers[]={");

            if (markers.Count > 0)
            {
                for (var index = 0; index < markers.Count - 1; index++)
                {
                    var marker = markers[index];
                    stringBuilder.Append("\"" + marker + "\",");
                }
                stringBuilder.Append("\"" + markers.Last() + "\"");
            }

            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }

        public string VisitEffects(List<string> effects)
        {
            if (effects == null)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("class Effects\n");
            stringBuilder.Append("{\n");

            foreach (var effect in effects)
            {
                stringBuilder.Append(effect + "\n");
            }

            stringBuilder.Append("};\n");

            return stringBuilder.ToString();
        }
    }
}
