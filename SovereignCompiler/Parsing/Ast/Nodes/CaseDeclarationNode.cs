using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.Nodes;

public class CaseDeclarationNode : AstNode 
{
    public CaseDeclarationNode(string value, List<AstNode> body)
    {
        Value = value;
        Body = body;
    }

    public string Value { get; }
    public List<AstNode> Body { get; }
}