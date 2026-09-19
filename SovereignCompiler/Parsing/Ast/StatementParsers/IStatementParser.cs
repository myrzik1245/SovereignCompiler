using SovereignCompiler.Parsing.Ast.Nodes;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public interface IStatementParser
{
    bool CanParse(ParserContext context);
    AstNode Parse(ParserContext context);
}
