namespace SovereignCompiler.Lexing.Scanners;

public interface ILexemeScanner
{
    bool TryScan(LexerContext context, out Token token);
}