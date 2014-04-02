using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Common;

namespace SQMImportExport.ArmA2
{
    public class MissionState : MissionStateBase
    {
        public MissionState()
        {
            AddOns = new List<string>();
            AddOnsAuto = new List<string>();

            Groups = new List<Vehicle>();
            Vehicles = new List<Vehicle>();
            Markers = new List<Marker>();
            Sensors = new List<Sensor>();
        }

        public List<string> AddOns { get; set; }
        public List<string> AddOnsAuto { get; set; }

        public int? RandomSeed { get; set; }

        public Intel Intel { get; set; }

        public new List<Vehicle> Groups
        {
            get { return base.Groups.Cast<Vehicle>().ToList(); }
            set { base.Groups = value; }
        }

        public List<Vehicle> Vehicles { get; set; }
        public List<Marker> Markers { get; set; }
        public List<Sensor> Sensors { get; set; }
    }
}