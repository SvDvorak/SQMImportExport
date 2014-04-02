using System.Text.RegularExpressions;

namespace SQMImportExport.Import.HelperFunctions
{
    internal class ParsingHelperFunctions
    {
        private readonly Regex _startBracketRegex = new Regex(@"^\s*\{\s*$", RegexOptions.Compiled);
        private readonly Regex _endBracketRegex = new Regex(@"^\s*\};\s*$", RegexOptions.Compiled);

        public bool IsLineStartOfContext(string line)
        {
            var startBracketMatch = _startBracketRegex.Match(line);

            return startBracketMatch.Success;
        }

        public bool IsLineEndOfContext(string line)
        {
            var endBracketMatch = _endBracketRegex.Match(line);

            return endBracketMatch.Success;
        }
    }
}
