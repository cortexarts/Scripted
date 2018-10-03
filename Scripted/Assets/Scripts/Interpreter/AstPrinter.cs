using System.Collections;
using System.Collections.Generic;
using System.Text;

public class AstPrinter : Expr.Visitor<string>
{
    public string Print(Expr expr)
    {
        return expr.Accept(this);
    }
    
    public string VisitBinaryExpr(Expr.Binary expr)
    {
        return Parenthesize(expr.oper.lexeme, expr.left, expr.right);
    }
    
    public string VisitGroupingExpr(Expr.Grouping expr)
    {
        return Parenthesize("group", expr.expression);
    }

    public string VisitVariableExpr(Expr.Variable expr)
    {
        return Parenthesize("variable", expr);
    }

    public string VisitAssignExpr(Expr.Assign expr)
    {
        return Parenthesize("assign", expr);
    }

    public string VisitLogicalExpr(Expr.Logical expr)
    {
        return Parenthesize("logical", expr);
    }

    public string VisitLiteralExpr(Expr.Literal expr)
    {
        if (expr.value == null) return "nil";
        return expr.value.ToString();
    }
    
    public string VisitUnaryExpr(Expr.Unary expr)
    {
        return Parenthesize(expr.oper.lexeme, expr.right);
    }

    private string Parenthesize(string name, params Expr[] exprs)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("(").Append(name);

        foreach (Expr expr in exprs)
        {
            builder.Append(" ");
            builder.Append(expr.Accept(this));
        }

        builder.Append(")");

        return builder.ToString();
    }

    public static void Test()
    {
        Expr expression = new Expr.Binary(
            new Expr.Unary(
                new Token(TokenType.MINUS, "-", null, 1),
                new Expr.Literal(123)),
            new Token(TokenType.STAR, "*", null, 1),
            new Expr.Grouping(
                new Expr.Literal(45.67)));

        System.Diagnostics.Debug.WriteLine(new AstPrinter().Print(expression));
    }
}
