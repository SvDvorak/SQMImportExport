using System;
using System.Text.RegularExpressions;
using SQMImportExport.ArmA3;
using SQMImportExport.Common;
using SQMImportExport.Import.ArmA3.MissionState;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import.ArmA3
{
    internal class SqmParser : ParserBase<SqmContents>, ISqmParser
    {
        public SqmParser()
        {
            var missionParser = new MissionStateParser("Mission");
            var introParser = new MissionStateParser("Intro");
            var outroWinParser = new MissionStateParser("OutroWin");
            var outroLooseParser = new MissionStateParser("OutroLoose");

            ContextSetters.Add(new ContextSetter<SQMImportExport.ArmA3.MissionState>(missionParser, x => ParseResult.Mission = x));
            ContextSetters.Add(new ContextSetter<SQMImportExport.ArmA3.MissionState>(introParser, x => ParseResult.Intro = x));
            ContextSetters.Add(new ContextSetter<SQMImportExport.ArmA3.MissionState>(outroWinParser, x => ParseResult.OutroWin = x));
            ContextSetters.Add(new ContextSetter<SQMImportExport.ArmA3.MissionState>(outroLooseParser, x => ParseResult.OutroLose = x));

            LineSetters.Add(new IntegerPropertySetter("version", x => ParseResult.Version = x));
        }

        protected override Regex HeaderRegex
        {
            get { throw new NotImplementedException(); }
        }

        public new SqmContentsBase ParseContext(SqmContext context)
        {
            return base.ParseContext(context);
        }
    }
}
