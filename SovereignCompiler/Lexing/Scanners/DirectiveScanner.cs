using System.Collections.Generic;
using System.Numerics;

namespace SovereignCompiler.Lexing.Scanners;

public class DirectiveScanner : ILexemeScanner
{
    private readonly Dictionary<string, TokenKind> _keywords;

    public DirectiveScanner(Dictionary<string, TokenKind> keywords)
    {
        _keywords = keywords;
    }

    public bool TryScan(LexerContext context, out Token token)
    {
        token = default;

        if (context.Current != '#')
            return false;

        Vector2 startPosition = context.Position;
        int startIndex = context.Index;

        while (char.IsLetterOrDigit(context.Current) || context.Current == '_' || context.Current == '#')
            context.Advance();

        string lexeme = context.Substring(startIndex, context.Index - startIndex);
        
        if (_keywords.TryGetValue(lexeme, out TokenKind tokenKind))
            token = new Token(tokenKind, lexeme, startPosition);
        else
            token = new Token(TokenKind.Unknown, lexeme, startPosition);
        
        return true;
    }
}
