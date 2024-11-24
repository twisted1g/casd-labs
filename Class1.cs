using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

enum VariableType
{
    Int,
    Float,
    Double
}

class Program
{
    static void Main(string[] args)
    {
        string content = File.ReadAllText("input.txt");

        string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        string fullContent = string.Join(" ", lines);


        string pattern = @"\b(int|float|double)\s+([a-zA-Z_]\w*)\s*=\s*(\d+);";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(fullContent);

        MyHashMap<string, (VariableType, string)> variables = new MyHashMap<string, (VariableType, string)>();
        MyLinkedList<string> errorMessages = new MyLinkedList<string>();

        foreach (Match match in matches)
        {
            string typeString = match.Groups[1].Value;
            string name = match.Groups[2].Value;
            string value = match.Groups[3].Value;

            VariableType type;
            if (Enum.TryParse(typeString, true, out type))
            {
                if (!variables.ContainsKey(name))
                {
                    variables.Put(name, (type, value));
                }
                else
                {
                    errorMessages.Add($"Переменная {name} уже определена.");
                }
            }
        }

        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            writer.WriteLine(variables);
        }

        Console.WriteLine(errorMessages);
    }
}
