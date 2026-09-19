namespace SovereignCompiler.Parsing.Ast.Nodes;

public class VariableDeclarationNode : AstNode
{
    public VariableDeclarationNode(string type, bool isReference, string name, string initializeValue)
    {
        Type = type;
        Name = name;
        InitializeValue = initializeValue;
        IsReference = isReference;
    }

    public string Type { get; }
    public string Name { get; }
    public string InitializeValue { get; }
    public bool IsReference { get; }
}
