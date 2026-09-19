namespace SovereignCompiler.Parsing.Ast.Nodes;

public class ReturnDeclarationNode : AstNode
{
    public string Expression { get; }

    public ReturnDeclarationNode(string expression)
    {
        Expression = expression;
    }
}
