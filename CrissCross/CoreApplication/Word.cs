namespace CoreApplication;

public class Word
{
    private string _word;
    private int _x, _y;
    private Orientation _orientation;
    public Orientation Orientation => _orientation; 
    public int X => _x;
    public int Y => _y;

    public int Count => _word.Length;
    public Word(string word, int x, int y, Orientation orientation)
    {
        _word = word;
        _x = x;
        _y = y;
        _orientation = orientation;
    }

    public char this[int index]
    {
        get { return _word[index]; }
    }
}
