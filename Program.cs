using ArrayList;

class Program
{   
    public static bool IsTag(string tag)
    {
        if (string.IsNullOrEmpty(tag)) return false;
        else
        {
            if (tag.Length < 3) return false;
            int cntOpen = 0;
            int cntClose = 0;
            int cntSlash = 0;
            if (tag[0] != '<' ) return false;
            
            if (!(tag[1] == '/' && char.IsLetter(tag[2]) || char.IsLetter(tag[1]))) return false;

            if (tag[^1] != '>') return false;

            for (int i = 0; i < tag.Length; i++)
            {
                if (cntOpen == 0 && tag[i] == '<') cntOpen++;
                else if (cntOpen == 1 && tag[i] == '<') return false;

                if (cntClose == 0 && tag[i] == '>') cntClose++;
                else if (cntClose == 1 && tag[i] == '>') return false;

                if (cntSlash == 0 && tag[i] == '/') cntSlash++;
                else if (cntSlash == 1 && tag[i] == '/') return false;

                if (!(tag[i] == '<' || tag[i] == '>' || tag[i] == '/' || char.IsDigit(tag[i]) || char.IsLetter(tag[i])))
                    return false;
                
            }
            return true;
        }
    }

    public static bool CompareTags(string firstTag,  string secondTag)
    {
        if (firstTag is not null && secondTag is not null)
        {
            string innerFirstTag = "";
            for (int i = 0; i < firstTag.Length; i++)
            {
                if (firstTag[i] == '<' || firstTag[i] == '>' || firstTag[i] == '/') continue;
                else innerFirstTag += firstTag[i];
            }

            string innerSecondTag = "";
            for (int i = 0; i < secondTag.Length; i++)
            {
                if (secondTag[i] == '<' || secondTag[i] == '>' || secondTag[i] == '/') continue;
                else innerSecondTag += secondTag[i];
            }

            return innerFirstTag.ToUpper() == innerSecondTag.ToUpper();
        }
        return false;
    }  

    public static void ReadFile(ref MyArrayList<string> myArrayList)
    {
        using (StreamReader reader = new StreamReader("Text.txt"))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string tag = "";
                int cnt = 0;
                for (int i = 0; i < line.Length; i++)
                {   
                    if (cnt <= 1 && line[i] == '<')
                    {
                        tag = "";
                        tag += line[i];
                        cnt = 1;
                    }

                    if (cnt == 1 && line[i] != '<') tag += line[i];
                   
                    if (line[i] == '>')
                    {
                        if (IsTag(tag)) myArrayList.Add(tag);

                        tag = "";
                        cnt = 0;
                    }
                }
            }
        }
    }

    public static void DeleteTimes(ref MyArrayList<string> myArrayList)
    {
        int countUnique = 0;

        // Ищем количество уникальных элементов
        for (int i = 0; i < myArrayList.Size(); i++)
        {
            bool isUnique = true;
            for (int j = 0; j < countUnique; j++)
            {
                if (CompareTags(myArrayList.Get(i), myArrayList.Get(j)))
                {
                    isUnique = false;
                    break;
                }
            }
            if (isUnique) myArrayList.Set(countUnique++, myArrayList.Get(i));
            
        }

        MyArrayList<string> copyMyArrayList = new MyArrayList<string>(myArrayList.SubList(0, countUnique));
        myArrayList = copyMyArrayList;
    }

    public static void Main(string[] args)
    {
        MyArrayList<string> myArrayList = new MyArrayList<string>();

        ReadFile(ref myArrayList);
        myArrayList.Print();
        
        DeleteTimes(ref myArrayList);
        myArrayList.Print();
    }
}