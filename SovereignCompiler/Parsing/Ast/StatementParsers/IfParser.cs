using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class IfParser : IStatementParser
{
    public bool CanParse(ParserContext context)
    {
        return context.Equal(TokenKind.IfKeyword);
    }

    public AstNode Parse(ParserContext context)
    {
        context.Consume();

        context.Expect(TokenKind.LParen, "Ожидалась открывающая скобка '(' после если_надо");

        string condition = "";

        while (context.Equal(TokenKind.RParen) == false && context.IsAtEnd == false)
            condition += context.Consume().Lexeme + " ";

        condition = condition.Trim();

        context.Expect(TokenKind.RParen, "Ожидалась закрывающая скобка ')' для условия");

        BlockParser blockParser = new(
            new VariableParser(),
            new IfParser(),
            new BreakParser(),
            new FunctionCallParser(),
            new WhileParser(),
            new ReturnParser(),
            new SwitchParser());

        List<AstNode> thenBranch = blockParser.Parse(context);

        List<AstNode> elseBranch = null;

        if (context.Equal(TokenKind.ElseKeyword))
        {
            context.Consume();
            
            elseBranch = blockParser.Parse(context);
        }
        
        return new IfDeclarationNode(condition, thenBranch, elseBranch);
    }
}
