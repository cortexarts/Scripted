using System.Collections;
using System.Collections.Generic;

public class Parser
{
    private List<Token> tokens = new List<Token>();
    private int current = 0;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }
    
    private Expr Expression()
    {
        return Equality();
    }

    private Expr Equality()
    {
        Expr expr = Comparison();

        while (Match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL))
        {
            Token oper = Previous();
            Expr right = Comparison();
            expr = new Expr.Binary(expr, oper, right);
        }

        return expr;
    }

    private bool Match(TokenType types)
    {
        foreach (TokenType type in types.GetValues(typeof(TokenType)))
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }

        return false;
    }
}
