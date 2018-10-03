using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

public class GenerateAst
{
    public static void Generate(string dir)
    {
        string[] astArray = new string[]
        {
            "Binary   : Expr left, Token oper, Expr right",
            "Grouping : Expr expression",
            "Literal  : Object value",
            "Unary    : Token oper, Expr right"
        };

        List<string> astList = astArray.ToList();

        DefineAst(dir, "Expr", astList);
    }

    private static void DefineAst(string outputDir, string baseName, List<string> types)
    {
        string path = outputDir + "/" + baseName + ".cs";

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            streamWriter.WriteLine("using System.Collections;");
            streamWriter.WriteLine("using System.Collections.Generic;");
            streamWriter.WriteLine("abstract class " + baseName + " {");

            DefineVisitor(streamWriter, baseName, types);

            // The AST classes
            foreach (string type in types)
            {
                string className = type.Split(':')[0].Trim();
                string fields = type.Split(':')[1].Trim();
                DefineType(streamWriter, baseName, className, fields);

                // The base Accept() method
                streamWriter.WriteLine();
                streamWriter.WriteLine("  abstract <R> R Accept(Visitor<R> visitor);");
            }

            streamWriter.WriteLine("}");
            streamWriter.Close();
        }                                       
    }

    private static void DefineVisitor(StreamWriter streamWriter, string baseName, List<string> types)
    {
        streamWriter.WriteLine("  interface Visitor<R> {");

        foreach (string type in types)
        {
            string typeName = type.Split(':')[0].Trim();
            streamWriter.WriteLine("    R Visit" + typeName + baseName + "(" + typeName + " " + baseName.ToLower() + ");");
        }

        streamWriter.WriteLine("  }");
    }

    private static void DefineType(StreamWriter streamWriter, string baseName, string className, string fieldList)
    {
        streamWriter.WriteLine("  static class " + className + " : " + baseName + " {");

        // Constructor
        streamWriter.WriteLine("    " + className + "(" + fieldList + ") {");

        // Store parameters in fields
        string[] fields = fieldList.Split(',', ' ');

        foreach (string field in fields)
        {
            string name = field.Split(' ')[1];
            streamWriter.WriteLine("      this." + name + " = " + name + ";");
        }

        streamWriter.WriteLine("    }");

        // Visitor pattern
        streamWriter.WriteLine();
        streamWriter.WriteLine("    <R> R Accept(Visitor<R> visitor) {");
        streamWriter.WriteLine("      return visitor.Visit" + className + baseName + "(this);");
        streamWriter.WriteLine("    }");

        // Fields
        streamWriter.WriteLine();

        foreach (string field in fields)
        {
            streamWriter.WriteLine("    public " + field + ";");
        }

        streamWriter.WriteLine("  }");
    }
}
