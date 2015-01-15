using System.Text.RegularExpressions;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import.ArmA3.Intel
{
    internal class IntelParser : ParserBase<SQMImportExport.ArmA3.Intel>
    {
        private readonly Regex _intelRegex = new Regex(@"class Intel", RegexOptions.Compiled);

        public IntelParser()
        {
            LineSetters.Add(new StringPropertySetter("briefingName", x => ParseResult.BriefingName = x));
            LineSetters.Add(new StringPropertySetter("overviewText", x => ParseResult.OverviewText = x));
            LineSetters.Add(new DoublePropertySetter("timeOfChanges", x => ParseResult.TimeOfChanges = x));
            LineSetters.Add(new DoublePropertySetter("startWeather", x => ParseResult.StartWeather = x));
            LineSetters.Add(new DoublePropertySetter("startWind", x => ParseResult.StartWind = x));
            LineSetters.Add(new IntegerPropertySetter("startWindDir", x => ParseResult.StartWindDir = x));
            LineSetters.Add(new DoublePropertySetter("startWaves", x => ParseResult.StartWaves = x));
            LineSetters.Add(new DoublePropertySetter("startGust", x => ParseResult.StartGust = x));
            LineSetters.Add(new DoublePropertySetter("forecastWeather", x => ParseResult.ForecastWeather = x));
            LineSetters.Add(new DoublePropertySetter("forecastFog", x => ParseResult.ForecastFog = x));
            LineSetters.Add(new DoublePropertySetter("forecastWind", x => ParseResult.ForecastWind = x));
            LineSetters.Add(new DoublePropertySetter("forecastWaves", x => ParseResult.ForecastWaves = x));
            LineSetters.Add(new DoublePropertySetter("forecastGust", x => ParseResult.ForecastGust = x));
            LineSetters.Add(new IntegerPropertySetter("forecastWindDir", x => ParseResult.ForecastWindDir = x));
            LineSetters.Add(new DoublePropertySetter("forecastLightnings", x => ParseResult.ForecastLightnings = x));
            LineSetters.Add(new IntegerPropertySetter("rainForced", x => ParseResult.RainForced = x));
            LineSetters.Add(new IntegerPropertySetter("lightningsForced", x => ParseResult.LightningsForced = x));
            LineSetters.Add(new IntegerPropertySetter("wavesForced", x => ParseResult.WavesForced = x));
            LineSetters.Add(new IntegerPropertySetter("windForced", x => ParseResult.WindForced = x));
            LineSetters.Add(new IntegerPropertySetter("year", x => ParseResult.Year = x));
            LineSetters.Add(new IntegerPropertySetter("month", x => ParseResult.Month = x));
            LineSetters.Add(new IntegerPropertySetter("day", x => ParseResult.Day = x));
            LineSetters.Add(new IntegerPropertySetter("hour", x => ParseResult.Hour = x));
            LineSetters.Add(new IntegerPropertySetter("minute", x => ParseResult.Minute = x));
            LineSetters.Add(new DoublePropertySetter("startFogBase", x => ParseResult.StartFogBase = x));
            LineSetters.Add(new DoublePropertySetter("forecastFogBase", x => ParseResult.ForecastFogBase = x));
            LineSetters.Add(new DoublePropertySetter("startFogDecay", x => ParseResult.StartFogDecay = x));
            LineSetters.Add(new DoublePropertySetter("forecastFogDecay", x => ParseResult.ForecastFogDecay = x));
        }

        protected override Regex HeaderRegex
        {
            get { return _intelRegex; }
        }
    }
}