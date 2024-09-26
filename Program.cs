using MyVec;
using System.Diagnostics.Metrics;


class Program
{
    public static bool IsGoodNumber(string number)
    {
        if (number.Length > 0)
        {
            int newNumber = Convert.ToInt32(number);
            if (newNumber > 255) return false;
            else if (newNumber >= 0) return true;
        }
        return false;
    }

    public static bool IsIP(string line)
    {
        int dotCounter = 0;

        if (line[0] == '.') return false;

        for (int i = 0; i < line.Length; i++)
        {
            if (!(line[i] == '.' || char.IsDigit(line[i]))) return false;
            if (line[i] == '.') dotCounter++;
        }
        
        if (dotCounter != 3) return false;
        else 
        {
            string currentNumber = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i])) currentNumber += line[i];
                else if (line[i] == '.')
                {
                    if (IsGoodNumber(currentNumber)) currentNumber = "";
                    else return false;
                }
            }

            currentNumber = "";
            dotCounter = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '.') dotCounter++;
                if (dotCounter  == 3 && char.IsDigit(line[i])) 
                {
                    currentNumber += line[i];
                }
            }
            if (!IsGoodNumber(currentNumber)) return false;
        }
            return true;
    }

    public static MyVector<string> ParseVector(MyVector<string> vector)
    {
        MyVector<string> resultVector = new MyVector<string>();

        for (int i = 0; i < vector.Size(); i++)
        {
            string line = vector.Get(i);
            
            if (IsIP(line)) resultVector.Add(line);           
        }

        return resultVector;
    }

    public static MyVector<string> ReadFile()
    {
        MyVector<string> vector = new MyVector<string>();
        using (StreamReader reader = new StreamReader("text.txt"))
        {
            string lines = reader.ReadToEnd();
            vector = new MyVector<string>(lines.Split(new char[] { ' ', '\n', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries));
        }
        return vector;
    }

    public static void WriteFromVector(MyVector<string> vector)
    {
        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            for (int i = 0; i < vector.Size(); i++) {
                writer.WriteLine(vector.Get(i));
            }
        }
    }

    public static void Main(string[] args)
    {
        MyVector<string> myVector = ReadFile();
        myVector.Print();

        MyVector<string> resutVector = ParseVector(myVector);
        resutVector.Print();

        WriteFromVector(resutVector);
    }
}
