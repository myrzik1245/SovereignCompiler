using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SovereignCompiler.Lexing.Scanners;

public class OperatorScanner : ILexemeScanner
{
    private static readonly Dictionary<string, TokenKind> Operators = new()
    {
        { ";", TokenKind.Semicolon },
        { ",", TokenKind.Comma },
        { ":", TokenKind.Colon },
        { "{", TokenKind.LBrace },
        { "}", TokenKind.RBrace },
        { "(", TokenKind.LParen },
        { ")", TokenKind.RParen },
        { "+", TokenKind.Plus },
        { "-", TokenKind.Minus },
        { "&", TokenKind.Ampersand },
        { "=", TokenKind.Assign },
        { "==", TokenKind.Equal },
        { "!=", TokenKind.NotEqual },
        { ">", TokenKind.Greater },
        { ">=", TokenKind.GreaterOrEqual },
        { "<", TokenKind.Less },
        { "<=", TokenKind.LessOrEqual }
    };

    private static readonly List<string> SortedOperators = Operators.Keys
        .OrderByDescending(op => op.Length)
        .ToList();

    public bool TryScan(LexerContext context, out Token token)
    {
        token = default;

        foreach (string op in SortedOperators)
        {
            bool matches = true;
            
            for (int i = 0; i < op.Length; i++)
            {
                if (context.Peek(i) != op[i])
                {
                    matches = false;
                    break;
                }
            }

            if (matches)
            {
                Vector2 startPosition = context.Position;
                
                for (int i = 0; i < op.Length; i++)
                    context.Advance();

                token = new Token(Operators[op], op, startPosition);
                
                return true;
            }
        }

        return false;
    }
}
