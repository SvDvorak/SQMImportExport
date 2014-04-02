namespace SQMImportExport.ArmA3
{
    public class Intel
    {
        public string BriefingName { get; set; }
        public string OverviewText { get; set; }
        public double? TimeOfChanges { get; set; }
        public double? StartWeather { get; set; }
        public double? StartWind { get; set; }
        public double? StartWaves { get; set; }
        public double? ForecastWeather { get; set; }
        public double? ForecastWind { get; set; }
        public double? ForecastWaves { get; set; }
        public double? ForecastLightnings { get; set; }
        public int? RainForced { get; set; }
        public int? LightningsForced { get; set; }
        public int? WavesForced { get; set; }
        public int? WindForced { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
        public double? StartFogBase { get; set; }
        public double? ForecastFogBase { get; set; }
        public double? StartFogDecay { get; set; }
        public double? ForecastFogDecay { get; set; }
    }
}
