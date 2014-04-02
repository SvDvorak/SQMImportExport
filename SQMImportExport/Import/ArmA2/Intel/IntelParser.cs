using System.Text.RegularExpressions;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import.ArmA2.Intel
{
    internal class IntelParser : ParserBase<SQMImportExport.ArmA2.Intel>
    {
        private readonly Regex _intelRegex = new Regex(@"class Intel", RegexOptions.Compiled);

        public IntelParser()
        {
            LineSetters.Add(new StringPropertySetter("briefingName", x => ParseResult.BriefingName = x));
            LineSetters.Add(new StringPropertySetter("briefingDescription", x => ParseResult.BriefingDescription = x));
            LineSetters.Add(new IntegerPropertySetter("resistanceWest", x => ParseResult.ResistanceWest = x));
            LineSetters.Add(new IntegerPropertySetter("resistanceEast", x => ParseResult.ResistanceEast = x));
            LineSetters.Add(new DoublePropertySetter("startWeather", x => ParseResult.StartWeather = x));
            LineSetters.Add(new DoublePropertySetter("startFog", x => ParseResult.StartFog = x));
            LineSetters.Add(new DoublePropertySetter("forecastWeather", x => ParseResult.ForecastWeather = x));
            LineSetters.Add(new DoublePropertySetter("forecastFog", x => ParseResult.ForecastFog = x));
            LineSetters.Add(new IntegerPropertySetter("year", x => ParseResult.Year = x));
            LineSetters.Add(new IntegerPropertySetter("month", x => ParseResult.Month = x));
            LineSetters.Add(new IntegerPropertySetter("day", x => ParseResult.Day = x));
            LineSetters.Add(new IntegerPropertySetter("hour", x => ParseResult.Hour = x));
            LineSetters.Add(new IntegerPropertySetter("minute", x => ParseResult.Minute = x));
        }

        protected override Regex HeaderRegex
        {
            get { return _intelRegex; }
        }
    }
}