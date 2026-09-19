using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.Nodes;

public class FunctionCallNode : AstNode
{
    public FunctionCallNode(string functionName, List<string> arguments)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }

    public string FunctionName { get; }
    public List<string> Arguments { get; }
}
