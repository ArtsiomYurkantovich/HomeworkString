using System;
using System.IO;
using System.Text.RegularExpressions;

namespace HomeWorkString
{
    class Program
    {
        private const string Clear = "clear";
        private const string Exit = "exit";
        private const string Info = "info";
        private const string LongWord = "lon";
        private const string NumbForWord = "numword";
        private const string QuesExclamation = "queses";
        private const string CommInSent = "comsen";
        private const string ReplLett = "replet";
        private const string NumOfDigInWord = "numdig";

        private static char[] delimiterChars = { '.', ',', '?', '!', ' ', ';', ':', '/', '|', '-', '\t', '\n','{','}','[',']','(',')'};
        private static bool RequestedExit;

        static void ShowCommands()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t**** List of commands: ****");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{Clear} - Clear console content.");
            Console.WriteLine($"{Exit} - Exit the application.");
            Console.WriteLine($"{LongWord} - Find the longest word and determine how many times it appears in the text.");
            Console.WriteLine($"{NumbForWord} - Changes numbers 0 through 9 with words.");
            Console.WriteLine($"{CommInSent} - Output of sentences not containing a comma.");
            Console.WriteLine($"{QuesExclamation} - Display of interrogative and exclamation sentences on the screen.");
            Console.WriteLine($"{ReplLett} - Words that start and end with the same letter.");
            Console.WriteLine($"{NumOfDigInWord} - Word containing the maximum number of digits.");
            Console.ResetColor();
        }

        static void ClearDisplay()
        {
            Console.Clear();
            ShowCommands();
        }

        static void Error()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Make sure that there is a \"Test.txt\" file on the disk \"C\" or create it.");
            Console.ResetColor();
        }

        static void SearchLongWord()
        {
            try
            {
                // Opening a text file on drive "C" called "Test.txt"

                var textFromFile = File.ReadAllText(@"D:\Test.txt");
                string[] text = textFromFile.Split(delimiterChars);

                int number = 0;

                int maxlen = text[0].Length, maxLong = 0;

                //Finding the longest word

                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i].Length > maxlen)
                    {
                        maxlen = text[i].Length;
                        maxLong = i;
                    }
                }

                // Determines how many times the longest word occurs in the text

                for (int i = 0; i < text.Length; i++)
                {
                    if (text[maxLong] == text[i])
                    {
                        number++;
                    }
                }
                
                Console.Write("Longest word in the text: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{text[maxLong]}");
                Console.ResetColor();
                
                Console.Write("Number of repetitions in the text: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{number}");
                Console.ResetColor();
            }
            catch
            {
                Error();
            }
        }

        // Method in which numbers are replaced by words

        static void NumberForWord()
        {
            try
            {
                var textFromFile = File.ReadAllText(@"D:\Test.txt");
                foreach (char digit in textFromFile)
                {
                    if (digit >= '0' && digit <= '9')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        
                        switch (digit)
                        {
                            case '0':
                                Console.Write(" zero ");
                                break;
                            case '1':
                                Console.Write(" one ");
                                break;
                            case '2':
                                Console.Write(" two ");
                                break;
                            case '3':
                                Console.Write(" three ");
                                break;
                            case '4':
                                Console.Write(" four ");
                                break;
                            case '5':
                                Console.Write(" five ");
                                break;
                            case '6':
                                Console.Write(" six ");
                                break;
                            case '7':
                                Console.Write(" seven ");
                                break;
                            case '8':
                                Console.Write(" eight ");
                                break;
                            case '9':
                                Console.Write(" nine ");
                                break;
                        }
                        Console.ResetColor();
                    }
                   
                    string textWithoutDigit = digit.ToString();
                    textWithoutDigit = textWithoutDigit.Trim('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                    Console.Write(textWithoutDigit);
                }
            }
            catch
            {
                Error();
            }
        }

        //The method displays first interrogative sentences, and then exclamation sentences in the text.
        static void QuestionAndExclamation()
        {
            try
            {
                string question = string.Empty;
                string exclam = string.Empty;
                
                var textFromFile = File.ReadAllText(@"D:\Test.txt");
                var sentences = textFromFile.Split(new[] { Environment.NewLine, "\t", "!", ".", ";" }, StringSplitOptions.RemoveEmptyEntries);
                
                Console.WriteLine("Interrogative sentences:");

                // Search for interrogative sentences in the text and display

                foreach (var sentence in sentences)
                {
                    int questionIndex = sentence.IndexOf('?');
                    int startWith = 0;
                   
                    while (questionIndex >= 0 && questionIndex < sentence.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        question = sentence.Substring(startWith, questionIndex + 1 - startWith).Trim();
                        Console.WriteLine(question);
                        startWith = questionIndex + 1;
                        questionIndex = sentence.IndexOf('?', startWith);
                        Console.ResetColor();
                    }
                }

                // Checks if there are interrogative sentences in the text and if not, then displays a message

                if (question == string.Empty)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("There are not interrogative sentences in the text.");
                    Console.ResetColor();
                }
               
                var exclamination = textFromFile.Split(new[] { Environment.NewLine, "\t", "?", ".", ";" }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("Exclamination sentences:");

                // // Search for exclamination sentences in the text and display

                foreach (var sentence in exclamination)
                {
                    int exclaminationIndex = sentence.IndexOf('!');
                    int startWith = 0;
                   
                    while (exclaminationIndex >= 0 && exclaminationIndex < sentence.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        exclam = sentence.Substring(startWith, exclaminationIndex + 1 - startWith).Trim();
                        Console.WriteLine(exclam);
                        startWith = exclaminationIndex + 1;
                        exclaminationIndex = sentence.IndexOf('!', startWith);
                        Console.ResetColor();
                    }
                    
                }

                // Checks if there are exclamation points in the text and if not, displays a message

                if (exclam == string.Empty)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("There are not exclamination sentences in the text.");
                    Console.ResetColor();
                }
            }
            catch
            {
                Error();
            }
        }

        // The method detects words that start and end with the same character and displays those words
        static void ReplacingNumbersWithLetters()
        {
            try
            {
                var textFromFile = File.ReadAllText(@"D:\Test.txt");
                string replacWord = string.Empty;

                // Removes extra spaces in text

                textFromFile = new Regex(@"\s+").Replace(textFromFile, " "); 
                
                Console.WriteLine("Words in which the first letter of the woeds is the last:");

                foreach (var word in textFromFile.Split(delimiterChars))
                {
                    if (word.Length > 1 && word[0] == word[word.Length - 1])
                    {
                        replacWord = word;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{replacWord}");
                        Console.ResetColor();
                    }
                }
               
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                if (replacWord == string.Empty)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("There are not words that include any numbers.");
                    Console.ResetColor();
                }

                Console.ResetColor();
            }
            catch
            {
                Error();
            }
            
        }

        // Search for sentences in the text that do not have commas and display them on the screen
        static void CommInSentence()
        {
            try
            {
                var textFromFile = File.ReadAllText(@"D:\Test.txt");
                // Removes extra spaces in text
                textFromFile = new Regex(@"\s+").Replace(textFromFile, " ");
               
                Console.WriteLine("Sentences without a comma:");
                
                while (textFromFile.Length > 0)
                {
                    int startIndex = textFromFile.IndexOfAny(new char[] { '\t', '?', '.', ';', '\n' }) + 1;
                   
                    if (startIndex == 0)
                    {
                        break;
                    }
                   
                    string sentenceWithoutComma = textFromFile.Remove(startIndex, textFromFile.Length - startIndex);
                    textFromFile = textFromFile.Remove(0, sentenceWithoutComma.Length);
                    
                    if (sentenceWithoutComma.IndexOf(',') == -1)
                    {
                        Console.WriteLine(sentenceWithoutComma);
                    }
                }
            }
            catch
            {
                Error();
            }

        }

        // Search for a word in the text containing the maximum number of digits
        static void NumberOfDigitsInWord()
        {
            try
            {
                var textFromFile = File.ReadAllText(@"D:\Test.txt");
               
                // Removes extra spaces in text
                textFromFile = new Regex(@"\s+").Replace(textFromFile, " "); 
               
                var words = textFromFile.Split(delimiterChars);
                string wordWithMaxDigitals = string.Empty;
                var maxDigitals = 0;

                foreach (var word in words)
                {
                    var currentDigitals = GetNumOfDigitals(word);
                   
                    if (currentDigitals > maxDigitals)
                    {
                        maxDigitals = currentDigitals;
                        wordWithMaxDigitals = word;
                    }
                }

                if (wordWithMaxDigitals == string.Empty)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("There are not words that include any numbers.");
                    Console.ResetColor();
                }
               
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"Word containing the maximum number of digit: {wordWithMaxDigitals}");
                    Console.ResetColor();
                }
             
            }
            catch
            {
                Error();
            }
        }

        // Is the character a digit
        static int GetNumOfDigitals(string str)
        {
            var count = 0;
            foreach(var ch in str)
            {
                if(ch >= '0' && ch <='9')
                {
                    count++;
                }
            }
            return count;
        }

        // Methode implements basic commands
        static void ApplyCommand()
        {
            Console.WriteLine();
            Console.Write("> ");
            
            string command = Console.ReadLine().ToLower();
            switch (command)
            {
                case Info:
                    ShowCommands();
                    break;
                case Clear:
                    ClearDisplay();
                    break;
                case Exit:
                    RequestedExit = true;
                    break;
                case LongWord:
                    SearchLongWord();
                    break;
                case NumbForWord:
                    NumberForWord();
                    break;
                case CommInSent:
                    CommInSentence();
                    break;
                case QuesExclamation:
                    QuestionAndExclamation();
                    break;
                case NumOfDigInWord:
                    NumberOfDigitsInWord();
                    break;
                case ReplLett:
                    ReplacingNumbersWithLetters();
                    break;
                default:
                    Console.WriteLine("Error! Please enter command at list information");
                    Console.WriteLine();
                    ShowCommands();
                    break;
            }
        }

        static void RunApplication()
        {
            ShowCommands();
          
            Console.WriteLine();
            Console.WriteLine("SELECT ONE OF THE COMMANDS!!!");

            // While RequestExit = false perform method ApplyCommand

            while (!RequestedExit)
            {
                ApplyCommand();
            }
        }

        static void Main(string[] args)
        {
            RunApplication();
        }
    }
}
