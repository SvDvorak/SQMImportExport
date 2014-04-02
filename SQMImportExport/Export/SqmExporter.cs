using System.IO;
using SQMImportExport.Common;

namespace SQMImportExport.Export
{
    public class SqmExporter : ISqmExporter
    {
        private readonly ISqmFileExporterFactory _exporterFactory;

        internal SqmExporter(ISqmFileExporterFactory exporterFactory)
        {
            _exporterFactory = exporterFactory;
        }

        public SqmExporter()
            : this(
                new SqmFileExporterFactory(
                    new ArmA2.SqmElementExportVisitor(),
                    new ArmA3.SqmElementExportVisitor(),
                    new ContextIndenter()))
        {
        }

        public void Export(Stream stream, SqmContentsBase contents)
        {
            var exportVisitor = _exporterFactory.Create(stream);
            contents.Accept(exportVisitor);
        }
    }
}
