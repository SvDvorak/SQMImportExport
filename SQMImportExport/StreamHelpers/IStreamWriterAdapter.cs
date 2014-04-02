namespace SQMImportExport.StreamHelpers
{
    internal interface IStreamWriterAdapter
    {
        void Write(string text);
        void Flush();
    }
}