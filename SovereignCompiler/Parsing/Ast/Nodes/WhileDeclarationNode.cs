using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.Nodes;

public class WhileDeclarationNode : AstNode
{
    public WhileDeclarationNode(string condition, List<AstNode> body)
    {
        Condition = condition;
        Body = body;
    }

    public string Condition { get; }
    public List<AstNode> Body { get; }
}
