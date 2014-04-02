using System.Collections.Generic;

namespace SQMImportExport.Common
{
    public abstract class VehicleBase
    {
        public int Number { get; set; }
        public Vector Position { get; set; }
        public IEnumerable<VehicleBase> Vehicles { get; set; }
    }
}