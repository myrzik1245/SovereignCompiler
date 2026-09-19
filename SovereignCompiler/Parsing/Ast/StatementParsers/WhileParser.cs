using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class WhileParser : IStatementParser
{
    public bool CanParse(ParserContext context)
    {
        return context.Equal(TokenKind.WhileKeyword);
    }

    public AstNode Parse(ParserContext context)
    {
        context.Consume();
        
        context.Expect(TokenKind.LParen, "Ожидалась открывающая скобка '(' после пока_надо");
        string condition = "";
        
        while (context.Equal(TokenKind.RParen) == false && context.IsAtEnd == false)
            condition += context.Consume().Lexeme + " ";

        condition = condition.Trim();
        
        context.Expect(TokenKind.RParen, "Ожидалась закрывающая скобка ')' для условия цикла");

        BlockParser blockParser = new(
            new VariableParser(),
            new IfParser(),
            new WhileParser(),
            new BreakParser(),
            new FunctionCallParser(),
            new ReturnParser(),
            new SwitchParser());
        
        List<AstNode> body = blockParser.Parse(context);

        return new WhileDeclarationNode(condition, body);
    }
}
