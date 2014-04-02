using System;

namespace SQMImportExport.Import
{
    public class SqmParseException : Exception
    {
        public SqmParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}