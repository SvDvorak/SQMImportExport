using System.IO;
using SQMImportExport.Common;
using SQMImportExport.StreamHelpers;

namespace SQMImportExport.Export
{
    internal class SqmFileExporterFactory : ISqmFileExporterFactory
    {
        private readonly ArmA2.ISqmElementVisitor _arma2Exporter;
        private readonly ArmA3.ISqmElementVisitor _arma3Exporter;
        private readonly IContextIndenter _contextIndenter;

        internal SqmFileExporterFactory(
            ArmA2.ISqmElementVisitor arma2Exporter,
            ArmA3.ISqmElementVisitor arma3Exporter,
            IContextIndenter contextIndenter)
        {
            _arma2Exporter = arma2Exporter;
            _arma3Exporter = arma3Exporter;
            _contextIndenter = contextIndenter;
        }

        public SqmFileExporterFactory()
            : this(
            new ArmA2.SqmElementExportVisitor(),
            new ArmA3.SqmElementExportVisitor(),
            new ContextIndenter())
        {
        }

        public ISqmContentsVisitor Create(Stream stream)
        {
            return new SqmFileExporter(
                new StreamWriterAdapter(stream),
                _arma2Exporter,
                _arma3Exporter,
                _contextIndenter);
        }
    }
}