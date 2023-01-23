namespace XML.Validator.Elements
{
    public interface INode
    {
        string Name { get; }
        string? Value { get; }
        string? Text { get; }
        IReadOnlyList<INode> Children { get; }
        INode Create(string name, string? value, string? text, NodeType nodeType);
        INode AddChild(INode node);
    }
}
