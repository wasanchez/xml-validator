namespace XML.Validator
{
    public interface IXml
    {
        bool IsValid { get; }
        bool DetermineXml(string xml);
    }
}
