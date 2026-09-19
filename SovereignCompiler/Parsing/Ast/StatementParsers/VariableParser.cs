using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class VariableParser : IStatementParser
{
    private readonly List<TokenKind> _typeKeywords = new()
    {
        TokenKind.IntKeyword,
        TokenKind.FloatKeyword,
        TokenKind.BoolKeyword,
        TokenKind.StringKeyword
    };
    
    public bool CanParse(ParserContext context)
    {
        foreach (var kind in _typeKeywords)
            if (context.Equal(kind))
                return true;
        
        return false;
    }

    public AstNode Parse(ParserContext context)
    {
        Token typeToken = context.Consume();
        string type = typeToken.Lexeme;

        bool isReference = false;
        if (context.Equal(TokenKind.Ampersand))
        {
            context.Consume();
            isReference = true;
        }
        
        Token nameToken = context.Expect(TokenKind.Identifier, "Ожидалось имя переменной");
        string name = nameToken.Lexeme;

        context.Expect(TokenKind.Assign, "Ожидался знак присваивания '='");
        
        Token valueToken = context.Consume();
        string value = valueToken.Lexeme;
        
        context.Expect(TokenKind.Semicolon, "Ожидалась точка с запятой ';' после объявления переменной");

        return new VariableDeclarationNode(type, isReference, name, value);
    }
}
