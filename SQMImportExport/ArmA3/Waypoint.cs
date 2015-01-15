using System.Collections.Generic;
using SQMImportExport.Common;

namespace SQMImportExport.ArmA3
{
    public class Waypoint : VehicleBase
    {
        public Waypoint()
        {
            Effects = new List<string>();
        }

        public int? Placement { get; set; }
        public int? CompletitionRadius { get; set; }
        public string Type { get; set; }
        public string CombatMode { get; set; }
        public string Formation { get; set; }
        public string Speed { get; set; }
        public string Combat { get; set; }
        public string ExpActiv { get; set; }
        public List<string> Effects { get; set; }
        public int? TimeoutMin { get; set; }
        public int? TimeoutMid { get; set; }
        public int? TimeoutMax { get; set; }
        public string ShowWp { get; set; }
    }
}