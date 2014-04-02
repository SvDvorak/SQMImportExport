using System.Globalization;

namespace SQMImportExport
{
    internal static class DoubleExtensions
    {
        public static string ToStringInvariant(this double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
