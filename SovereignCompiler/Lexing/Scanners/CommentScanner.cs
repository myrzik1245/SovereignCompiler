namespace SovereignCompiler.Lexing.Scanners;

public class CommentScanner : ILexemeScanner
{
    public bool TryScan(LexerContext context, out Token token)
    {
        token = default;

        if (context.Current == '/' && context.Peek(1) == '/')
        {
            while (context.Current != '\n' && context.Current != '\r' && context.Current != '\0' && !context.IsAtEnd)
                context.Advance();
            
            if (context.Current == '\r')
                context.Advance();
            
            if (context.Current == '\n')
                context.Advance();
        }
        
        return false;
    }
}
