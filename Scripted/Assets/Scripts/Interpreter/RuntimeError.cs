using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RuntimeError : SystemException
{
    public Token token;

    public RuntimeError(Token token, string message)
    {
        // TODO: Actual runtime error
        System.Diagnostics.Debug.WriteLine(message);
        // Unity
        Debug.Log(message);
        this.token = token;
    }
}
