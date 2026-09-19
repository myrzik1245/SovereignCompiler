using System.Numerics;

namespace SovereignCompiler.Lexing;

public class LexerContext
{
    private readonly string _source;
    
    public LexerContext(string source)
    {
        _source = source;
        Position = new Vector2(1, 1);
    }
    
    public int Index { get; private set; }
    public Vector2 Position { get; private set; }
    public bool IsAtEnd => Index >= _source.Length;
    
    public char Current => IsAtEnd ? '\0' :  _source[Index];

    public char Peek(int offset)
    {
        int index = Index + offset;
        
        return index < _source.Length ? _source[index] : '\0';
    }
    
    public void Advance()
    {
        if (Current == '\r')
        {
            Index++;
            return;
        }
        if (Current == '\n')
        {
            Position = new Vector2(Position.X + 1 , 1);
        }
        else
        {
            Position = new Vector2(Position.X , Position.Y + 1);
        }
        
        Index++;
    }
    
    public string Substring(int start, int length)
    {
        return _source.Substring(start, length);
    }
}
