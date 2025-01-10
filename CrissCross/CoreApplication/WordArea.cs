﻿
namespace CoreApplication;

public class WordArea
{
    private List<Word> _words;
    private int _height, _width;

    private int _numberOfWord;
    private int _sizeWordArea;
    private int _intersectWordCount;

    public int Height => _height;
    public int Width => _width;
    public List<Word> Words => _words;

    public WordArea()
    {
        _words = new List<Word>();
        _height = 0;
        _width = 0;
        _numberOfWord = 0;
        _sizeWordArea = 0;
        _intersectWordCount = 0;
    }

    private bool AddWord(string word)
    {
        Word newWord = new Word(word, 0, 0, Orientation.Horizontal);
        if (_words.Count == 0)
        {
            _words.Add(newWord);
            _height = 1;
            _width = newWord.Count;
            _sizeWordArea = _height * _width;
            _intersectWordCount++;
            _numberOfWord++;
            return true;
        }

        double currentDensity = _sizeWordArea / _numberOfWord;
        double minDensity = currentDensity;

        int currentX = 0;
        int currentY = 0;

        newWord = new Word(word, currentX, currentY, Orientation.Vertical);
        int newHeight = _height;
        int newWidth = _width;
        int intersection = 1;
        int maxIntersection = 1;

        bool isAdd = false;

        foreach(Word currentWord in _words)
        {
            for (int i = 0; i < currentWord.Count; i++)
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if (currentWord[i] == word[j])
                    {
                        if (currentWord.Orientation == Orientation.Horizontal)
                        {
                            int newX = currentWord.X + i;
                            int newY = currentWord.Y - j;

                            Word curWord = new Word(word, newX, newY, Orientation.Vertical);

                            if (CanBeAdd(currentWord, curWord, newX, newY)) {
                                if (!isAdd) newWord = curWord;

                                isAdd = true;
                                intersection = IntersectionCounting(curWord, newX, newY);

                                newHeight = word.Length > _height ? word.Length : _height;
                                minDensity = newHeight * _width / (_intersectWordCount + intersection);

                                if (currentDensity > minDensity)
                                {
                                    currentDensity = minDensity;
                                    currentX = newX;
                                    currentY = newY;
                                    maxIntersection = intersection;
                                    newWord = new Word(word, currentX, currentY, Orientation.Vertical);
                                }
                            }
                        }
                        else
                        {
                            int newX = currentWord.X - j;
                            int newY = currentWord.Y + i;

                            Word curWord = new Word(word, newX, newY, Orientation.Horizontal);

                            if (CanBeAdd(currentWord, curWord, newX, newY))
                            {
                                if (!isAdd) newWord = curWord;

                                isAdd = true;
                                intersection = IntersectionCounting(curWord, newX, newY);

                                newWidth = word.Length > _width ? word.Length : _width;
                                minDensity = newWidth * _height / (_intersectWordCount + intersection);

                                if (currentDensity > minDensity)
                                {
                                    currentDensity = minDensity;
                                    currentX = newX;
                                    currentY = newY;
                                    maxIntersection = intersection;
                                    newWord = new Word(word, currentX, currentY, Orientation.Vertical);
                                }
                            }
                        }
                    }
                }
            }
        }
        if (isAdd)
        {
            _width = newWidth;
            _height = newHeight;
            _intersectWordCount += maxIntersection;
            _words.Add(newWord);
            _numberOfWord++;
            return true;
        }
        return false;
    }

    private bool CanBeAdd(Word currentWord, Word newWord, int x, int y)
    {
        foreach (Word word in _words)
        {
            if (currentWord.Equals(word)) continue;
            if (word.Orientation == newWord.Orientation)
            {
                if (word.Orientation == Orientation.Horizontal) 
                {
                    if ((word.X <= x && x <= word.X + word.Count) || (x <= word.X && word.X <= x + newWord.Count))
                    {
                        if (y + 1 == word.Y || y - 1 == word.Y) return false;
                    }
                    if (y == word.Y)
                    {
                        if (x + newWord.Count + 1 == word.X) return false;
                        if (x - 1 == word.X + word.Count) return false;
                        if ((x <= word.X && word.X <= x + newWord.Count) || (word.X <= x && x <= word.X + word.Count)) return false;
                    }
                }
                else
                {
                    if ((word.Y <= y && y <= word.Y + word.Count) || (y <= word.Y && word.Y <= y + newWord.Count))
                    {
                        if (x + 1 == word.X || x - 1 == word.X) return false;
                    }
                    if (x == word.X)
                    {
                        if (y + newWord.Count + 1 == word.Y) return false;
                        if (y - 1 == word.Y + word.Count) return false;
                        if ((y <= word.Y && word.Y <= y + newWord.Count) || (word.Y <= y && y <= word.Y + word.Count)) return false;
                    }
                }
            }
            else
            {
                if (word.Orientation == Orientation.Horizontal)
                {
                    if ((word.X <= x && x <= word.X + word.Count - 1) && (word.Y == y + newWord.Count || y == word.Y + 1)) return false;
                    if ((y <= word.Y && word.Y <= y + newWord.Count - 1) && (x == word.X + word.Count || x == word.X - 1)) return false;

                    if ((word.X <= x && x <= word.X + word.Count - 1) && (y <= word.Y && word.Y <= y + newWord.Count - 1))
                    {
                        int indX = x - word.X;
                        int indY = word.Y - y;
                        if (newWord[indY] != word[indX]) return false;
                    }
                }
                else
                {
                    if ((x <= word.X && word.X <= x + newWord.Count - 1) && (y == word.Y - 1 || y == word.Y + word.Count)) return false;
                    if ((word.Y <= y && y <= word.Y + word.Count - 1) && (x == word.X + 1 || x + newWord.Count == word.X)) return false;

                    if ((x <= word.X && word.X <= x + newWord.Count - 1) && (word.Y <= y && y <= word.Y + word.Count - 1))
                    {
                        int indX = word.X - x;
                        int indY = y - word.Y;
                        if (newWord[indX] != word[indY]) return false;
                    }
                }
            }
        }
        return true;
    }

    private int IntersectionCounting(Word newWord, int x, int y)
    {
        int cnt = 0;
        foreach (Word word in _words)
        {
            if (word.Orientation == newWord.Orientation) continue;
            if (word.Orientation == Orientation.Horizontal)
            {
                if ((word.X <= x && x <= word.X + word.Count - 1) && (y <= word.Y && word.Y <= y + newWord.Count - 1)) cnt++;
            }
            else
            {
                if ((x <= word.X && word.X <= x + newWord.Count - 1) && (word.Y <= y && y <= word.Y + word.Count - 1)) cnt++;
            }
        }
        return cnt;
    }

    public bool AddAllWords(List<string> words)
    {
        foreach (string word in words)
        {
            if (!AddWord(word))
            {
                return false;
            }
        }
        return true;
    }





}