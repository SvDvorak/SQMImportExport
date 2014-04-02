using System.Collections.Generic;
using SQMImportExport.ArmA2;

namespace SQMImportExport.Export.ArmA2
{
    internal interface ISqmElementVisitor
    {
        string Visit(string elementName, SqmContents sqmContents);
        string Visit(string elementName, MissionState mission);
        string Visit(string elementName, Intel intel);
        string Visit(string elementName, IEnumerable<Vehicle> vehicles);
        string Visit(string elementName, List<Marker> markers);
        string Visit(string elementName, List<Sensor> sensors);
        string Visit(string elementName, Vehicle vehicle);
        string Visit(string elementName, Marker marker);
        string Visit(string elementName, Sensor sensor);
    }
}
