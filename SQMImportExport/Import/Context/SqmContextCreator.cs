using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Import.HelperFunctions;

namespace SQMImportExport.Import.Context
{
    internal class SqmContextCreator : ISqmContextCreator
    {
        private readonly ParsingHelperFunctions _parsingHelperFunctions;

        public SqmContextCreator()
        {
            _parsingHelperFunctions = new ParsingHelperFunctions();
        }

        public SqmContext CreateRootContext(List<string> contextText)
        {
            var context = new SqmContext();

            ReadInsideContext(contextText, context);

            return context;
        }

        public SqmContext CreateContext(List<string> contextText)
        {
            var context = new SqmContext();

            context.Header = contextText[0];

            ReadInsideContext(contextText.Skip(2).ToList(), context);

            return context;
        }

        private void ReadInsideContext(List<string> contextText, SqmContext context)
        {
            for (int i = 0; i < contextText.Count - 1; i++)
            {
                var currentLine = contextText[i];
                var nextLine = contextText[i + 1];

                if (_parsingHelperFunctions.IsLineStartOfContext(nextLine))
                {
                    var linesForSubContext = contextText.Skip(i).ToList();
                    var getContextResult = GetContextText(linesForSubContext);

                    i += getContextResult.EndLineNumberInParentContext;
                    var subContext = CreateContext(getContextResult.ContextText);

                    context.SubContexts.Add(subContext);
                }
                else
                {
                    context.Lines.Add(new SqmLine(currentLine));
                }
            }
        }

        private GetContextResult GetContextText(List<string> parentContextText)
        {
            var lineIndex = 2;
            var endOfContextsToSkip = 0;

            for (; lineIndex < parentContextText.Count; lineIndex++)
            {
                var currentLine = parentContextText[lineIndex];

                if(_parsingHelperFunctions.IsLineStartOfContext(currentLine))
                {
                    endOfContextsToSkip += 1;
                }

                if (_parsingHelperFunctions.IsLineEndOfContext(currentLine))
                {
                    if(endOfContextsToSkip == 0)
                    {
                        break;                        
                    }

                    endOfContextsToSkip -= 1;
                }
            }

            return new GetContextResult(parentContextText.Take(lineIndex + 1).ToList(), lineIndex);
        }

        private class GetContextResult
        {
            public GetContextResult(List<string> contextText, int endLineNumberInParentContext)
            {
                ContextText = contextText;
                EndLineNumberInParentContext = endLineNumberInParentContext;
            }

            public List<string> ContextText { get; set; }
            public int EndLineNumberInParentContext { get; set; }
        }
    }
}