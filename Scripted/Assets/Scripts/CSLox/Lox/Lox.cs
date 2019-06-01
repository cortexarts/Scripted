﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace CSLox
{
    internal class Lox : MonoBehaviour
    {
        private static readonly Interpreter interpreter = new Interpreter();
        private static bool hadError = false;
        private static bool hadRuntimeError = false;

        private static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: lox [script]");
                return;
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        public void RunInputText(Text input)
        {
            Run(input.text);
        }

        private static void RunFile(string path)
        {
            string input;
            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
            {
                input = streamReader.ReadToEnd();
            }

            Run(input);

            // Indicate an error in the exit code.        
            if (hadError)
            {
                System.Environment.Exit(65);
            }

            if (hadRuntimeError)
            {
                System.Environment.Exit(70);
            }
        }

        private static void RunPrompt()
        {
            string input = Console.In.ToString();
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(input));

            using (var inputStream = new StreamReader(memoryStream))
            {
                Console.WriteLine("> ");
                Run(inputStream.ReadLine());
                hadError = false;
            }
        }

        private static void Run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.ScanTokens();

            Parser parser = new Parser(tokens);
            List<Stmt> statements = parser.Parse();

            // Stop if there was a syntax error
            if (hadError) return;

            Resolver resolver = new Resolver(interpreter);
            resolver.Resolve(statements);

            // Stop if there was a resolution error
            if (hadError) return;

            interpreter.Interpret(statements);
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        public static void Error(Token token, String message)
        {
            if (token.Type == TokenType.EOF)
            {
                Report(token.Line, "at end", message);
            }
            else
            {
                Report(token.Line, "at '" + token.Lexeme + "'", message);
            }
        }

        public static void RuntimeError(RuntimeError error)
        {
            Console.WriteLine(error.Message + $"\n[line {error.Token.Line}]");

#if UNITY_EDITOR
            Debug.Log(error.Message + $"\n[line {error.Token.Line}]");
#endif

            hadRuntimeError = true;
        }

        public static void Report(int line, string where, string message)
        {
            Console.WriteLine($"Line [{line}] Error {where}: {message}");

#if UNITY_EDITOR
            Debug.Log($"Line [{line}] Error {where}: {message}");
#endif

            hadError = true;
        }

        private static void TestAstPrinter()
        {
            // -123 * (45.67) => (* (- 123) (group 45.67))
            Expr expression =
            new Expr.Binary(
                new Expr.Unary(
                    new Token(TokenType.MINUS, "-", null, 1),
                    new Expr.Literal(123)
                ),
                new Token(TokenType.STAR, "*", null, 1),
                new Expr.Grouping(
                    new Expr.Literal(45.67)
                )
            );

            var printer = new AstPrinter();
            Console.WriteLine(expression.Accept(printer));
            Console.ReadLine();
        }

        private static void TestRpnPrinter()
        {
            // (1 + 2) * (4 - 3) => 1 2 + 4 3 - *
            Expr expression =
            new Expr.Binary(
                new Expr.Grouping(
                    new Expr.Binary(
                        new Expr.Literal(1),
                        new Token(TokenType.PLUS, "+", null, 1),
                        new Expr.Literal(2)
                    )
                ),
                new Token(TokenType.STAR, "*", null, 1),
                new Expr.Grouping(
                    new Expr.Binary(
                        new Expr.Literal(4),
                        new Token(TokenType.MINUS, "-", null, 1),
                        new Expr.Literal(3)
                    )
                )
            );

            var printer = new RpnPrinter();
            Console.WriteLine(expression.Accept(printer));
            Console.ReadLine();
        }
    }
}
