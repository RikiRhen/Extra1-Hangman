using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extra1_Hangman
{
    internal class Switch
    {
        public void run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Welcome to CMD-Hangman!\n" +
                    "Select either to:\n" +
                    "Start - Start a new round\n" +
                    "Exit - Close down the program");
                string switchChoice = Helper.TakeInput()!.ToUpper();

                switch (switchChoice)
                {
                    case "START":
                        List<Letter> secretWord = Helper.ParseSecretWord(Helper.ChooseSecretWord());
                        StringBuilder guesses = new();
                        bool letterWasFound = false;
                        
                        Console.WriteLine("\nGame start!");
                        //Keeps looping until player runs out of guesses.
                        int triesLeft = 10;
                        while (triesLeft != 0)
                        {
                            Console.WriteLine("You have: " + triesLeft + " tries left");
                            //Prints out made guesses if any have been made.
                            if (guesses.Length > 0)
                            {
                                Console.WriteLine("Guesses made: " + guesses.ToString());
                            }

                            //Prints out found letters in the word, and replaces non-found letters with _.
                            Helper.PrintSecretWordLetters(secretWord);

                            //TODO: Make the player able to guess on the whole word.
                            //Player makes a guess, this guess is compared to the non-found letters in the secretWord list.
                            char guess = Helper.MakeSureInputIsSingleChar(Helper.TakeInput());
                            Helper.CheckGuessedChar(secretWord, guesses, ref letterWasFound, ref triesLeft, guess);

                            //Checks if the entire word has been discovered.
                            if (Helper.HasWordBeenDiscovered(secretWord))
                            {
                                Helper.VictoryText(secretWord);
                                break;
                            }

                        }
                        if (triesLeft == 0)
                        {
                            Console.WriteLine("Game over! You ran out of tries.\n\n");
                        }

                        break;

                    case "EXIT":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input, try again\n");
                        break;
                }
            }
        }




    }
}
