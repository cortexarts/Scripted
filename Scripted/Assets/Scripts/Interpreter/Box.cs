using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public static Interpreter interpreter = new Interpreter();

    private static bool hadError = false;
    private static bool hadRuntimeError = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void RunText(Text source)
    {
        Run(source.text);
    }

    public void Run(string source)
    {
        if (source.Length > 0)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.ScanTokens();
            Parser parser = new Parser(tokens);
            Expr expression = parser.Parse();

            if (hadError) return;

            interpreter.interpret(expression);
        }
        else
        {
            // TODO: Add pop-up or in-game feedback
            Debug.LogWarning("Script error!");
        }
    }

    public static void Error(int line, string message)
    {
        Report(line, "", message);
    }

    private static void Report(int line, string where, string message)
    {
        Debug.LogWarning("[line " + line + "] Error" + where + ": " + message);
        hadError = true;
    }

    static void Error(Token token, string message)
    {
        if (token.type == TokenType.EOF)
        {
            Report(token.line, " at end", message);
        }
        else
        {
            Report(token.line, " at '" + token.lexeme + "'", message);
        }
    }

    public static void RuntimeError(RuntimeError error)
    {
        Debug.LogError(error.GetMessage() + "\n[line " + error.token.line + "]");
        hadRuntimeError = true;
    }
}
