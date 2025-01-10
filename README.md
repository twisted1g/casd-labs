# Крисс-Кросс #

## Постановка задачи ##

Необходимо разработать программу, которая по заданному списку слов строит
корректную и связную схему головоломки типа «крисс-кросс».

Крисс-кросс – это головоломка, похожая на кроссворд, но без определений. Игроку
предоставляется пустая сетка и список слов, которые нужно вписать в сетку так, чтобы они
пересекались по правилам кроссворда: в местах пересечения у слов должны совпадать
буквы. Все слова из списка должны быть использованы, и каждое из них может быть
использовано только один раз. Длина слов служит подсказкой для их размещения.

Пример крисс-кросса:

![image](https://github.com/user-attachments/assets/146156c9-5749-42c0-93c2-7f6108028722)

Программа должна удовлетворять следующим условиям:

Построение схемы:
- Сгенерировать связную схему крисс-кросса, в которую будут вписаны все слова из
списка.
- В местах пересечения слов должны совпадать соответствующие буквы.
- Схема должна быть связной, то есть все слова должны быть соединены между
собой через пересечения.
- В схеме не должно быть повторяющихся слов.

Вывод результатов:
- Представить заполненную схему как подтверждение правильности решения.
- Обеспечить красивый графический вывод схемы для наглядности (например,
используя текстовую графику или графический интерфейс).

Обработка особых случаев:
- Если для заданного списка слов невозможно построить связную схему крисс-кросса, программа должна сообщить об этом.

## Описание решения ##
Проведем краткое описание решения поставленной задачи.

Для построения схемы крисс-кросс, реализуем алгоритм такой, что поиск оптимального размещения основывается на минимизации плотности и увеличении пересечений.

Главная задача алгоритма заключается в максимальном использование пересечений букв, при минимизации увеличения площади таблицы.
Помимо этого будем учитывать различные ограничения для предотвращения конфликтов с уже размещенными словами.

## Описание реализации ##
Теперь более подробно рассмотрим сам алгоритм и его реализацию.

Для начала создадим класс Word, который содержит данные о слове, его расположении на плоскости и ориентации (горизонтальной или вертикальной).

``` c#

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

```

Поля:

`_word` - слово.

`_x` и `_y` - координаты начала слова на плоскости.

`_orientation` - ориентация слова.

Свойства:

`Orientation` -  возвращает ориентацию слова.

`X` и `Y` — возвращают координаты начала слова.

`Count`— возвращает длину слова.

Конструктор:

`Word(string word, int x, int y, Orientation orientation)`

Индексатор:

`word[index]` - возвращает символ слова с указанным индексом.

Для определения ориентации воспользуемся enum 

``` c#
﻿public enum Orientation
{
    Horizontal,
    Vertical
}
```

Далее создадим основной класс WordArea, в котором будут размещаться слова.
``` c#
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
...
```

Поля:

`_words` - список добавленных слов.

`_height` и `_width` - высота и ширина таблицы.

`_numberOfWord` - общее количество слов в таблице.

`_sizeWordArea` - площадь таблицы.

`_intersectWordCount` - количество пересечений между словами.

Свойства:

`Height`и `Width` - возвращают текущие размеры таблицы.

`Words` - возвращает список добавленных слов.

 Конструктор:
 
`WordArea()` - инициализирует пустую таблицу.

Рассмотрим методы данного класса.

`AddWord(string word)`

 Этот метод отвечает за добавление одного слова в таблицу. Алгоритм работы следующий:
если таблица пуста, слово добавляется горизонтально, устанавливаются начальные размеры, количество слов, площадь и пересечения. Если в таблице уже есть слова:
- Вычисляется текущая плотность: 
$$\text{Плотность} = \frac{\text{Площадь таблицы}}{\text{Число слов}}$$
 - Для каждого слова, уже размещённого в таблице, проверяется возможность пересечения с новым словом. Проверка проводится по символам двух слов.
- Если символы совпадают, проверяется возможность размещения нового слова перпендикулярно уже существующему.
- Если размещение возможно, оценивается новое положение с точки зрения минимизации плотности.
- Плотность пересчитывается с учётом нового слова и возможных пересечений.
- Если после всех проверок нашлось подходящее место, слово добавляется, обновляются размеры таблицы, количество пересечений и общее число слов.
- Если подходящего место найдено, метод возвращает `true`. Иначе `false`.

```c#
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
```

`CanBeAdd(Word currentWord, Word newWord, int x, int y)`  

Этот метод проверяет возможность размещения нового слова в заданной позиции. Сравниваются ориентации текущего и нового слова. Потом проверяется, чтобы слова не находились слишком близко друг к другу, не пересекались некорректно или не перекрывали друг друга. Если подходящее место найдено, метод возвращает `true`. Иначе `false`.

```c#
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
```

`IntersectionCounting(Word newWord, int x, int y)` 

 Подсчитывает количество корректных пересечений нового слова с уже размещёнными словами. Перебираются все слова в таблице. Если новое слово пересекается с другим перпендикулярно, и символы совпадают, счётчик увеличивается.

```c#
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
```

`AddAllWords(List<string> words)`

 Пытается добавить все слова из списка в таблицу. Для каждого слова вызывается метод `AddWord`. Если хотя бы одно слово не удаётся добавить, метод возвращает `false`.

```c#
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
```

Суммируя все выше сказанное, получаем итоговый алгоритм добавления слов
Первое слово всегда добавляется горизонтально, размеры таблицы принимают длину первого слова.  Чтобы добавить новое слово, для каждого уже добавленного слова перебираются все символы текущего слова и проверяются совпадения символов с символами нового слова. Для каждого совпадения рассматривается потенциальное расположение нового слова перпендикулярно к текущему.

Для каждой возможной позиции нового слова проверяется корректность размещения, подсчитывается количество пересечений. Вычисляется новая плотность таблицы. Если новая плотность лучше текущей, выбирается эта позиция. Если нашлось подходящее место, слово добавляется в таблицу.

---
