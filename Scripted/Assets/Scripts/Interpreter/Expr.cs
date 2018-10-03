using System.Collections;
using System.Collections.Generic;

public abstract class Expr
{
    public interface Visitor<T>
    {
        T VisitBinaryExpr(Binary expr);
        T VisitGroupingExpr(Grouping expr);
        T VisitLiteralExpr(Literal expr);
        T VisitLogicalExpr(Logical expr);
        T VisitUnaryExpr(Unary expr);
        T VisitVariableExpr(Variable expr);
        T VisitAssignExpr(Assign expr);
    }

    public class Binary : Expr
    {
        public Binary(Expr left, Token oper, Expr right)
        {
            this.left = left;
            this.oper = oper;
            this.right = right;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }

        public Expr left;
        public Token oper;
        public Expr right;
    }

    public class Grouping : Expr
    {
        public Grouping(Expr expression)
        {
            this.expression = expression;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitGroupingExpr(this);
        }

        public Expr expression;
    }

    public class Literal : Expr
    {
        public Literal(object value)
        {
            this.value = value;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitLiteralExpr(this);
        }

        public object value;
    }

    public class Logical : Expr
    {
        public Logical(Expr left, Token oper, Expr right)
        {
            this.left = left;
            this.oper = oper;
            this.right = right;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitLogicalExpr(this);
        }

        public Expr left;
        public Token oper;
        public Expr right;
    }

    public class Unary : Expr
    {
        public Unary(Token oper, Expr right)
        {
            this.oper = oper;
            this.right = right;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitUnaryExpr(this);
        }

        public Token oper;
        public Expr right;
    }

    public class Variable : Expr
    {
        public Variable(Token name)
        {
            this.name = name;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitVariableExpr(this);
        }
        
        public Token name;
    }

    public class Assign : Expr
    {
        public Assign(Token name, Expr value)
        {
            this.name = name;
            this.value = value;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitAssignExpr(this);
        }

        public Token name;
        public Expr value;
    }

    public abstract T Accept<T>(Visitor<T> visitor);
}
