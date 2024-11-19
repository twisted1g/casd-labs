using System.Text.RegularExpressions;
class Program
{
    static string ReTag(string tag)
    {
        if (tag[1] == '/')
        {
            tag = tag.Remove(1, 1);
        }
        return tag.ToUpper();
    }

    static void ReadTagsFromFile(ref MyHashMap<string, int> hashMap)
    {
        using(StreamReader reader = new StreamReader("input.txt"))
        {
            Regex regex = new Regex(@"^</?\w+>$");
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                bool flag = false;
                string currentLine = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '<')
                    {
                        flag = true;
                        currentLine = "";
                    }

                    if (flag == true)
                    {
                        currentLine += line[i];
                    }

                    if (line[i] == '>')
                    {
                        MatchCollection matches = regex.Matches(currentLine);
                        if (matches.Count > 0)
                        {
                            currentLine = ReTag(currentLine);
                            if (hashMap.ContainsKey(currentLine))
                            {
                                hashMap.Put(currentLine, hashMap.Get(currentLine)+1);
                            }
                            else hashMap.Put(currentLine, 1);
                        }
                        flag = false;
                        currentLine = "";
                    } 
                } 
            }
        }
    }


    static void Main(string[] args)
    {
        MyHashMap<string, int> hashMap = new MyHashMap<string, int>();
        
        ReadTagsFromFile(ref hashMap);
        Console.WriteLine(hashMap);

    }
}