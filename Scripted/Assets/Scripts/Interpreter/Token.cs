﻿using System.Collections;
using System.Collections.Generic;

public class Token
{
    public TokenType type;
    public string lexeme;
    public string literal;
    public int line;

    public Token(TokenType type, string lexeme, string literal, int line)
    {
        this.type = type;
        this.lexeme = lexeme;
        this.literal = literal;
        this.line = line;
    }

    public string TokenString()
    {
        return type + " " + lexeme + " " + literal;
    }
}
