namespace SovereignCompiler.Parsing.Ast.Nodes;

public class IncludeDirectiveNode : AstNode
{
    public string Path { get; }
    
    public IncludeDirectiveNode(string path)
    {
        Path = path;
    }
}
