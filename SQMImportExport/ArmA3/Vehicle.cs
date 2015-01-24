using System.Collections.Generic;
using System.Linq;
using SQMImportExport.Common;

namespace SQMImportExport.ArmA3
{
    public class Vehicle : VehicleBase
    {
        public Vehicle()
        {
            Synchronizations = new List<int>();
            Vehicles = new List<Vehicle>();
            Waypoints = new List<Waypoint>();
        }

        public double? Presence { get; set; }
        public string PresenceCondition { get; set; }
        public int? Placement { get; set; }
        public double? Azimut { get; set; }
        public double? OffsetY { get; set; }
        public string Special { get; set; }
        public string Age { get; set; }
        public int? Id { get; set; }
        public string Side { get; set; }
        public string VehicleName { get; set; }
        public int? IsUAV { get; set; }
        public string Player { get; set; }
        public int? ForceHeadlessClient { get; set; }
        public int? Leader { get; set; }
        public string Rank { get; set; }
        public double? Skill { get; set; }
        public string Lock { get; set; }
        public double? Health { get; set; }
        public double? Ammo { get; set; }
        public string Text { get; set; }
        public string Init { get; set; }
        public string Description { get; set; }
        public int? SyncID { get; set; }
        public List<int> Synchronizations { get; set; }

        public new IEnumerable<Vehicle> Vehicles
        {
            get { return base.Vehicles.Cast<Vehicle>(); }
            set { base.Vehicles = value; }
        }

        public List<Waypoint> Waypoints { get; set; }
    }
}