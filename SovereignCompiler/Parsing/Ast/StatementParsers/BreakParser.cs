using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class BreakParser : IStatementParser
{
    public bool CanParse(ParserContext context)
    {
        return context.Equal(TokenKind.BreakKeyword);
    }

    public AstNode Parse(ParserContext context)
    {
        context.Consume();
        context.Expect(TokenKind.Semicolon, "Ожидалась точка с запятой ';' после отбой");
        
        return new BreakDeclarationNode();
    }
}
