using System.Collections;
using System.Collections.Generic;

public interface BoxCallable
{
    int Arity();
    object Call(Interpreter interpreter, List<object> arguments);
}
