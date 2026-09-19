using System.Numerics;

namespace SovereignCompiler.Lexing;

public record struct Token
{
    public TokenKind Kind { get; init; }
    public string Lexeme { get; init; }
    public Vector2 Position { get; init; }

    public Token(TokenKind kind, string lexeme, Vector2 position)
    {
        Kind = kind;
        Lexeme = lexeme;
        Position = position;
    }
}
