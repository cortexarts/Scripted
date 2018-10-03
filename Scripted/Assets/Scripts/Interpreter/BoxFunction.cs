using System.Collections;
using System.Collections.Generic;

public class BoxFunction : BoxCallable
{
    private Stmt.Function declaration;
    private Environment closure;

    public BoxFunction(Stmt.Function declaration, Environment closure)
    {
        this.closure = closure;
        this.declaration = declaration;
    }

    public object Call(Interpreter interpreter, List<object> arguments)
    {
        Environment environment = new Environment(closure);
        for (int i = 0; i < declaration.parameters.Count; i++)
        {
            environment.Define(declaration.parameters[i].lexeme, arguments[i]);
        }

        interpreter.ExecuteBlock(declaration.body, environment);

        try
        {
            interpreter.ExecuteBlock(declaration.body, environment);
        }
        catch (BoxReturn returnValue)
        {
            return returnValue.value;
        }

        return null;
    }

    public int Arity()
    {
        return declaration.parameters.Count;
    }

    public string FunctionString()
    {
        return "<fn " + declaration.name.lexeme + ">";
    }
}
