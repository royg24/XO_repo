using Logics;
using UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UserInterface.InterFace;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            UserInterface.InterFace uf = new UserInterface.InterFace();
            do
            {
                uf.DurationOfGame();
            } while (AskForAnotherGame() == true);
        }
        public static bool AskForAnotherGame()
        {
            string answer = null;
            bool result;
            Console.WriteLine(@"
Do you want to play another game?
1. Yes
2. No");
            answer = Console.ReadLine();
            while (answer != "1" && answer != "2")
            {
                InvalidInputMessagePrint();
                answer = Console.ReadLine();
            }
            if(answer == "1")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
