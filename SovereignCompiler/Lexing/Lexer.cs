using SovereignCompiler.Lexing.Scanners;
using System.Collections.Generic;

namespace SovereignCompiler.Lexing;

public class Lexer
{
    private readonly LexerContext _context;
    private readonly List<ILexemeScanner> _scanners;

    private static readonly Dictionary<string, TokenKind> Keywords = new()
    {
        {"#наш_аналог", TokenKind.IncludeKeyword},
        {"главная_скрепа", TokenKind.MainKeyword},
        {"целое", TokenKind.IntKeyword},
        {"дробное", TokenKind.FloatKeyword},
        {"логическое", TokenKind.BoolKeyword},
        {"текстовое", TokenKind.StringKeyword},
        {"пусто", TokenKind.VoidKeyword},
        {"правда", TokenKind.TrueKeyword},
        {"фейк", TokenKind.FalseKeyword},
        {"если_надо", TokenKind.IfKeyword},
        {"а_придется", TokenKind.ElseKeyword},
        {"пока_надо", TokenKind.WhileKeyword},
        {"славу_отдать", TokenKind.ReturnKeyword},
        {"бюрократический_аппарат", TokenKind.SwitchKeyword},
        {"инстанция", TokenKind.CaseKeyword},
        {"отбой", TokenKind.BreakKeyword}
    };

    public Lexer(string source)
    {
        _context = new LexerContext(source);
        
        _scanners = new List<ILexemeScanner>
        {
            new CommentScanner(),
            new DirectiveScanner(Keywords),
            new KeywordOrIdentifierScanner(Keywords),
            new OperatorScanner(),
            new StringScanner(),
            new NumberScanner(),
        };
    }

    public List<Token> Tokenize()
    {
        List<Token> tokens = new();

        while (_context.IsAtEnd == false)
        {
            if (char.IsWhiteSpace(_context.Current))
            {
                _context.Advance();
                continue;
            }
            
            bool matched = false;

            foreach (ILexemeScanner scanner in _scanners)
            {
                if (scanner.TryScan(_context, out Token token))
                {
                    tokens.Add(token);
                    matched = true;
                    break;
                }
            }

            if (matched == false)
            {
                tokens.Add(new Token(TokenKind.Unknown, _context.Current.ToString(), _context.Position));
                _context.Advance();
            }
        }

        tokens.Add(new Token(TokenKind.Eof, "", _context.Position));

        return tokens;
    }
}
