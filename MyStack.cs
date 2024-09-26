﻿using MyVec;

class MyStack<T> : MyVector<T>
{
    public MyStack(int initialCapacity, int capacityIncrement) : base(initialCapacity, capacityIncrement) { }

    public MyStack(int initialCapacity) : base(initialCapacity) { }

    public MyStack() : base() { }

    public void Push(T item)
    {
        base.Add(item);
    }

    public T Pop()
    {
        return base.RemoveIndex(base.Size() - 1);
    }

    public T Peek()
    {
        return base.Get(base.Size() - 1);
    }

    public bool Empty()
    {
        return base.IsEmpty();
    }

    public int Search(T item)
    {
        if (base.Contains(item))
        {
            return base.Size() - base.IndexOf(item);
        }
        return -1;
    } 

    public override void Print()
    {
        for (int i = base.Size() - 1; i >= 0; --i) 
        {
            Console.Write(base.Get(i) + " ");
        }
        Console.WriteLine();
    }
}


class MyStack
{
    public static void Main(string[] args)
    {
        MyStack<int> myStack = new MyStack<int>();
        myStack.Push(1);
        myStack.Add(2);
        myStack.Add(3);
        
        Console.WriteLine(myStack.Search(0));
    }
}