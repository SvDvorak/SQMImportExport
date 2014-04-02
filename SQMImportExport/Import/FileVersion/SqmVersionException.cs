using System;

namespace SQMImportExport.Import.FileVersion
{
    public class SqmVersionException : Exception
    {
        public SqmVersionException(string message) : base(message)
        {
        }
    }

    public class SqmIncorrectVersionException : SqmVersionException
    {
        public SqmIncorrectVersionException(int version) : base("Version " + version + " is unknown")
        {
        }
    }

    public class SqmMissingVersionException : SqmVersionException
    {
        public SqmMissingVersionException() : base("Unable to find version field")
        {
        }
    }
}