using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class BlockParser
{
    private readonly IStatementParser[] _parsers;

    public BlockParser(params IStatementParser[] parsers)
    {
        _parsers = parsers;
    }

    public List<AstNode> Parse(ParserContext context)
    {
        context.Expect(TokenKind.LBrace, "Ожидалась открывающая фигурная скобка '{'");

        List<AstNode> statements = new();

        while (context.Equal(TokenKind.RBrace) == false && context.IsAtEnd == false)
        {
            bool matched = false;

            foreach (IStatementParser parser in _parsers)
            {
                if (parser.CanParse(context))
                {
                    statements.Add(parser.Parse(context));
                    matched = true;
                    break;
                }
            }

            if (matched == false)
                context.Consume();
        }

        context.Expect(TokenKind.RBrace, "Ожидалась закрывающая фигурная скобка '}'");

        return statements;
    }
}
