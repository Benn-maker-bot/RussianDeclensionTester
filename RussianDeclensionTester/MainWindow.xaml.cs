using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RussianDeclensionTester
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> nounDeclensions;
        private string currentNoun;
        private string currentCase;
        private string currentNumber;
        private static readonly List<string> cases = new List<string> { "nominative", "genitive", "dative", "accusative", "instrumental", "prepositional" };
        private static readonly List<string> numbers = new List<string> { "singular", "plural" };
        private Random random;

        public MainWindow()
        {
            InitializeComponent();
            InitializeNounDeclensions();
            random = new Random();
            NewQuestion();
        }

        private void InitializeNounDeclensions()
        {
            nounDeclensions = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>
            {
                {
                    "собака", new Dictionary<string, Dictionary<string, string>>
                    {
                        {
                            "singular", new Dictionary<string, string>
                            {
                                { "nominative", "собака" },
                                { "genitive", "собаки" },
                                { "dative", "собаке" },
                                { "accusative", "собаку" },
                                { "instrumental", "собакой" },
                                { "prepositional", "собаке" }
                            }
                        },
                        {
                            "plural", new Dictionary<string, string>
                            {
                                { "nominative", "собаки" },
                                { "genitive", "собак" },
                                { "dative", "собакам" },
                                { "accusative", "собак" },
                                { "instrumental", "собаками" },
                                { "prepositional", "собаках" }
                            }
                        }
                    }
                }
                // Add more nouns and their declensions here
            };
        }

        private void NewQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            NewQuestion();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer();
        }

        private void NewQuestion()
        {
            currentNoun = GetRandomKey(nounDeclensions);
            currentCase = GetRandomElement(cases);
            currentNumber = GetRandomElement(numbers);

            QuestionLabel.Content = $"Decline '{currentNoun}' in {currentNumber} {currentCase} case:";
            AnswerTextBox.Text = string.Empty;
        }

        private void CheckAnswer()
        {
            string correctAnswer = nounDeclensions[currentNoun][currentNumber][currentCase];
            string userAnswer = AnswerTextBox.Text.Trim();

            if (userAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Correct!", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Incorrect! The correct answer is '{correctAnswer}'.", "Result", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetRandomKey(Dictionary<string, Dictionary<string, Dictionary<string, string>>> dict)
        {
            List<string> keys = new List<string>(dict.Keys);
            return keys[random.Next(keys.Count)];
        }

        private string GetRandomElement(List<string> list)
        {
            return list[random.Next(list.Count)];
        }
    }
}
