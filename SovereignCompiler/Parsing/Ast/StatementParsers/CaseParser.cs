using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class CaseParser : IStatementParser
{
    public bool CanParse(ParserContext context)
    {
        return context.Equal(TokenKind.CaseKeyword);
    }

    public AstNode Parse(ParserContext context)
    {
        context.Consume();

        string caseValue = "";
        
        while (context.Equal(TokenKind.Colon) == false && context.IsAtEnd == false)
            caseValue += context.Consume().Lexeme + " ";

        caseValue = caseValue.Trim();

        context.Expect(TokenKind.Colon, "Ожидалось двоеточие ':' после инстанции");

        BlockParser blockParser = new(
            new VariableParser(),
            new IfParser(),
            new WhileParser(),
            new SwitchParser(),
            new ReturnParser(),
            new BreakParser(),
            new FunctionCallParser()
        );

        List<AstNode> caseBody = blockParser.Parse(context);

        return new CaseDeclarationNode(caseValue, caseBody);
    }
}

public class SwitchParser : IStatementParser
{
    public bool CanParse(ParserContext context)
    {
        return context.Equal(TokenKind.SwitchKeyword);
    }

    public AstNode Parse(ParserContext context)
    {
        context.Consume();

        context.Expect(TokenKind.LParen, "Ожидалась открывающая скобка '('");

        string expression = "";
        while (context.Equal(TokenKind.RParen) == false && context.IsAtEnd == false)
            expression += context.Consume().Lexeme + " ";

        expression = expression.Trim();

        context.Expect(TokenKind.RParen, "Ожидалась закрывающая скобка ')'");
        context.Expect(TokenKind.LBrace, "Ожидалась открывающая фигурная скобка '{'");

        List<CaseDeclarationNode> cases = new();
        CaseParser caseParser = new();

        while (context.Equal(TokenKind.RBrace) == false && context.IsAtEnd == false)
        {
            if (caseParser.CanParse(context))
            {
                if (caseParser.Parse(context) is CaseDeclarationNode caseNode)
                    cases.Add(caseNode);
            }
            else
            {
                context.Consume();
            }
        }

        context.Expect(TokenKind.RBrace, "Ожидалась закрывающая фигурная скобка '}'");

        return new SwitchDeclarationNode(expression, cases);
    }
}
