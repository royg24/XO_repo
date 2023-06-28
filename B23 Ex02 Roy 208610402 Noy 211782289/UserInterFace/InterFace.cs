using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;
using Logics;
using static Logics.GameManager;
using System.Runtime.Remoting.Lifetime;

namespace UserInterface
{
    internal class InterFace
    {
        private const int k_IncreaseLineLength = 4;
        private const int k_LengthForShowBoard = 3;
        private Player m_Player1;
        private Player m_Player2;
        private GameManager m_GameManager;
        private AllGamesData m_AllGamesData;

        internal InterFace()
        {
            string waitForEnter = null;
            Console.WriteLine("Welcome to reversed Tic-tac-toe!");
            printBoardForShowInTheStartOfTheGame();
            Console.WriteLine(
@"Enjoy the game!!!
Press ENTER to start the game");
            while(waitForEnter != "")
            {
                waitForEnter = Console.ReadLine();
            }
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(
@"Please enter the size of the board(a number between 3 - 9)
and then press ENTER");
            int boardSize = returnValidBoardSize();
            m_GameManager = new GameManager(boardSize);
            m_Player1 = new Player(eSpotOnBoard.player1, false);
            m_AllGamesData = new AllGamesData();
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(
@"Choose the type of game:
1. Play against other player.
2. Play against the computer.
and then press ENTER");
            playerOrComputer();
            Ex02.ConsoleUtils.Screen.Clear();
        }
        internal void DurationOfGame()
        {
            int turnsCounter = 0;
            bool thereIsSequence = false;
            bool playerQuits = false;
            bool boardIsFull = false;
            bool IsSpotNotTaken = false;
            string row = null;
            string column = null;
            eSpotOnBoard currentPlayer;
            m_AllGamesData.NumberOfGames++;
            m_GameManager.RestartGame();
            Ex02.ConsoleUtils.Screen.Clear();
            while (thereIsSequence == false && playerQuits == false && boardIsFull == false)
            {
                printBoard(m_GameManager.GameBoard.BoardMatrix);
                do
                {
                    if (turnsCounter % 2 == 1 && m_Player2.ComputerOrPerson == true)
                    {
                        m_GameManager.PlayGame(m_Player1, m_Player2, ref row, ref column);
                        turnsCounter++;
                        break;
                    }
                    else
                    {
                        getChoosenSpotOnBoardFromPlayer(out row, out column);
                        if (m_GameManager.CheckIfAPlayerQuits(out currentPlayer, row) == true)
                        {
                            playerQuits = true;
                            updateScoreIfPlayerLost(currentPlayer);
                            break;
                        }
                        IsSpotNotTaken = m_GameManager.PlayGame(m_Player1, m_Player2, ref row, ref column);
                        if (IsSpotNotTaken == true)
                        {
                            turnsCounter++;
                        }
                        else
                        {
                            Console.WriteLine("This spot is taken, please choose another one.");
                        }
                    }
                } while (IsSpotNotTaken == false);
                if (playerQuits == false)
                {
                    if (m_GameManager.CheckForASequence(out currentPlayer, int.Parse(row) - 1, int.Parse(column) - 1) == true)
                    {
                        thereIsSequence = true;
                        updateScoreIfPlayerLost(currentPlayer);
                        break;
                    }
                    if (m_GameManager.CheckIfBoardFull() == true)
                    {
                        boardIsFull = true;
                        m_AllGamesData.NumberOfDraws++;
                        break;
                    }
                }
                Ex02.ConsoleUtils.Screen.Clear();
            }
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard(m_GameManager.GameBoard.BoardMatrix);
            showScoreBoard(m_AllGamesData);
        }
        private void updateScoreIfPlayerLost(eSpotOnBoard i_Loser)
        {
            if (i_Loser == eSpotOnBoard.player1)
            {
                m_AllGamesData.NumberOfWinsToPlayer2++;
            }
            else
            {
                m_AllGamesData.NumberOfWinsToPlayer1++;
            }
        }
        private void getChoosenSpotOnBoardFromPlayer(out string o_Row, out string o_Column)
        {
            Console.WriteLine(
@"Please choose a spot on the board.
Press Q to quit the game.
Enter the row's number and then press ENTER:"
                             );
            o_Row = getValidIndex();
            if (o_Row == null)
            {
                o_Column = null;
            }
            else
            {
                Console.WriteLine("Enter the column's number and then press ENTER:");
                o_Column = getValidIndex();
            }
        }
        private string getValidIndex()
        {
            string input = null;
            int sizeOfBoard = m_GameManager.GameBoard.BoardMatrix.GetLength(0);
            input = Console.ReadLine();
            while (!isStringCanBeParseToInt(input))
            {
                if (input == "Q")
                {
                    return null;
                }
                InvalidInputMessagePrint();
                input = Console.ReadLine();
            }
            int parseToInt = int.Parse(input);
            if (parseToInt < 1 || parseToInt > sizeOfBoard)
            {
                InvalidInputMessagePrint();
                input = getValidIndex();
            }

            return input;
        }
        private bool isStringCanBeParseToInt(string i_String)
        {
            bool res = true;
            foreach (char element in i_String)
            {
                if (element < 48 || element > 57)
                {
                    res = false;
                    break;
                }
            }
            return res;

        }
        private void playerOrComputer()
        {
            string choice;
            choice = Console.ReadLine();
            while (choice != "1" && choice != "2")
            {
                InvalidInputMessagePrint();
                choice = Console.ReadLine();
            }
            if (choice == "1")
            {
                m_Player2 = new Player(eSpotOnBoard.player2, false);
            }
            else
            {
                m_Player2 = new Player(eSpotOnBoard.player2, true);
            }
        }
        private int returnValidBoardSize()
        {
            int boardSize;
            int.TryParse(Console.ReadLine(), out boardSize);
            while (boardSize < 3 || boardSize > 9)
            {
                InvalidInputMessagePrint();
                int.TryParse(Console.ReadLine(), out boardSize);
            }
            return boardSize;

        }
        internal static void InvalidInputMessagePrint()
        {
            Console.WriteLine(
@"Invalid input!
please enter a new value."
                );
        }
        private void printBoard(eSpotOnBoard[,] i_GmaeBoard)
        {
            string cellOnBoard;
            string startingIndex;
            string upperIndex;
            int sizeOfLine = i_GmaeBoard.GetLength(0);
            int index = 0;
            for (int i = 0; i < sizeOfLine; i++)
            {
                upperIndex = string.Format("  {0} ", i + 1);
                Console.Write(upperIndex);
            }
            Console.WriteLine();
            foreach (eSpotOnBoard element in i_GmaeBoard)
            {
                if (index % sizeOfLine == 0)
                {
                    startingIndex = string.Format("{0}", (index / sizeOfLine) + 1);
                    Console.Write(startingIndex);
                }
                cellOnBoard = string.Format("| {0} ", getSymbolOfPlayer(element));
                Console.Write(cellOnBoard);
                if ((index + 1) % sizeOfLine == 0)
                {
                    Console.WriteLine("|");
                    printSeperatorLine(k_IncreaseLineLength * sizeOfLine + 1);
                }
                index++;
            }
        }
        private string getSymbolOfPlayer(eSpotOnBoard i_SpotOnBoard)
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
        private void printSeperatorLine(int i_Size)
        {
            string seperatorLine = " ";
            for (int i = 0; i < i_Size; i++)
            {
                seperatorLine += "=";
            }
            Console.WriteLine(seperatorLine);
        }
        private void showScoreBoard(AllGamesData i_AllGamesData)
        {
            string PresentageOfPlayer1 = (((double)i_AllGamesData.NumberOfWinsToPlayer1 / i_AllGamesData.NumberOfGames) * 100).ToString("0.00");
            string PresentageOfPlayer2 = (((double)i_AllGamesData.NumberOfWinsToPlayer2 / i_AllGamesData.NumberOfGames) * 100).ToString("0.00");
            string PresentageOfDraw = (((double)i_AllGamesData.NumberOfDraws / i_AllGamesData.NumberOfGames) * 100).ToString("0.00");
            string table = string.Format
                (
@"
Score Board:
=========================================
{0} games were played.
=========================================
Player 1 won {1} games ({2}%).
=========================================
Player 2 won {3} games ({4}%).
=========================================
{5} games were finish in draw ({6}%).
=========================================
"
             , i_AllGamesData.NumberOfGames, i_AllGamesData.NumberOfWinsToPlayer1, PresentageOfPlayer1,
i_AllGamesData.NumberOfWinsToPlayer2, PresentageOfPlayer2, i_AllGamesData.NumberOfDraws, PresentageOfDraw);
            Console.WriteLine(table);
        }
        private void printBoardForShowInTheStartOfTheGame()
        {
            Board printedBoard = new Board(k_LengthForShowBoard);
            
            for (int i = 0; i < k_LengthForShowBoard ; i++)
            {
                for (int j = 0; j < k_LengthForShowBoard; j++)
                {
                    if((i + j) % 2 == 0)
                    {
                        printedBoard.BoardMatrix[i, j] = eSpotOnBoard.player1;
                    }
                    else
                    {
                        printedBoard.BoardMatrix[i, j] = eSpotOnBoard.player2;
                    }
                }
            }
            printBoard(printedBoard.BoardMatrix);
        }
    }
}
