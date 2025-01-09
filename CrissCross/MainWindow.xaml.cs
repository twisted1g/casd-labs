using CoreApplication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CrissCross
{
    public partial class MainWindow : Window
    {
        List<string> words;
        WordArea table;

        double canvasWidth;
        double canvasHeight;

        double boxSize = 40;

        public MainWindow()
        {
            InitializeComponent();
            words = new List<string>();
        }

        public void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            WordArea wordArea = new WordArea();
            if (!wordArea.AddAllWords(words))
            {
                MessageBox.Show("Слово не может быть добавлено");
                return;
            }

            canvasWidth = Canvas.ActualWidth;
            canvasHeight = Canvas.ActualHeight;
            double x = (canvasWidth - wordArea.Width * boxSize) / 2;
            double y = (canvasHeight - wordArea.Height * boxSize) / 2;
            this.table = wordArea;

            PaintTable(wordArea, x, y);
        }

        private void Canvas_Size_Changed(object sender, SizeChangedEventArgs e)
        {
            if (table != null)
            {
                canvasHeight = Canvas.ActualHeight;
                canvasWidth = Canvas.ActualWidth;
                double x = (canvasWidth - table.Width * boxSize) / 2;
                double y = (canvasHeight - table.Height * boxSize) / 2;

                PaintTable(table, x, y);
            }
        }

        public void AddAndDrawWordButton_Click(object sender, RoutedEventArgs e)
        {
            string newWord = WordInput.Text.Trim();
            if (!string.IsNullOrEmpty(newWord) && newWord != "Введите слово")
            {
                words.Add(newWord.ToUpper());
                WordInput.Clear();

                SubmitButton_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Введите слово!");
            }
        }

        public void LoadAndDrawWordsButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var fileWords = File.ReadAllLines(openFileDialog.FileName);
                    foreach (var word in fileWords)
                    {
                        string trimmedWord = word.Trim().ToUpper();
                        if (!string.IsNullOrEmpty(trimmedWord))
                        {
                            words.Add(trimmedWord);
                        }
                    }

                    SubmitButton_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}");
                }
            }
        }

        public void ClearTableButton_Click(object sender, RoutedEventArgs e)
        {
            words.Clear();
            table = null;
            Canvas.Children.Clear();
        }

        private void WordInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WordInput.Text == "Введите слово")
            {
                WordInput.Text = "";
                WordInput.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void WordInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WordInput.Text))
            {
                WordInput.Text = "Введите слово";
                WordInput.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        public void PaintTable(WordArea table, double x, double y)
        {
            List<Word> words = table.Words;
            Canvas.Children.Clear();
            Rectangle R;
            foreach (Word word in words)
            {
                double X = x + word.X * boxSize;
                double Y = y + word.Y * boxSize;
                int isVert = word.Orientation == Orientation.Vertical ? 1 : 0;
                for (int i = 0; i < word.Count; i++)
                {
                    double letterX = X + i * boxSize * (Math.Abs(isVert - 1));
                    double letterY = Y + i * boxSize * isVert;

                    R = new Rectangle
                    {
                        Width = boxSize,
                        Height = boxSize,
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeThickness = 1,
                        Fill = new SolidColorBrush(Colors.White)
                    };
                    Canvas.SetLeft(R, letterX);
                    Canvas.SetTop(R, letterY);
                    Canvas.Children.Add(R);

                    TextBlock textBlock = new TextBlock
                    {
                        Text = word[i].ToString(),
                        FontSize = 12,
                        Foreground = new SolidColorBrush(Colors.Black),
                        Width = boxSize,
                        Height = boxSize,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    Canvas.SetLeft(textBlock, letterX);
                    Canvas.SetTop(textBlock, letterY);
                    Canvas.Children.Add(textBlock);
                }
            }
        }
    }
}
