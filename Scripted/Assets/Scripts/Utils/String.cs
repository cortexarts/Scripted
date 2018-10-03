using System.Collections;
using System.Collections.Generic;

public static partial class String
{
    public static string IndexBasedSubstring(this string str, int start, int end)
    {
        return str.Substring(start, end - start);
    }
}
