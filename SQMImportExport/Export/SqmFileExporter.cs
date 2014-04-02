using SQMImportExport.Common;
using SQMImportExport.StreamHelpers;

namespace SQMImportExport.Export
{
    internal class SqmFileExporter : ISqmContentsVisitor
    {
        private readonly IStreamWriterAdapter _streamWriter;
        private readonly ArmA2.ISqmElementVisitor _arma2ElementVisitor;
        private readonly ArmA3.ISqmElementVisitor _arma3ElementVisitor;
        private readonly IContextIndenter _contextIndenter;

        public SqmFileExporter(
            IStreamWriterAdapter streamWriter,
            ArmA2.ISqmElementVisitor arma2ElementVisitor,
            ArmA3.ISqmElementVisitor arma3ElementVisitor, 
            IContextIndenter contextIndenter)
        {
            _streamWriter = streamWriter;
            _arma2ElementVisitor = arma2ElementVisitor;
            _arma3ElementVisitor = arma3ElementVisitor;
            _contextIndenter = contextIndenter;
        }

        public void Visit(SQMImportExport.ArmA2.SqmContents arma2Contents)
        {
            var contentText = _arma2ElementVisitor.Visit("", arma2Contents);
            var indentedText = _contextIndenter.Indent(contentText);

            WriteText(indentedText);
        }

        public void Visit(SQMImportExport.ArmA3.SqmContents arma3Contents)
        {
            var contentText = _arma3ElementVisitor.Visit("", arma3Contents);
            var indentedText = _contextIndenter.Indent(contentText);

            WriteText(indentedText);
        }

        private void WriteText(string indentedText)
        {
            _streamWriter.Write(indentedText);

            _streamWriter.Flush();
        }
    }
}