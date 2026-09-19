using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.Nodes;

public class FunctionDeclarationNode : AstNode
{
    public FunctionDeclarationNode(string returnType, string name, List<AstNode> body, List<ParameterDeclarationNode> parameters)
    {
        ReturnType = returnType;
        Name = name;
        Body = body;
        Parameters = parameters;
    }

    public string ReturnType { get; }
    public string Name { get; }
    public List<AstNode> Body { get; }
    public List<ParameterDeclarationNode> Parameters { get; }
}