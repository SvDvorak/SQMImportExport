using SQMImportExport.ArmA2;

namespace SQMImportExport.Common
{
    public interface ISqmContentsVisitor
    {
        void Visit(SqmContents arma2Contents);
        void Visit(ArmA3.SqmContents arma3Contents);
    }

    public interface ISqmContentsVisitor<out T>
    {
        T Visit(SqmContents arma2Contents);
        T Visit(ArmA3.SqmContents arma3Contents);
    }
}