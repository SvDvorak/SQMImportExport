using System;
using System.Text.RegularExpressions;

namespace SQMImportExport.Import.Context
{
    internal class SqmLine
    {
        public SqmLine(string lineText)
        {
            LineText = lineText;
        }

        private string LineText { get; set; }

        public Result Match(Regex lineRegex, Action<Match> setMatch)
        {
            var match = lineRegex.Match(LineText);

            if (match.Success)
            {
                setMatch(match);

                return Result.Success;
            }

            return Result.Failure;
        }

        public bool IsMatch(Regex lineRegex)
        {
            var match = lineRegex.Match(LineText);

            return match.Success;
        }

        public override bool Equals(object obj)
        {
            return LineText == obj.ToString();
        }

        public override int GetHashCode()
        {
            return LineText.GetHashCode();
        }

        public override string ToString()
        {
            return LineText;
        }
    }
}