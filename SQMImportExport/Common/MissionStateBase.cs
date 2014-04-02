using System.Collections.Generic;

namespace SQMImportExport.Common
{
    public abstract class MissionStateBase
    {
        public IEnumerable<VehicleBase> Groups { get; set; }
    }
}