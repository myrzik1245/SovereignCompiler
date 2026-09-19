using System.Numerics;

namespace SovereignCompiler.Lexing.Scanners;

public class NumberScanner : ILexemeScanner
{
    public bool TryScan(LexerContext context, out Token token)
    {
        token = default;

        Vector2 startPosition = context.Position;
        int startIndex = context.Index;
        
        if (char.IsDigit(context.Current) == false)
            return false;

        while (char.IsDigit(context.Current))
            context.Advance();

        if (context.Current == '.' && char.IsDigit(context.Peek(1)))
        {
            context.Advance();
            
            while (char.IsDigit(context.Current))
                context.Advance();
        }
        
        string lexeme = context.Substring(startIndex, context.Index - startIndex);
        token = new Token(TokenKind.NumberLiteral, lexeme, startPosition);
        
        return true;
    }
}
