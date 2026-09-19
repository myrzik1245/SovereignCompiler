namespace SovereignCompiler.Parsing.Ast.Nodes;

public class ParameterDeclarationNode
{
    public string Type { get; }
    public bool IsReference { get; }
    public string Name { get; }

    public ParameterDeclarationNode(string type, bool isReference, string name)
    {
        Type = type;
        IsReference = isReference;
        Name = name;
    }
}
