namespace XML.Validator.Elements
{
    public class Node : INode
    {
        public string Name { get; }
        public string? Value { get; }
        public string? Text { get; }
        public NodeType NodeType { get; }
        private List<INode> _children;
        public IReadOnlyList<INode> Children => _children;

        private Node(string name, string? value, string? text, NodeType nodeType)
        {
            Name = name;
            Value = value;
            Text = text;
            NodeType = nodeType;
            _children = new List<INode>();
        }
        
        public INode Create(string name, string? value, string? text, NodeType nodeType)
        {
            return new Node(name, value, text, nodeType);
        }

        public INode AddChild(INode node)
        {
            _children.Add(node);
            return this;
        } 
    }
}
