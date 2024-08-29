using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Lab3
{
    class PalindromeChecker
    {
        public static bool IsPalindrome(string word)
        {
            string cleanedWord = RemoveTrailingSpecialSymbols(word.ToLower());
            return cleanedWord == new string(cleanedWord.ToCharArray().Reverse().ToArray());
        }

        public static bool IsValidWord(string word)
        {
            return word.All(c => char.IsLetter(c) || char.IsPunctuation(c)) && !ContainsSpecialSymbolsInside(word);
        }

        public static bool ContainsSpecialSymbolsInside(string word)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                if (!char.IsLetter(word[i]) && !char.IsWhiteSpace(word[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private static string RemoveTrailingSpecialSymbols(string word)
        {
            while (word.Length > 0 && !char.IsLetter(word[word.Length - 1]))
            {
                word = word.Remove(word.Length - 1);
            }
            return word;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string value = TextBox_EnterValue.Text;

            if (PalindromeChecker.ContainsSpecialSymbolsInside(value))
            {
                MessageBox.Show("Слово не повинно містити спеціальні символи всередині.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!PalindromeChecker.IsValidWord(value))
            {
                MessageBox.Show("Будь ласка, введіть слово, а не символи чи цифри.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool result = PalindromeChecker.IsPalindrome(value);

            if (result)
            {
                Label_PalindromResult.Content = "Це слово є паліндромом.";
                BackgroundR.Fill = new SolidColorBrush(Colors.Green);
            }
            else
            {
                Label_PalindromResult.Content = "Це слово не є паліндромом.";
                BackgroundR.Fill = new SolidColorBrush(Colors.Red);
            }
        }
    }
}