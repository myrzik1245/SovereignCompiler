using SovereignCompiler.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SovereignCompiler.Parsing;

public class ParserContext
{
    private readonly List<Token> _tokens;
    private int _index;

    public ParserContext(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public Token Current => _index < _tokens.Count ? _tokens[_index] : _tokens.Last();
    public bool IsAtEnd => Current.Kind == TokenKind.Eof;

    public Token Peek(int offset)
    {
        int index = _index + offset;

        return index < _tokens.Count ? _tokens[index] : _tokens.Last();
    }

    public Token Consume()
    {
        Token current = Current;

        if (IsAtEnd == false)
            _index++;

        return current;
    }

    public bool Equal(TokenKind kind)
    {
        return Current.Kind == kind;
    }

    public bool Match(params TokenKind[] kinds)
    {
        foreach (TokenKind kind in kinds)
        {
            if (Equal(kind))
            {
                Consume();
                
                return true;
            }
        }
        
        return false;
    }

    public Token Expect(TokenKind kind, string message)
    {
        if (Equal(kind))
            return Consume();

        throw new SystemException(
            $"Синтаксическая ошибка ["
            + $"Строка: {Current.Position.X},"
            + $"Колонка: {Current.Position.Y}]: {message}. "
            + $"Ожидалось {kind}, но найдено {Current.Kind} ('{Current.Lexeme}').");
    }
}
