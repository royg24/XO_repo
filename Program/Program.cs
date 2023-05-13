using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UserInterface.GameActivation;

namespace ReversedTicTacToe
{
    public class Program
    {
        public static void Main()
        {
            ActivateGame();
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(
 @"Thanks for playing, hope you had a good time!
Bye Bye...");
        }
    }
}
