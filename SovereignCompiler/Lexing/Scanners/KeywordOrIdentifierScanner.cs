using System.Collections.Generic;
using System.Numerics;

namespace SovereignCompiler.Lexing.Scanners;

public class KeywordOrIdentifierScanner : ILexemeScanner
{
    private readonly Dictionary<string, TokenKind> _keywords;

    public KeywordOrIdentifierScanner(Dictionary<string, TokenKind> keywords)
    {
        _keywords = keywords;
    }

    public bool TryScan(LexerContext context, out Token token)
    {
        token = default;

        if (char.IsLetter(context.Current) == false && context.Current != '_')
            return false;

        Vector2 startPosition = context.Position;
        int startIndex = context.Index;

        while (char.IsLetterOrDigit(context.Current) || context.Current == '_')
            context.Advance();

        string lexeme = context.Substring(startIndex, context.Index - startIndex);
        
        if (_keywords.TryGetValue(lexeme, out TokenKind tokenKind))
            token = new Token(tokenKind, lexeme, startPosition);
        else
            token = new Token(TokenKind.Identifier, lexeme, startPosition);

        return true;
    }
}
