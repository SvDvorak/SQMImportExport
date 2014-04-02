using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import
{
    internal abstract class ParserBase<TParseResult> : IParser<TParseResult>
        where TParseResult : new()
    {
        public List<IContextSetter> ContextSetters { get; private set; }
        public List<LineSetterBase> LineSetters { get; private set; }

        protected TParseResult ParseResult { get; set; }

        protected abstract Regex HeaderRegex { get; }

        protected ParserBase()
        {
            ContextSetters = new List<IContextSetter>();
            LineSetters = new List<LineSetterBase>();
        }

        public bool IsCorrectContext(SqmContext context)
        {
            return context.IsHeaderMatch(HeaderRegex);
        }

        public virtual TParseResult ParseContext(SqmContext context)
        {
            ParseResult = new TParseResult();

            foreach (var subContext in context.SubContexts)
            {
                var parseResult = Result.Failure;

                foreach (var contextSetter in ContextSetters)
                {
                    parseResult = contextSetter.SetContextIfMatch(subContext);

                    if (parseResult == Result.Success)
                    {
                        break;
                    }
                }

                if (parseResult == Result.Failure)
                {
                    var resultTypeName = typeof(TParseResult).Name;
                    throw new SqmParseException(string.Format("Unknown context in {0}: {1}", resultTypeName, subContext.Header.Trim()));
                }
            }

            foreach (var line in context.Lines)
            {
                var parseResult = new Result();

                foreach (var lineSetter in LineSetters)
                {
                    parseResult = lineSetter.SetValueIfLineMatches(line);

                    if (parseResult == Result.Success)
                    {
                        break;
                    }
                }

                if (parseResult == Result.Failure)
                {
                    var resultTypeName = typeof(TParseResult).Name;
                    throw new SqmParseException(string.Format("Unknown line in {0}: {1}", resultTypeName, line.ToString().Trim()));
                }
            }

            return ParseResult;
        }
    }
}
