namespace SQMImportExport.Common
{
    public abstract class SqmContentsBase
    {
        public int? Version { get; set; }
        public MissionStateBase Mission { get; set; }

        public abstract void Accept(ISqmContentsVisitor visitor);
        public abstract T Accept<T>(ISqmContentsVisitor<T> visitor);
    }
}