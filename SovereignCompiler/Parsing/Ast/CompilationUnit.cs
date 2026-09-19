using SovereignCompiler.Parsing.Ast.Nodes;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast;

public class CompilationUnit : AstNode
{
    public CompilationUnit(List<AstNode> members)
    {
        Members = members;
    }

    public List<AstNode> Members { get; }
}
