using System.Collections;
using System.Collections.Generic;
using System;

public class BoxReturn : Exception
{
    public object value;

    public BoxReturn(object value)
    {
        // TODO: Fix exception
        //Exception(null, null, false, false);
        this.value = value;
    }
}