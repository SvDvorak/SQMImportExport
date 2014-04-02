using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SQMImportExport.Import.Context
{
    internal class SqmContext
    {
        public string Header { get; set; }

        public List<SqmLine> Lines { get; set; }
        public List<SqmContext> SubContexts { get; set; }

        public SqmContext()
        {
            Lines = new List<SqmLine>();
            SubContexts = new List<SqmContext>();
        }

        public bool IsHeaderMatch(Regex headerRegex)
        {
            var match = headerRegex.Match(Header);

            return match.Success;
        }

        public void MatchHeader(Regex headerRegex, Action<Match> setMatch)
        {
            var match = headerRegex.Match(Header);

            if (match.Success)
            {
                setMatch(match);
            }
        }
    }
}