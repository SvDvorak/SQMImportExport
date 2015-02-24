using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SQMImportExport.Import.FileVersion
{
    internal class FileVersionRetriever : IFileVersionRetriever
    {
        private readonly IStreamReaderFactory _streamReaderFactory;

        private readonly Regex _versionRegex = new Regex(@"version\=(?<version>\d+)", RegexOptions.Compiled);

        public FileVersionRetriever(IStreamReaderFactory streamReaderFactory)
        {
            _streamReaderFactory = streamReaderFactory;
        }

        public FileVersion GetVersion(Stream stream)
        {
            var streamReader = _streamReaderFactory.Create(stream);

            var versionLine = streamReader.ReadLine();
            stream.Seek(0, SeekOrigin.Begin);

            var version = MatchVersion(versionLine);

            return GetVersion(version);
        }

        private FileVersion GetVersion(int version)
        {
            switch (version)
            {
                case 11:
                    return FileVersion.ArmA2;
                case 12:
                    return FileVersion.ArmA3;
            }

            throw new SqmIncorrectVersionException(version);
        }

        private int MatchVersion(string versionLine)
        {
            var match = _versionRegex.Match(versionLine);
            var versionGroup = match.Groups["version"];

            if (!versionGroup.Success)
            {
				throw new SqmMissingVersionException();
            }

            return Convert.ToInt32(versionGroup.Value);
        }
    }
}