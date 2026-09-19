using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing.Ast.Nodes;
using System;
using System.Collections.Generic;

namespace SovereignCompiler.Parsing.Ast.StatementParsers;

public class FunctionParser : IStatementParser
{
    private readonly List<TokenKind> _tokenKinds = new()
    {
        TokenKind.VoidKeyword,
        TokenKind.IntKeyword,
        TokenKind.FloatKeyword,
        TokenKind.BoolKeyword,
        TokenKind.StringKeyword,
    };

    public bool CanParse(ParserContext context)
    {
        foreach (TokenKind tokenKind in _tokenKinds)
            if (context.Equal(tokenKind))
                return true;

        return false;
    }

    public AstNode Parse(ParserContext context)
    {
        Token typeToken = context.Consume();
        string returnType = typeToken.Lexeme;

        Token nameToken = context.Consume();
        if (nameToken.Kind != TokenKind.Identifier && nameToken.Kind != TokenKind.MainKeyword)
            throw new Exception($"Ожидалось имя функции или ключевое слово на позиции {nameToken.Position}");

        string functionName = nameToken.Lexeme;

        context.Expect(TokenKind.LParen, "Ожидалась открывающая скобка '(' после имени функции");
        
        List<ParameterDeclarationNode> parameters = new();

        while (context.Equal(TokenKind.RParen) == false && context.IsAtEnd == false)
        {
            Token paramTypeToken = context.Consume();
            string paramType = paramTypeToken.Lexeme;

            bool isReference = false;
            if (context.Equal(TokenKind.Ampersand))
            {
                context.Consume();
                isReference = true;
            }

            Token paramNameToken = context.Expect(TokenKind.Identifier, "Ожидалось имя параметра");
            string paramName = paramNameToken.Lexeme;

            parameters.Add(new ParameterDeclarationNode(paramType, isReference, paramName));

            if (context.Equal(TokenKind.Comma))
                context.Consume();
            else if (context.Equal(TokenKind.RParen) == false)
                throw new Exception($"Ожидалась запятая ',' или закрывающая скобка ')' в списке параметров на позиции {context.Current.Position}");
        }
        
        context.Expect(TokenKind.RParen, "Ожидалась закрывающая скобка ')'");

        BlockParser blockParser = new(
            new VariableParser(),
            new IfParser(),
            new FunctionCallParser(),
            new WhileParser(),
            new ReturnParser(),
            new SwitchParser());
        
        List<AstNode> bodyStatements = blockParser.Parse(context);

        return new FunctionDeclarationNode(returnType, functionName, bodyStatements, parameters);
    }
}
