using System.Collections;
using System.Collections.Generic;
using System;

public class RuntimeError : SystemException
{
    public Token token;

    public RuntimeError(Token token, string message)
    {
        // TODO: Actual runtime error
        System.Diagnostics.Debug.WriteLine(message);
        this.token = token;
    }
}
