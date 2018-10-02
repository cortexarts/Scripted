using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interpreter : MonoBehaviour
{
    private static bool hadError = false;

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

            // Temp test
            foreach (Token token in tokens)
            {
                Debug.Log(token.TokenString());
            }

            // TODO: Parse script
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
}
