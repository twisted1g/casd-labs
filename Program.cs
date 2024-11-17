class Program
{
    static void Main(string[] args)
    {
        MyHashMap<int, string> hashMap = new MyHashMap<int, string>();
        hashMap.Put(1, "a");
        hashMap.Put(1, "b");
        hashMap.Put(2, "b");
        hashMap.Put(3, "c");
        hashMap.Put(4, "d");
        hashMap.Put(2, "a");
        hashMap.Put(1, "a");

        Console.WriteLine(hashMap);
        Console.WriteLine(hashMap.ContainsKey(2));
        Console.WriteLine(hashMap.ContainsKey(5));
        Console.WriteLine(hashMap.ContainsValue("a"));
        Console.WriteLine(hashMap.EntrySet());
        Console.WriteLine(hashMap.Get(2));
        Console.WriteLine(hashMap.IsEmpty());
        Console.WriteLine(hashMap.KeySet());
        Console.WriteLine(hashMap.Size);
        hashMap.Remove(1);
        hashMap.Remove(2);
        Console.WriteLine(hashMap.Size);
        Console.WriteLine(hashMap);
        hashMap.Clear();
        Console.WriteLine(hashMap);
        Console.WriteLine(hashMap.Size);
    }
}