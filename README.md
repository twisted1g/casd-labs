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
- Если для заданного списка слов невозможно построить связную схему крисскросса, программа должна сообщить об этом.

## Описание решения ##
Проведем краткое описание решения поставленной задачи.

Для построенния схемы крисс-кросс, реализуем алгоритм такой, что поиск оптимального размещения основывается на минимизации плотности и увеличении пересечений.

Главная задача алгоритма заключается в максимальном использование пересечений букв, при минимизации увеличения площади таблицы.
Помимо этого будем учитывать различные ограничения для предотвращения конфликтов с уже размещенными словами.

## Описание реализации ##
Теперь более подробно рассмотрим сам алгоритм и его реализацию.
Для начала создадим
