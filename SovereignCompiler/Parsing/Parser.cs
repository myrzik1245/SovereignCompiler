using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast;
using SovereignCompiler.Parsing.Ast.Nodes;
using SovereignCompiler.Parsing.Ast.StatementParsers;
using System;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing;

public class Parser
{
    private readonly ParserContext _context;
    private readonly List<IStatementParser> _statementParsers;

    public Parser(List<Token> tokens)
    {
        _context = new ParserContext(tokens);

        _statementParsers = new List<IStatementParser>
        {
            new IncludeDirectiveParser(),
            new FunctionParser(),
        };
    }

    public CompilationUnit Parse()
    {
        List<AstNode> members = new();

        while (_context.IsAtEnd == false)
        {
            bool matched = false;

            foreach (IStatementParser parser in _statementParsers)
            {
                if (parser.CanParse(_context))
                {
                    members.Add(parser.Parse(_context));
                    matched = true;
                    
                    break;
                }
            }

            if (matched == false)
            {
                throw new Exception($"Непредвиденный токен '{_context.Current.Lexeme}' на позиции {_context.Current.Position}");
            }
        }

        return new CompilationUnit(members);
    }
}
