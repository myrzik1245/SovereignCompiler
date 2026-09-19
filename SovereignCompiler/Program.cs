using SovereignCompiler.Lexing;
using SovereignCompiler.Parsing;
using SovereignCompiler.Parsing.Ast;
using SovereignCompiler.Parsing.Ast.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SovereignCompiler;

class Program
{
    static void Main(string[] args)
    {
        string sourceCode = """
                            #наш_аналог "стандартная_библиотека"
                            
                            // Функция с проверкой и возвратом значения (славу_отдать)
                            целое проверить_уровень(целое &уровень) {
                                если_надо (уровень >= 18) {
                                    славу_отдать 1;
                                }
                                а_придется {
                                    славу_отдать 0;
                                }
                            }
                            
                            // Точка входа в суверенную программу
                            пусто главная_скрепа() {
                                целое возраст = 25;
                                целое &ссылка_на_возраст = возраст;
                                текстовое приветствие = "Здравствуйте, товарищ!";
                                логическое допуск = правда;
                                дробное коэффициент = 1.8;
                            
                                // Проверка условного оператора
                                если_надо (ссылка_на_возраст >= 18) {
                                    рапорт(приветствие);
                                }
                                а_придется {
                                    отбой;
                                }
                            
                                // Проверка цикла пока_надо
                                целое счетчик = 0;
                                пока_надо (счетчик < 3) {
                                    рапорт("Итерация цикла...");
                                    счетчик = счетчик + 1;
                                }
                            
                                // Проверка бюрократического аппарата (switch-case)
                                целое инстанция_номер = 2;
                                бюрократический_аппарат (инстанция_номер) {
                                    инстанция 1: {
                                        рапорт("Первая инстанция: отказ");
                                        отбой;
                                    }
                                    инстанция 2: {
                                        рапорт("Вторая инстанция: документы согласованы");
                                        отбой;
                                    }
                                }
                            }
                            """;

        Console.WriteLine("=== ИСХОДНЫЙ КОД CZ ===");
        Console.WriteLine(sourceCode);
        Console.WriteLine("=======================\n");

        Lexer lexer = new(sourceCode);
        List<Token> tokens = lexer.Tokenize();

        Console.WriteLine($"Результат лексического анализа (всего токенов: {tokens.Count}):\n");

        foreach (var token in tokens)
            Console.WriteLine($"[Строка: {token.Position.X}, Колонки: {token.Position.Y}]  {token.Kind,-22} -> '{token.Lexeme}'");
        
        Parser parser = new(tokens);
        
        try
        {
            CompilationUnit ast = parser.Parse();
            
            Console.WriteLine("Парсинг успешно завершен! Дерево AST построено без ошибок.");

            PrintAst(ast);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка синтаксического анализа: {ex.Message}");
        }
        
        
    }
    
    static void PrintAst(AstNode node, string indent = "")
    {
        switch (node)
        {
            case CompilationUnit cu:
                Console.WriteLine($"{indent}CompilationUnit");
                foreach (var member in cu.Members)
                    PrintAst(member, indent + "  ");
                break;

            case FunctionDeclarationNode funcDecl:
                string paramsStr = string.Join(", ", funcDecl.Parameters.Select(p => $"{p.Type}{(p.IsReference ? " &" : " ")}{p.Name}"));
                Console.WriteLine($"{indent}Function: {funcDecl.ReturnType} {funcDecl.Name}({paramsStr})");
                Console.WriteLine($"{indent}  Body {{");
                foreach (var child in funcDecl.Body)
                    PrintAst(child, indent + "    ");
                Console.WriteLine($"{indent}  }}");
                break;

            case VariableDeclarationNode varDecl:
                Console.WriteLine($"{indent}VariableDeclaration: {varDecl.Type} {varDecl.Name} = {varDecl.InitializeValue}");
                break;

            case IfDeclarationNode ifNode:
                Console.WriteLine($"{indent}IfStatement (Condition: {ifNode.Condition})");
                Console.WriteLine($"{indent}  Then {{");
                foreach (var child in ifNode.ThenBody)
                    PrintAst(child, indent + "    ");
                Console.WriteLine($"{indent}  }}");
                
                if (ifNode.ElseBody.Count > 0)
                {
                    Console.WriteLine($"{indent}  Else {{");
                    foreach (var child in ifNode.ElseBody)
                        PrintAst(child, indent + "    ");
                    Console.WriteLine($"{indent}  }}");
                }
                break;

            case FunctionCallNode call:
                string args = string.Join(", ", call.Arguments);
                Console.WriteLine($"{indent}FunctionCall: {call.FunctionName}({args});");
                break;

            case BreakDeclarationNode:
                Console.WriteLine($"{indent}BreakStatement: отбой;");
                break;

            case IncludeDirectiveNode includeNode:
                Console.WriteLine($"{indent}IncludeDirective: {includeNode.Path}");
                break;
            
            case WhileDeclarationNode whileNode:
                Console.WriteLine($"{indent}WhileStatement (Condition: {whileNode.Condition})");
                Console.WriteLine($"{indent}  Body {{");
                foreach (var child in whileNode.Body)
                    PrintAst(child, indent + "    ");
                Console.WriteLine($"{indent}  }}");
                break;
            
            case ReturnDeclarationNode returnNode:
                Console.WriteLine($"{indent}ReturnStatement: славу_отдать {returnNode.Expression};");
                break;
            
            case SwitchDeclarationNode switchNode:
                Console.WriteLine($"{indent}SwitchStatement (Expression: {switchNode.Expression}) {{");
                foreach (var caseNode in switchNode.Cases)
                {
                    Console.WriteLine($"{indent}  Case ({caseNode.Value}) {{");
                    foreach (var child in caseNode.Body)
                        PrintAst(child, indent + "    ");
                    Console.WriteLine($"{indent}  }}");
                }
                Console.WriteLine($"{indent}}}");
                break;
            
            default:
                Console.WriteLine($"{indent}UnknownNode: {node.GetType().Name}");
                break;
        }
    }
}
