using System.Collections.Generic;
using SQMImportExport.Common;

namespace SQMImportExport.ArmA2
{
    public class Sensor : VehicleBase
    {
        public Sensor()
        {
            Effects = new List<string>();
            Synchronizations = new List<int>();
        }

        public double? A { get; set; }
        public double? B { get; set; }
        public double? Angle { get; set; }
        public int? Rectangular { get; set; }
        public string Type { get; set; }
        public string ActivationBy { get; set; }
        public string ActivationType { get; set; }
        public int? Repeating { get; set; }
        public int? TimeoutMin { get; set; }
        public int? TimeoutMid { get; set; }
        public int? TimeoutMax { get; set; }
        public int? Interruptable { get; set; }
        public string Age { get; set; }
        public int? IdStatic { get; set; }
        public int? IdVehicle { get; set; }
        public int? IdObject { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string ExpCond { get; set; }
        public string ExpActiv { get; set; }
        public string ExpDesactiv { get; set; }
        public List<string> Effects { get; set; }
        public List<int> Synchronizations { get; set; }
    }
}
