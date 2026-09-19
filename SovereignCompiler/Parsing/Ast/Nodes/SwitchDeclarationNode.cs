using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.Nodes;

public class SwitchDeclarationNode : AstNode
{
    public SwitchDeclarationNode(string expression, List<CaseDeclarationNode> cases)
    {
        Expression = expression;
        Cases = cases;
    }

    public string Expression { get; }
    public List<CaseDeclarationNode> Cases { get; }
}
