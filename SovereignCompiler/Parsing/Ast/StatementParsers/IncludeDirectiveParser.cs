using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class IncludeDirectiveParser : IStatementParser
{
    public bool CanParse(ParserContext context)
    {
        return context.Equal(TokenKind.IncludeKeyword);
    }

    public AstNode Parse(ParserContext context)
    {
        context.Consume();

        Token pathToken = context.Expect(TokenKind.StringLiteral, "Ожидался путь к библиотеке в кавычках");

        return new IncludeDirectiveNode(pathToken.Lexeme);
    }
}
