using SQMImportExport.Common;

namespace SQMImportExport.ArmA2
{
    public class SqmContents : SqmContentsBase
    {
        public new MissionState Mission
        {
            get { return (MissionState)base.Mission; }
            set { base.Mission = value; }
        }

        public MissionState Intro { get; set; }
        public MissionState OutroWin { get; set; }
        public MissionState OutroLose { get; set; }

        public override void Accept(ISqmContentsVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override T Accept<T>(ISqmContentsVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}