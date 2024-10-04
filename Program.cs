using MyStck;
using System.Numerics;

class RPN
{
    public string expr {  get; private set; }

    public string postExpr { get; private set; }

    public RPN(string str)
    {
        expr = str;
    }

    private Dictionary<string, int> operationPriority = new()
    {
        {"(", 0 },
        {"+", 1 },
        {"-", 1 },
        {"*", 2 },
        {"/", 2 },
        {"^", 3 },
        {"~", 4 },
        {"sqrt", 4 },
        {"abs", 4 },
        {"sign", 4 },
        {"sin", 3 },
        {"cos", 4 },
        {"tg", 4 },
        {"log", 4 },
        {"ln", 4 },
        {"min", 4 },
        {"max", 4 },
        {"%", 4 },
        {"&", 4 }, //частное от деления
        {"exp", 4 },
        {"tranck", 4 } // отбрасывание дробной части числа
    };

    private List<string> unarryOperations = new List<string>()
    {
        "~", "sqrt", "abs", "sign", "sin", "cos", "tg", "log", "ln", "tranck"
    };

    private double Calculate(string operation, double first, double second=0)
    {
        switch (operation)
        {
            case "+":
                return first + second;

            case "-":
                return first - second;

            case "*":
                return first * second;

            case "/":
                if (second != 0)
                    return first / second;
                throw new DivideByZeroException();

            case "^":
                return Math.Pow(first, second);

            case "~":
                return -1 * first;

            case "sqrt":
                return Math.Pow(first, 0.5);

            case "abs":
                return Math.Abs(first);

            case "sign":
                if (first >= 0) return 1;
                return 0;

            case "sin":
                return Math.Sin(first);

            case "cos":
                return Math.Cos(first);

            default:
                throw new ArgumentException();
            
        }
    }

    private string GetStringNumber(ref int index, string str) 
    {
        string strNum = "";
        int cntDot = 0;

        for (; index < str.Length; index++) 
        {
            char num = str[index];

            if (num == '.') cntDot++;

            if (char.IsDigit(num) || num == '.')
            {
                strNum += num.ToString();
            }
            else
            {
                index--;
                break;
            }
        }
        if (cntDot <= 1) return strNum;
        else throw new InvalidDataException();
    }

    private string GetStringOperation(ref int index, string str)
    {
        string strOp = "";
        for (; index < str.Length; index++)
        {
            char symbol = str[index];
            if (char.IsLetter(symbol))
            {
                strOp += symbol;
            }
            else
            {
                index--;
                break;
            }
        }
        return strOp;
    }

    private double GetArg(string arg)
    {
        Console.WriteLine($"Enter {arg}");
        double num = Console.Read();
        return num;
    }

    public void ConvertToRPN()
    {
        postExpr = "";
        MyStack<string> operations = new MyStack<string>();

        for (int i = 0; i < expr.Length; i++)
        {
            char cur = expr[i];

            if (char.IsDigit(cur))
            {
                postExpr += GetStringNumber(ref i, expr) + " ";
            }
            else if (cur == '(')
            {
                operations.Push(Convert.ToString(cur));
            }
            else if (char.IsLetter(cur))
            {
                string op = GetStringOperation(ref i, expr);
                if (op.Length > 1)
                {
                    operations.Push(op);
                }
                else
                {
                    postExpr += ( op + " ");
                }
            }
            else if (cur  == ')')
            {
                while (operations.Count() > 0 && operations.Peek() != "(")
                    postExpr += (operations.Pop() + " ");
                operations.Pop();
            }
            else if (operationPriority.ContainsKey(Convert.ToString(cur)))
            {
                char op = cur;
                if (op == '-' && (i == 0 || (i > 1 && operationPriority.ContainsKey(Convert.ToString(expr[i - 1]))) ) )
                {
                    op = '~';
                }

                while (operations.Count() > 0 && (operationPriority[operations.Peek()] >= operationPriority[Convert.ToString(op)])) 
                {
                    postExpr += (operations.Pop() + " ");
                }
                operations.Push(Convert.ToString(op));
            }
            
        }
        for (; operations.Count() > 0;)
        {
            postExpr += (operations.Pop() + " ");
        }
    }

    public double CalculateRPN()
    {
        Stack<double> numbers = new Stack<double>();

        for (int i = 0; i < postExpr.Length; i++)
        {
            char cur = postExpr[i];
            string op = "";

            if (char.IsDigit(cur))
            {
                string num = GetStringNumber(ref i, postExpr);
                numbers.Push(Convert.ToDouble(num));
            }

            else if (char.IsLetter(cur))
            {
                 op = GetStringOperation(ref i, postExpr);

                if (op.Length == 1)
                {
                    double num = GetArg(op);
                    numbers.Push(num);
                    postExpr.Replace(op, Convert.ToString(num));

                }
            }

            else if (operationPriority.ContainsKey(Convert.ToString(cur)))
            {
                op = Convert.ToString(cur);
            }

            if (operationPriority.ContainsKey(op))
            {
                double firstNumber, secondNumber;
                if (unarryOperations.Contains(op))
                {
                    firstNumber = numbers.Pop();
                    numbers.Push(Calculate(op, firstNumber));
                }
                else
                {
                    secondNumber = numbers.Pop();
                    firstNumber = numbers.Pop();
                    numbers.Push(Calculate(op, firstNumber, secondNumber));
                }
            }

        }
        return numbers.Pop();
    }

}



class Program
{
    public static void Main(string[] args)
    {
        RPN polish = new RPN("cos(sqrt(4))^2");
        polish.ConvertToRPN();
        Console.WriteLine(polish.postExpr);
        Console.WriteLine(polish.CalculateRPN());

    }
}