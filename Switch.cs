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
                        int triesLeft = 10;
                        List<Letter> secretWord = Helper.ParseSecretWord(Helper.ChooseSecretWord());
                        StringBuilder guesses = new();
                        bool letterWasFound = false;

                        Helper.RunGame(ref triesLeft, secretWord, guesses, ref letterWasFound);

                        //Checks if the entire word has been discovered.
                        if (Helper.HasWordBeenDiscovered(secretWord))
                        {
                            break;
                        }

                        //Checks if player has run out of tries.
                        if (triesLeft == 0)
                        {
                            Helper.GameOver();
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
