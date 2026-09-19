using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class ReturnParser : IStatementParser
{
    public bool CanParse(ParserContext context)
    {
        return context.Equal(TokenKind.ReturnKeyword);
    }

    public AstNode Parse(ParserContext context)
    {
        context.Consume();
        
        string expression = "";

        while (context.Equal(TokenKind.Semicolon) == false && context.IsAtEnd == false)
            expression += context.Consume().Lexeme + " ";

        expression = expression.Trim();

        context.Expect(TokenKind.Semicolon, "Ожидалась точка с запятой ';' после славу_отдать");

        return new ReturnDeclarationNode(expression);
    }
}
