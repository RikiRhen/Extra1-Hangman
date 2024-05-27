using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extra1_Hangman
{
    internal class Letter(char letter)
    {
        public char letter { get; } = letter;
        public bool Found { get; private set; } = false;

        public void LetterFound()
        {
            Found = true;
        }
    }
}
