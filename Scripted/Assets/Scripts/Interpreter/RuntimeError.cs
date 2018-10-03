using System.Collections;
using System.Collections.Generic;
using System;

class RuntimeError : SystemException
{
    public Token token;

    public RuntimeError(Token token, string message)
    {
        Super(message);
        this.token = token;
    }
}
