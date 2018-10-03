﻿using System.Collections;
using System.Collections.Generic;

public abstract class Stmt
{
    public interface Visitor<T>
    {
        T VisitExpressionStmt(Expression stmt);
        T VisitPrintStmt(Print stmt);
        T VisitVarStmt(Var stmt);
        T VisitBlockStmt(Block stmt);
        T VisitIfStmt(If stmt);
        T VisitWhileStmt(While stmt);
        T VisitFunctionStmt(Function stmt);
        T VisitReturnStmt(Return stmt);
    }

    public class Expression : Stmt
    {
        public Expression(Expr expression)
        {
            this.expression = expression;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitExpressionStmt(this);
        }

        public Expr expression;
    }
    
    public class Print : Stmt
    {
        public Print(Expr expression)
        {
            this.expression = expression;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitPrintStmt(this);
        }

        public Expr expression;
    }

    public class Var : Stmt
    {
        public Var(Token name, Expr initializer)
        {
            this.name = name;
            this.initializer = initializer;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitVarStmt(this);
        }

        public Token name;
        public Expr initializer;
    }

    public class Block : Stmt
    {
        public Block(List<Stmt> statements)
        {
            this.statements = statements;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitBlockStmt(this);
        }

        public List<Stmt> statements;
    }

    public class If : Stmt
    {
        public If(Expr condition, Stmt thenBranch, Stmt elseBranch)
        {
            this.condition = condition;
            this.thenBranch = thenBranch;
            this.elseBranch = elseBranch;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitIfStmt(this);
        }

        public Expr condition;
        public Stmt thenBranch;
        public Stmt elseBranch;
    }

    public class While : Stmt
    {
        public While(Expr condition, Stmt body)
        {
            this.condition = condition;
            this.body = body;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitWhileStmt(this);
        }

        public Expr condition;
        public Stmt body;

    }

    public class Function : Stmt
    {
        public Function(Token name, List<Token> parameters, List<Stmt> body)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = body;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitFunctionStmt(this);
        }

        public Token name;
        public List<Token> parameters;
        public List<Stmt> body;
    }

    public class Return : Stmt
    {
        public Return(Token keyword, Expr value)
        {
            this.keyword = keyword;
            this.value = value;
        }

        public override T Accept<T>(Visitor<T> visitor)
        {
            return visitor.VisitReturnStmt(this);
        }

        public Token keyword;
        public Expr value;
    }

    public abstract T Accept<T>(Visitor<T> visitor);
}