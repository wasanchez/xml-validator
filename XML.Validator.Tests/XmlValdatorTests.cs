namespace XML.Validator.Tests;

public class XmlValdatorTests
{
    private readonly IXml _sut;

    public XmlValdatorTests()
    {
        _sut = new Xml();
    }

    [Theory(DisplayName = "DetermineXml method should be successful")]
    [InlineData("<tutorial><topic>XML</topic></tutorial>", true)]
    [InlineData("<Design><Code>hello world</Code></Design>", true)]
    [InlineData("<Design><Code>hello world</Code></Design><People>", false)]
    [InlineData("<People><Design><Code>hello world</People></Code></Design>", false)]
    [InlineData("<People age=”1”>hello world</People>", false)]
    [InlineData("<note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>", true)]   
    [InlineData("<note></note>", true)]
    [InlineData("<note><to>", false)]
    [InlineData("<note name=\"test\"><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>", false)]
    [InlineData("<note><to>Tove</to><from>Jani</from><heading>Reminder</pheading><body>Don't forget me this weekend!</body></note>", false)]
    public void XML_Determine_Xml_Should_Be_Successful(string xmlInput, bool expected)
    {
        bool actual = _sut.DetermineXml(xmlInput);

        Assert.Equal(expected, actual);
    }


    [Theory(DisplayName = "Should throw an ArgumentNullException")]
    [InlineData("", true)]
    [InlineData("  ", true)]
    [InlineData(null, true)]
    public void XML_Determine_Xml_Should_Throw_Exception_When_Xml_String_Is_NullOrEmpty(string xmlInput, bool expected)
    {
        var actual = Assert.Throws<ArgumentNullException>(() => _sut.DetermineXml(xmlInput));
        var actualParamenter = "xml";
        Assert.IsType<ArgumentNullException>(actual);
        Assert.Equal(actual.ParamName == actualParamenter, expected);
    }
}
