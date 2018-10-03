using System.Collections;
using System.Collections.Generic;

public class Interpreter : Expr.Visitor<object>, Stmt.Visitor<object>
{
    public void Interpret(List<Stmt> statements)
    {
        try
        {
            foreach (Stmt statement in statements)
            {
                Execute(statement);
            }
        }
        catch (RuntimeError error)
        {
            Box.RuntimeError(error);
        }
    }

    public object VisitLiteralExpr(Expr.Literal expr)
    {
        return expr.value;
    }

    public object VisitUnaryExpr(Expr.Unary expr)
    {
        object right = Evaluate(expr.right);

        switch (expr.oper.type) 
        {
            case TokenType.BANG:
                return !IsTruthy(right);
            case TokenType.MINUS:
                return -(double)right;
        }

        // Unreachable.                              
        return null;
    }

    private bool IsTruthy(object obj)
    {
        if (obj == null) return false;
        if (obj == typeof(bool)) return (bool)obj;
        return true;
    }

    private bool IsEqual(object a, object b)
    {
        // nil is only equal to nil.               
        if (a == null && b == null) return true;
        if (a == null) return false;

        return a.Equals(b);
    }

    private string Stringify(object obj)
    {
        if (obj == null) return "nil";

        // Hack. Work around Java adding ".0" to integer-valued doubles.
        if (obj == typeof(double))
        {
            string text = obj.ToString();
            if (text.EndsWith(".0"))
            {
                text = text.Substring(0, text.Length - 2);
            }
            return text;
        }

        return obj.ToString();
    }

    public object VisitGroupingExpr(Expr.Grouping expr)
    {
        return Evaluate(expr.expression);
    }

    private object Evaluate(Expr expr)
    {
        return expr.Accept(this);
    }

    private void Execute(Stmt stmt)
    {
        stmt.Accept(this);
    }

    public object VisitExpressionStmt(Stmt.Expression stmt)
    {
        Evaluate(stmt.expression);
        return null;
    }

    public object VisitPrintStmt(Stmt.Print stmt)
    {
        object value = Evaluate(stmt.expression);
        System.Diagnostics.Debug.WriteLine(Stringify(value));
        return null;
    }

    public object VisitBinaryExpr(Expr.Binary expr)
    {
        object left = Evaluate(expr.left);
        object right = Evaluate(expr.right);

        switch (expr.oper.type)
        {
            case TokenType.GREATER:
                CheckNumberOperands(expr.oper, left, right);
                return (double)left > (double)right;
            case TokenType.GREATER_EQUAL:
                CheckNumberOperands(expr.oper, left, right);
                return (double)left >= (double)right;
            case TokenType.LESS:
                CheckNumberOperands(expr.oper, left, right);
                return (double)left < (double)right;
            case TokenType.LESS_EQUAL:
                CheckNumberOperands(expr.oper, left, right);
                return (double)left <= (double)right;
            case TokenType.MINUS:
                CheckNumberOperands(expr.oper, left, right);
                return (double)left - (double)right;
            case TokenType.PLUS:
                if (left == typeof(double) && right == typeof(double))
                {
                    return (double)left + (double)right;
                }

                if (left == typeof(string) && right == typeof(string))
                {
                    return (string)left + (string)right;
                }

                throw new RuntimeError(expr.oper, "Operands must be two numbers or two strings.");
            case TokenType.SLASH:
                CheckNumberOperands(expr.oper, left, right);
                return (double)left / (double)right;       
            case TokenType.STAR:
                CheckNumberOperands(expr.oper, left, right);
                return (double)left * (double)right;
            case TokenType.BANG_EQUAL: return !IsEqual(left, right);
            case TokenType.EQUAL_EQUAL: return IsEqual(left, right);
        }

        // Unreachable.                                
        return null;
    }

    private void CheckNumberOperand(Token oper, object operand)
    {
        if (operand == typeof(double)) return;
        throw new RuntimeError(oper, "Operand must be a number.");
    }

    private void CheckNumberOperands(Token oper, object left, object right)
    {
        if (left == typeof(double) && right == typeof(double)) return;

        throw new RuntimeError(oper, "Operands must be numbers.");
    }
}
