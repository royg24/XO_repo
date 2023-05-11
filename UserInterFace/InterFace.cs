﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;
using Logics;
using static Logics.GameManager;

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
Please enter the size of the board (a number between 1-9)");
            int boardSize = ReturnValidBoardSize();
            m_GameManager = new GameManager(boardSize);
            m_Player1 = new Player(eSpotOnBoard.player1, false);
            Console.WriteLine(
@"Choose the type of game:
1. Play against other player.
2. Play against the computer.");
            PlayerOrComputer();

        }
        public void DurationOfGame()
        {
            bool ThereIsSequence = false;
            bool PlayerQuits = false;
            bool BoardIsFull = false;
            bool IsSpotTaken = false;
            string row = null;
            string column = null;
            eSpotOnBoard CurrentPlayer;
            while(ThereIsSequence == false && PlayerQuits == false && BoardIsFull == false)
            {
                PrintBoard(m_GameManager.GameBoard.BoardMatrix);
                while (IsSpotTaken == false)
                {
                    GetChoosenSpotOnBoardFromPlayer(out row, out column);
                    if (m_GameManager.CheckIfAPlayerQuit(out CurrentPlayer, row) == true)
                    {
                        PlayerQuits = true;
                        break;
                    }
                    IsSpotTaken = m_GameManager.PlayGame(m_Player1, m_Player2, int.Parse(row), int.Parse(column));
                }
                if(m_GameManager.CheckForASequence(out CurrentPlayer, int.Parse(row), int.Parse(column)) == true)
                {
                    ThereIsSequence = true;
                    break;
                }
                if(m_GameManager.CheckIfBoardFull() == true)
                {
                    BoardIsFull = true;
                    break;
                }
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }
        public void GetChoosenSpotOnBoardFromPlayer(out string o_Row, out string o_Column)
        {
            Console.WriteLine(
@"Please choose a spot on the board.
Press Q to quit the game.
Enter the row's number:"
                             );
            o_Row = GetValidIndex();
            if(o_Row == QuitString)
            {
                o_Column = QuitString;
            }
            else
            {
                Console.WriteLine("Enter the column's number:");
                o_Column = GetValidIndex();
            }
        }
        public string GetValidIndex()
        {
            string input = null;
            int sizeOfBoard = m_GameManager.GameBoard.BoardMatrix.GetLength(0);
            do
            {
                if(input != null)
                {
                    InvalidInputMessagePrint();
                }
                input = Console.ReadLine();
                if (input == QuitString)
                {
                    break;
                }
            } while (int.Parse(input) < 1 || int.Parse(input) > sizeOfBoard);
            return input;
        }
        public void PlayerOrComputer()
        {
            string choice;
            choice = Console.ReadLine();
            while(choice != "1" && choice != "2")
            {
                InvalidInputMessagePrint();
                choice = Console.ReadLine();
            }
            if(choice == "1")
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
            string startingIndex;
            string upperIndex;
            int sizeOfLine = i_GmaeBoard.GetLength(0);
            int index = 0;
            for(int i = 0; i < sizeOfLine; i++)
            {
                upperIndex = string.Format("  {0} ", i + 1);
                Console.Write(upperIndex);
            }
            Console.WriteLine();
            foreach (eSpotOnBoard element in i_GmaeBoard)
            {
                if(index % sizeOfLine  == 0)
                {
                    startingIndex = string.Format("{0}", (index / sizeOfLine) + 1);
                    Console.Write(startingIndex);
                }
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
            string seperatorLine = " ";
            for (int i = 0; i < i_Size; i++)
            {
                seperatorLine += "=";
            }
            Console.WriteLine(seperatorLine);
        }
    }
}
