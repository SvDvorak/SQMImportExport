using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import.ArmA2.Marker
{
    internal class MarkerItemParser : ItemParserBase<SQMImportExport.ArmA2.Marker>
    {
        public MarkerItemParser()
        {
            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new StringPropertySetter("name", x => ParseResult.Name = x));
            LineSetters.Add(new StringPropertySetter("text", x => ParseResult.Text = x));
            LineSetters.Add(new StringPropertySetter("markerType", x => ParseResult.MarkerType = x));
            LineSetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            LineSetters.Add(new StringPropertySetter("colorName", x => ParseResult.ColorName = x));
            LineSetters.Add(new StringPropertySetter("fillName", x => ParseResult.FillName = x));
            LineSetters.Add(new DoublePropertySetter("a", x => ParseResult.A = x));
            LineSetters.Add(new DoublePropertySetter("b", x => ParseResult.B = x));
            LineSetters.Add(new IntegerPropertySetter("drawBorder", x => ParseResult.DrawBorder = x));
            LineSetters.Add(new DoublePropertySetter("angle", x => ParseResult.Angle = x));
        }
    }
}
