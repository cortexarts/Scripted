using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    TokenType type;
    string lexeme;
    Object literal;
    int line;

    // Constructor
    public Token(TokenType type, string lexeme, Object literal, int line)
    {
        this.type = type;
        this.lexeme = lexeme;
        this.literal = literal;
        this.line = line;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string toString()
    {
        return type + " " + lexeme + " " + literal;
    }
}
