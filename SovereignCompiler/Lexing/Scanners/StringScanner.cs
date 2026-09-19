using System.Numerics;

namespace SovereignCompiler.Lexing.Scanners;

public class StringScanner : ILexemeScanner
{
    public bool TryScan(LexerContext context, out Token token)
    {
        token = default;

        if (context.Current != '"')
            return false;

        Vector2 startPosition = context.Position;
        context.Advance();
        int startIndex = context.Index;

        while (context.Current != '"' && context.Current != '\0')
            context.Advance();

        string lexeme = context.Substring(startIndex, context.Index - startIndex);
        
        if (context.Current == '"')
            context.Advance();

        token = new Token(TokenKind.StringLiteral, lexeme, startPosition);
        return true;
    }
}
