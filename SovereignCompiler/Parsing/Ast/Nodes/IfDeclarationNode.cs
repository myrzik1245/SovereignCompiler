using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.Nodes;

public class IfDeclarationNode : AstNode
{
    public IfDeclarationNode(string condition, List<AstNode> thenBody, List<AstNode> elseBody)
    {
        Condition = condition;
        ThenBody = thenBody;
        ElseBody = elseBody;
    }

    public string Condition { get; }
    public List<AstNode> ThenBody { get; }
    public List<AstNode> ElseBody { get; }
}
