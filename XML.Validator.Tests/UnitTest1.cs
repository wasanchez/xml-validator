namespace XML.Validator.Tests;

public class UnitTest1
{
    [Fact(DisplayName = "Create elements map")]    
    public void XML_Load_Should_Create_Elements_Map()
    {
        var xmlInput = "<tutorial><topic>XML</topic></tutorial>";

        var xml = new Xml();

        xml.DetermineXml(xmlInput);

	}
}
