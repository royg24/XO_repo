using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;
using Logics;

namespace UserInterface
{
    public class InterFace
    {
        public const int IncreaseLineLength = 4;
        private Player m_Player1;
        private Player m_Player2;
        private GameManager m_GameManager;

        public InterFace()
        {
            Console.WriteLine(
@"Welcome to reversed Tic-tac-toe!
Please enter the size of the board (a number between 1-9)
                             ");
            int boardSize = ReturnValidBoardSize();
            m_GameManager = new GameManager(boardSize);
            m_Player1 = new Player(eSpotOnBoard.player1, false);
            Console.WriteLine(
@"Choose the type of game:
1. Play against other player.
2. Play against the computer.
                             ");
            PlayerOrComputer();

        }
        public void DurationOfGame()
        {
            PrintBoard(m_GameManager.GameBoard.BoardMatrix);
        }
        public void PlayerOrComputer()
        {
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            while(choice != 1 && choice != 2)
            {
                InvalidInputMessagePrint();
                int.TryParse(Console.ReadLine(), out choice);
            }
            if(choice == 1)
            {
                m_Player2 = new Player(eSpotOnBoard.player2, false);
            }
            else
            {
                m_Player2 = new Player(eSpotOnBoard.player2, true);
            }
        }
        public int ReturnValidBoardSize()
        {
            int boardSize;
            int.TryParse(Console.ReadLine(), out boardSize);
            while (boardSize < 1 || boardSize > 9)
            {
                InvalidInputMessagePrint();
                int.TryParse(Console.ReadLine(), out boardSize);
            }
            return boardSize;
            
        }
        public void InvalidInputMessagePrint()
        {
            Console.WriteLine(
@"Invalid input!
please enter a new value."
                );
        }
        public void PrintBoard(eSpotOnBoard[,] i_GmaeBoard)
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
