using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Globalization;


namespace Backend;

public static class ExpressionEvaluator
{
    public static double Evalute(string infix) => EvalutePostfix(ToPostfix(infix));

    /*public static double Evalute(string infix) // prueba para revisar el funcionamiento de los decimales en ToPostfix
    {
        var postfix = ToPostfix(infix);

        Console.WriteLine($"Postfix: {postfix}");

        return EvalutePostfix(postfix);
    }*/

    private static string ToPostfix(string infix)
    {
        var posfix = string.Empty;
        var stack = new Stack<char>();

        // se cambia el foreach por un for para poder acceder al indice del caracter
        for (int i = 0; i < infix.Length; i++)
        {
            char item = infix[i]; // se obtiene el caracter en la posicion i

            if (char.IsDigit(item) || item == '.') // se verifica si el caracter es un digito y se maneja el caso de los numeros de mas de un digito
            {
                string number = "";
                bool hasDecimalPoint = false;

                while (i < infix.Length) // se recorre la expresion mientras se encuentren digitos o un solo punto decimal
                {
                    if (char.IsDigit(infix[i]))
                    {
                        number += infix[i];
                        i++;
                    }
                    else if (infix[i] == '.' && !hasDecimalPoint)
                    {
                        number += infix[i];
                        hasDecimalPoint = true;
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                posfix += number + " ";
                i--; // para ajustar el indice para el siguiente caracter
            }
            else if (IsOperator(item))
            {
                if (item == ')')
                {
                    var ope = stack.Pop();
                    while(ope != '(')
                    {
                        posfix += ope + " ";
                        ope = stack.Pop();
                    }
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(item);
                    }
                    else
                    {
                        if (PriorityInfix(item) > PriorityStack(stack.Peek()))
                        {
                            stack.Push(item);
                        }
                        else
                        {
                            posfix += stack.Pop() + " ";
                            stack.Push(item);
                        }
                    }
                }
            }
        }
        
        while (stack.Count != 0)
        {
            posfix += stack.Pop() + " ";
        } 
        
        return posfix.Trim();
    }


    private static int PriorityStack(char op) => op switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => throw new Exception("Invalid expression."),
    };

    private static int PriorityInfix(char op) => op switch
    {
        '^' => 4,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 5,
        _ => throw new Exception("Invalid expression."),
    };

    private static bool IsOperator(char item) => item == '^' || item == '*' || item == '/' || item == '+' || item == '-' || item == '(' || item == ')';

    private static double EvalutePostfix(string postfix)
    {
        var stack = new Stack<double>();

        var tokens = postfix.Split(' ');

        foreach (var token in tokens)
        {
            if (token.Length == 1 && IsOperator(token[0]))
            {
                var ope2 = stack.Pop();
                var ope1 = stack.Pop();

                stack.Push(Calculate(ope1, ope2, token[0]));
            }
            else
            {
                //stack.Push(double.Parse(token));
                stack.Push(double.Parse(token, CultureInfo.InvariantCulture)); // utiliza el . como separador de decimales
            }
        }

        /* codigo original
        foreach (var item in postfix)
        {
            if (IsOperator(item))
            {
                var ope2 = stack.Pop();
                var ope1 = stack.Pop();
                stack.Push(Calculate(ope1, ope2, item));
            }
            else
            {
                stack.Push(char.GetNumericValue(item));
            }
        }*/

        return stack.Pop();
    }

    private static double Calculate(double ope1, double ope2, char item) => item switch
    {
        '*' => ope1 * ope2,
        '/' => ope1 / ope2,
        '+' => ope1 + ope2,
        '-' => ope1 - ope2,
        '^' => Math.Pow(ope1, ope2),
        _ => throw new Exception("Invalid expression."),
    };
}
