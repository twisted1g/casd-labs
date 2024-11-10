using ArrayDeque;

class Program
{
    static int CountNumbersInString(string? currentString)
    {
        int countNumbers = 0;
        if (currentString is not  null )
        {
            for (int i = 0; i < currentString.Length; i++)
            {
                if (Char.IsDigit(currentString[i])) countNumbers++;
            }
        }
        return countNumbers;
    }

    static int CountSpacesInString(string? currentString)
    {
        int countSpaces = 0;
        if (currentString is not null)
        {
            for (int i = 0; i < currentString.Length; i++)
            {
                if (currentString[i] == ' ') countSpaces++;
            }
        }
        return countSpaces;
    }

    static bool CompareNumbersInString(string? firstString, string? secondString)
    {
        if (CountNumbersInString(firstString) >= CountNumbersInString(secondString)) return true;
        else return false;
    }

    static void ReadFromFile(ref MyArrayDeque<string> deque)
    {
        using (StreamReader reader = new StreamReader("input.txt"))
        {
            string? currentString;
            while ((currentString = reader.ReadLine()) != null)
            {
                if (CompareNumbersInString(currentString, deque.PeekFirst()))
                {
                    deque.AddLast(currentString);
                }
                else deque.AddFirst(currentString);
            }
        }
    }

    static void WriteToFile(MyArrayDeque<string> deque)
    {
        using (StreamWriter wtiter = new StreamWriter("sorted.txt"))
        {
            while(deque.Size() != 0)
            {
                wtiter.WriteLine(deque.PollFirst());
            }
        }
    }

    static MyArrayDeque<string> DeleteWrongLines(MyArrayDeque<string> deque, int countSpaces)
    {
        MyArrayDeque<string> newDeque = new MyArrayDeque<string>();
        while(deque.Size() != 0)
        {
            string? currentString = deque.PollFirst();
            if (CountSpacesInString(currentString) < countSpaces)
            {
                newDeque.AddLast(currentString);
            }
        }
        return newDeque;
    }

    static void Main(string[] args)
    {
        MyArrayDeque<string> deque = new MyArrayDeque<string>();    
        ReadFromFile(ref deque);
        Console.WriteLine(deque.ToString());
        MyArrayDeque<string> copyDeque = new MyArrayDeque<string>(deque.ToArray());
        WriteToFile(copyDeque);

        int numbersOfSpaces = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(deque.ToString());
        copyDeque = new MyArrayDeque<string>(deque.ToArray());
        deque = DeleteWrongLines(copyDeque, numbersOfSpaces);
        Console.WriteLine(deque.ToString());
    }
}