using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class FunctionCallParser : IStatementParser
{

    public bool CanParse(ParserContext context)
    {
        return context.Equal(TokenKind.Identifier) && context.Peek(1).Kind == TokenKind.LParen;
    }

    public AstNode Parse(ParserContext context)
    {
        Token nameToken = context.Consume();
        string functionName = nameToken.Lexeme;

        context.Expect(TokenKind.LParen, "Ожидалась открывающая скобка '(' для вызова функции");

        List<string> arguments = new();

        while (context.Equal(TokenKind.RParen) == false && context.IsAtEnd == false)
        {
            Token argumentToken = context.Consume();
            arguments.Add(argumentToken.Lexeme);
            
            if (context.Equal(TokenKind.Comma))
                context.Consume();
        }
        
        context.Expect(TokenKind.RParen, "Ожидалась закрывающая скобка ')' после аргументов");
        context.Expect(TokenKind.Semicolon, "Ожидалась точка с запятой ';' после вызова функции");

        return new FunctionCallNode(functionName, arguments);
    }
}
