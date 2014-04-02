using System.Collections.Generic;
using System.Linq;

namespace SQMReorderer.Tests
{
    public static class IEnumerableExtensions
    {
        public static T Second<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Skip(1).First();
        }
    }
}
