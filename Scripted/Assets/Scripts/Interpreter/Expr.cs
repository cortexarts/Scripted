using System.Collections;
using System.Collections.Generic;

public abstract class Expr
{
    public abstract class Visitor<T>
    {
        public abstract T VisitNumber(int n);
        public abstract T VisitSum(T left, T right);
    }

    public <T> T Accept(Visitor<T> v)
    {
        return v.VisitSum(left.Accept(v), right.Accept(v));
    }

    public class Binary : Expr
    {
        public Binary(Expr left, Token oper, Expr right)
        {
            this.left = left;
            this.oper = oper;
            this.right = right;
        }

        public Expr left;
        public Token oper;
        public Expr right;
    }

    public class Literal : Expr
    {
        public Literal(object value)
        {
            this.value = value;
        }

        public object value;
    }

    public class Unary : Expr
    {
        public Unary(Token oper, Expr right)
        {
            this.oper = oper;
            this.right = right;
        }

        public Token oper;
        public Expr right;
    }

    public class Grouping : Expr
    {
        public Grouping(Expr expression)
        {
            this.expression = expression;
        }

        public Expr expression;
    }
}
