namespace SovereignCompiler.Lexing;

public enum TokenKind
{
    Eof,
    Unknown,

    Identifier,
    NumberLiteral,
    StringLiteral,
    
    IncludeKeyword,
    MainKeyword,
    VoidKeyword,
    TrueKeyword,
    FalseKeyword,
    
    IntKeyword,
    FloatKeyword,
    BoolKeyword,
    StringKeyword,
    
    IfKeyword,
    ElseKeyword,
    WhileKeyword,
    ReturnKeyword,
    SwitchKeyword,
    CaseKeyword,
    BreakKeyword,

    Assign,
    Equal,
    NotEqual,
    GreaterOrEqual,
    LessOrEqual,
    Greater,
    Less,
    Plus,
    Minus,
    Ampersand,

    Semicolon,
    Comma,
    Colon,
    LBrace,
    RBrace,
    LParen,
    RParen,
}
