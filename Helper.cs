using System;
using System.IO;
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
        /// Has a selection of secret words. Will randomly choose one and return it as a string.
        /// </summary>
        /// <returns></returns>
        public static string ChooseSecretWord()
        {
            //Reads in the file "secretwords.txt" found in the solution folder. File should contain all possible words seperated by a comma.
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\secretwords.txt");
            string text = File.ReadAllText(filePath);
            //Creates an array with all the words from the textfile.
            string[] words = text.Split(',');

            //Randomly chooses one of the words to be the secret word
            Random random = new Random();
            int randomNumber = random.Next(0, words.Length);
            return words[randomNumber];
            //Test by: running method, checking if returned word is among the words in the source file.
        }


        /// <summary>
        /// Takes an input string, parses it to a char-array and returns the array.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static char[] ParseSecretWord (string input)
        {
            string selectedWord = input;
            List<Char> wordInChars = selectedWord.ToUpper().Where(c => !char.IsWhiteSpace(c)).ToList();
            char[] result = wordInChars.ToArray();
            foreach (char c in wordInChars)
            {
                result[wordInChars.IndexOf(c)] = c;
            }
            return result;
            //Test by: running method, making sure the returned array is properly parsed.
        }

        /// <summary>
        /// Takes the secret word as a char-array. Checks if the entire secret word has been discovered. Returns bool.
        /// </summary>
        /// <param name="secretWord"></param>
        /// <returns></returns>
        public static bool HasWordBeenDiscovered(char[] secretWord, StringBuilder guesses)
        {
            bool wordIsFound = true;
            foreach (char c in secretWord)
            {
                if (!guesses.ToString().Contains(c))
                {
                    wordIsFound = false;
                }
            }
            return wordIsFound;
            //Test by: running method, checking if the bool returned is correct or not.
        }

        /// <summary>
        /// Takes a char-array and prints out the found letters and replaces non-found letters with _.
        /// </summary>
        /// <param name="secretWord"></param>
        public static void PrintSecretWordLetters(char[] secretWord, StringBuilder guesses)
        {
            Console.Write("Secret word: ");
            foreach (char c in secretWord)
            {
                if (guesses.ToString().Contains(c))
                {
                    Console.Write(c);
                }
                else
                {
                    Console.Write("_");
                }
            }
            Console.WriteLine();
            //Void method, can't be tested.
        }

        /// <summary>
        /// Checks if the guessed character is in the secret word.
        /// </summary>
        /// <param name="secretWord"></param>
        /// <param name="guesses"></param>
        /// <param name="triesLeft"></param>
        /// <param name="guess"></param>
        public static void CheckGuessedChar(char[] secretWord, StringBuilder guesses, ref int triesLeft, char guess)
        {
            //Checks if the guessed character is in the secret word.
            bool letterWasFound = false;
            foreach (char c in secretWord)
            {
                if(c == guess)
                {
                    letterWasFound = true;
                }
            }

            //Checks if the letter has already been guessed before.
            if (guesses.ToString().Contains(guess))
            {
                Console.WriteLine("The letter " + guess + " has already been guessed, try again!\n");
            }

            //Prints positive result if the letter was found in the word.
            else if (letterWasFound)
            {
                Console.WriteLine("The letter " + guess + " is in the word!\n");
                guesses.Append(guess);
                triesLeft--;
            }
            //Prints a negative result if the letter was not found in the word.
            else
            {
                Console.WriteLine("Sorry, the letter " + guess + " was not in the word. \n");
                guesses.Append(guess);
                triesLeft--;
            }
            letterWasFound = false;
            //Void method, can't be tested.
        }

        /// <summary>
        /// Base-method of running the game and calling other helpermethods.
        /// </summary>
        /// <param name="triesLeft"></param>
        /// <param name="secretWord"></param>
        /// <param name="guesses"></param>
        public static void RunGame(ref int triesLeft, char[] secretWord, StringBuilder guesses)
        {
            Console.WriteLine("\nGame start!");
            //Keeps looping until player runs out of guesses.

            while (triesLeft != 0)
            {
                Console.WriteLine("You have: " + triesLeft + " tries left");
                //Prints out made guesses if any have been made.
                if (guesses.Length > 0)
                {
                    Console.WriteLine("Guesses made: " + guesses.ToString());
                }

                //Prints out found letters in the word, and replaces non-found letters with _.
                PrintSecretWordLetters(secretWord, guesses);

                //Player makes a guess, checks if the guess is a single character or a whole word.
                string guess = TakeInput().ToUpper();

                //If guess is a single letter, checks if the letter is in the word.
                if (guess.Length == 1)
                {
                    CheckGuessedChar(secretWord, guesses, ref triesLeft, Convert.ToChar(guess));
                } 

                //If guess is more than 2 characters, will assume input was a word to try and solve the riddle. Will check if the input word is the same as the secret word.
                else if (guess.Length > 1)
                {
                    //Build the secret word to a comparable string.
                    StringBuilder compare = new();
                    foreach (char c in secretWord)
                    {
                        compare.Append(c);
                    }

                    //Check if the guess is the same as the secret word. If it is, print victory text and break the loop.
                    if (guess.ToUpper().Equals(compare.ToString()))
                    {
                        VictoryText(secretWord);
                        break;
                    }

                    //If a single letter was given or the word was incorrect, remove a try.
                    triesLeft--;
                }
                

                //Checks if the entire word has been discovered.
                if (HasWordBeenDiscovered(secretWord, guesses))
                {
                    VictoryText(secretWord);
                    break;
                }
            }
            //Void method, can't be tested.
        }

        /// <summary>
        /// Takes the secret word as a char-array as parameter. Prints out the victory text together with the secret word. 
        /// </summary>
        /// <param name="secretWord"></param>
        public static void VictoryText(char[] secretWord)
        {
            StringBuilder finalWord = new();
            foreach (char c in secretWord)
            {
                finalWord.Append(c);
            }
            Console.WriteLine("\n----------------------------\n" +
                "You found the word! Congratulations!\n" +
                "Secret word: " + finalWord + "\n" +
                "----------------------------\n\n");
            //No real reason to test. Only manual testing to check formating.
        }

        /// <summary>
        /// Prints out the Game Over screen.
        /// </summary>
        public static void GameOver(char[] secretWord)
        {
            StringBuilder finalWord = new();
            foreach (char c in secretWord)
            {
                finalWord.Append(c);
            }
            Console.WriteLine("\n----------------------------\n" +
                "Game over! You ran out of tries.\n" +
                "Secret word was: " + finalWord +
                "\n----------------------------\n\n");
        }
        //No real reason to test. Only manual testing to check formating.
    }
}
