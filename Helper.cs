using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly:InternalsVisibleTo("Tester")]

namespace Extra1_Hangman
{
    /// <summary>
    /// Contains helpermethods for the program.
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// Takes ReadLine() input, checks that the written input is not left empty of whitespace.
        /// </summary>
        /// <returns></returns>
        public static string TakeInput()
        {
            string result = Console.ReadLine()!;
            while (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine("Invalid input, try again\n");
                result = Console.ReadLine()!;
            }
            return result;
            //Takes ReadLine, cant be tested.
        }

        /// <summary>
        /// Takes input string, converts it to upper case characters and splits to a list of char.
        /// Checks to make sure the input string was only a single character long before returning it as a char.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static char MakeSureInputIsSingleChar(string input)
        {
            List<char> convertList = input.ToUpper().Where(c => !char.IsWhiteSpace(c)).ToList();
            while (convertList.Count != 1)
            {
                Console.WriteLine("Input should only be a single character, try again");
                convertList = TakeInput().ToUpper().Where(c => !char.IsWhiteSpace(c)).ToList();
            }
            return convertList[0];
            //Takes ReadLine, can't be tested.
        }

        /// <summary>
        /// Has a selection of secret words. Will randomly choose one and return it as a string.
        /// </summary>
        /// <returns></returns>
        public static string ChooseSecretWord()
        {
            //TODO: Fill out "words" from a file.
            string[] words = { "TESTER", "CANVAS", "TOLOUSE" };
            Random random = new Random();
            int randomNumber = random.Next(0, words.Length);
            return words[randomNumber];
            //Test by: running method, checking if returned word is among the words in the source file.
        }

        /// <summary>
        /// Takes an input string, parses it to a list of Letter-classes and returns the list.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<Letter> ParseSecretWord (string input)
        {
            string selectedWord = input;
            List<Char> wordInChars = selectedWord.ToUpper().Where(c => !char.IsWhiteSpace(c)).ToList();
            List<Letter> result = new List<Letter>();
            foreach (char c in wordInChars)
            {
                Letter l = new Letter(c);
                result.Add(l);
            }
            return result;
            //Test by: running method, making sure the return is a list of Letter-classes that spell out the input string.
        }

        /// <summary>
        /// Takes the secret word as a List<Letter>. Checks if the entire secret word has been discovered. Returns bool.
        /// </summary>
        /// <param name="secretWord"></param>
        /// <returns></returns>
        public static bool HasWordBeenDiscovered(List<Letter> secretWord)
        {
            bool wordIsFound = true;
            foreach (Letter l in secretWord)
            {
                if (!l.Found)
                {
                    wordIsFound = false;
                }
            }
            return wordIsFound;
            //Test by: reunning method, checking if the bool returned is correct or not.
        }

        /// <summary>
        /// Takes a List<Letter> and prints out the found letters and replaces non-found letters with _.
        /// </summary>
        /// <param name="secretWord"></param>
        public static void PrintSecretWordLetters(List<Letter> secretWord)
        {
            Console.Write("Secret word: ");
            foreach (Letter l in secretWord)
            {
                if (!l.Found)
                {
                    Console.Write("_");
                }
                else
                {
                    Console.Write(l.letter);
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Checks if the guessed character is in the secret word.
        /// </summary>
        /// <param name="secretWord"></param>
        /// <param name="guesses"></param>
        /// <param name="letterWasFound"></param>
        /// <param name="triesLeft"></param>
        /// <param name="guess"></param>
        public static void CheckGuessedChar(List<Letter> secretWord, StringBuilder guesses, ref bool letterWasFound, ref int triesLeft, char guess)
        {
            foreach (Letter l in secretWord)
            {
                if (!l.Found)
                {
                    if (guess == l.letter)
                    {
                        l.LetterFound();
                        letterWasFound = true;

                    }
                }
            }

            if (guesses.ToString().Contains(guess))
            {
                Console.WriteLine("The letter " + guess + " has already been guessed, try again!\n");
            }

            else if (letterWasFound)
            {
                Console.WriteLine("The letter " + guess + " is in the word!\n");
                triesLeft--;
            }
            else
            {
                Console.WriteLine("Sorry, the letter " + guess + " was not in the word. \n");
                guesses.Append(guess);
                triesLeft--;
            }
            letterWasFound = false;
        }

        /// <summary>
        /// Takes the secret word as a List<Letter> as parameter. Prints out the victory text together with the secret word. 
        /// </summary>
        /// <param name="secretWord"></param>
        public static void VictoryText(List<Letter> secretWord)
        {
            string finalWord = "";
            foreach (Letter l in secretWord)
            {
                finalWord += l.letter;
            }
            Console.WriteLine(@"\______/\_~* ^ *~_/\______/" +
                "You found the word! Congratulations!\n" +
                "Secret word: " + finalWord + "\n" +
                @"\______/\_~* ^ *~_/\______/" +
                "\n \n");
            //No real reason to test. Only manual testing to check formating.
        }
    }
}
