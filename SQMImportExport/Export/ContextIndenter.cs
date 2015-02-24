using System.Text;

namespace SQMImportExport.Export
{
    internal interface IContextIndenter
    {
        string Indent(string text);
    }

    internal class ContextIndenter : IContextIndenter
    {
        public string Indent(string text)
        {
            var builder = new StringBuilder();
            var indentationLevel = 0;

            for (var index = 0; index < text.Length; index++)
            {
                var currentCharacter = text[index];
                var nextCharacter = GetNextCharacterIfPossible(text, index);
                builder.Append(currentCharacter);

                indentationLevel = UpdateIndentation(indentationLevel, currentCharacter, nextCharacter);

                if(ShouldIndent(currentCharacter))
                {
                    AddIndentation(indentationLevel, builder);
                }
            }

            return builder.ToString();
        }

        private bool ShouldIndent(char currentCharacter)
        {
            return IsNewLine(currentCharacter);
        }

        private char? GetNextCharacterIfPossible(string text, int index)
        {
            if (index < text.Length - 1)
            {
                return text[index + 1];
            }

            return null;
        }

        private int UpdateIndentation(int currentIndentation, char currentCharacter, char? nextCharacter)
        {
            var newIndentation = currentIndentation;

            if (nextCharacter.HasValue && IsEndOfContext(nextCharacter.Value))
            {
                newIndentation -= 1;
            }

            if (IsStartOfContext(currentCharacter))
            {
                newIndentation += 1;
            }

            return newIndentation;
        }

        private void AddIndentation(int indentationLevel, StringBuilder builder)
        {
            for (var i = 0; i < indentationLevel; i++)
            {
                builder.Append('\t');
            }
        }

        private bool IsEndOfContext(char nextCharacter)
        {
            return nextCharacter == '}';
        }

        private bool IsStartOfContext(char currentCharacter)
        {
            return currentCharacter == '{';
        }

        private bool IsNewLine(char currentCharacter)
        {
            return currentCharacter == '\n';
        }
    }
}