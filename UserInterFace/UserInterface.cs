using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;

namespace Program
{
    internal class UserInterface
    {
        public const int IncreaseLineLength = 4;

        internal void StartGame()
        {
            Console.WriteLine(@"Welcome to reversed Tic-tac-toe!
                                Please enter the size of the board (a number between 1-9)");

        }
        internal void PrintBoard(eSpotOnBoard[,] i_GmaeBoard)
        {
            string cellOnBoard;
            int sizeOfLine = i_GmaeBoard.GetLength(0);
            int index = 0;
            foreach (eSpotOnBoard element in i_GmaeBoard)
            {
                cellOnBoard = string.Format("| {0} ", GetSymbolOfPlayer(element));
                Console.Write(cellOnBoard);
                if ((index + 1) % sizeOfLine == 0)
                {
                    Console.WriteLine("|");
                    PrintSeperatorLine(IncreaseLineLength * sizeOfLine + 1);
                }
                index++;
            }
        }
        internal string GetSymbolOfPlayer(eSpotOnBoard i_SpotOnBoard)
        {
            if (i_SpotOnBoard == eSpotOnBoard.player1)
            {
                return "X";
            }
            else if (i_SpotOnBoard == eSpotOnBoard.player2)
            {
                return "O";
            }
            else
            {
                return " ";
            }
        }
        internal void PrintSeperatorLine(int i_Size)
        {
            string seperatorLine = "";
            for (int i = 0; i < i_Size; i++)
            {
                seperatorLine += "=";
            }
            Console.WriteLine(seperatorLine);
        }
    }
}
