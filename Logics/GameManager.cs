using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;
namespace Logics
{
    public class GameManager
    {
        private Board m_Board;
        private List<Tuple<int, int>> m_EmptySpots;
        private int m_TurnsCounter;
        public GameManager(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
            m_EmptySpots = new List<Tuple<int, int>>(i_BoardSize * i_BoardSize);
            m_TurnsCounter = 0;
        }
        public Board GameBoard { 
            get
            {
                return m_Board;
            }
        }
        private bool humanTurn(Player i_CurrentPlayer, int i_Row, int i_Column)
        {
            bool result;
            if (m_Board.BoardMatrix[i_Row - 1, i_Column - 1] == eSpotOnBoard.empty)
            {
                m_Board.BoardMatrix[i_Row - 1, i_Column - 1] = i_CurrentPlayer.Title;
                m_EmptySpots.Remove(new Tuple<int, int>(i_Row - 1, i_Column - 1));
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        private void computerTurn(out int o_Row, out int o_Column)
        {
            Random random = new Random();
            int index;
            index = random.Next(0, m_EmptySpots.Count);
            o_Row = m_EmptySpots[index].Item1;
            o_Column = m_EmptySpots[index].Item2;
            m_Board.BoardMatrix[o_Row, o_Column] = eSpotOnBoard.player2;
            m_EmptySpots.Remove(new Tuple<int, int>(o_Row, o_Column));
            o_Row++;
            o_Column++;

        }
        public bool PlayGame(Player i_Player1, Player i_Player2, ref string io_Row, ref string io_Column) 
        {
            int row = int.Parse(io_Row);
            int column = int.Parse(io_Column);
            bool result;
            if (m_TurnsCounter % 2 == 0)
            {
                result = humanTurn(i_Player1, row, column);
            }
            else
            {
                if(i_Player2.ComputerOrPerson == true)
                {
                    computerTurn(out row, out column);
                    result = true;
                }
                else
                {
                    result = humanTurn(i_Player2, row, column);
                }
            }
            io_Row = row.ToString();
            io_Column = column.ToString();
            return result;
        }
        public bool CheckForASequence (out eSpotOnBoard o_CurrentPlayer, int i_Row, int i_Column)
        {
            bool result = false;
            int sizeOfRow = m_Board.BoardMatrix.GetLength(0);
            o_CurrentPlayer = CheckCurrentPlayer();
            if(m_Board.IsRowIdentical(i_Row) == true)
            {
                result = true;
            }
            else
            {
                if (m_Board.IsColumnIdentical(i_Column) == true)
                {
                    result = true;
                }
                else
                {
                    if (i_Row == i_Column)
                    {
                        if (m_Board.IsMainDiagonalIdentical() == true)
                        {
                            result = true;
                        }
                        else if(i_Row + i_Column == m_Board.BoardMatrix.GetLength(0) - 1)
                        {
                            if(m_Board.IsSecondaryDiagonalIdentical() == true)
                            {
                                result = true;
                            }
                        }
                    }
                    else if(i_Row + i_Column == m_Board.BoardMatrix.GetLength(0) - 1)
                    {
                        if (m_Board.IsSecondaryDiagonalIdentical() == true)
                        {
                            result = true;
                        }
                    }
                    
                }
            }
            return result;
        }
        public bool CheckIfAPlayerQuits(out eSpotOnBoard o_QuitingPlayer, string i_Input)
        {
            bool result;
            o_QuitingPlayer = CheckCurrentPlayer();
            if(i_Input == null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public bool CheckIfBoardFull()
        {
            bool result;
            if(m_EmptySpots.Count == 0)
            {
                result = true;
            }
            else
            {
                m_TurnsCounter++;
                result = false;
            }
            return result;
        }
        public eSpotOnBoard CheckCurrentPlayer()
        {
            eSpotOnBoard result;
            if (m_TurnsCounter % 2 == 0)
            {
                result = eSpotOnBoard.player1;
            }
            else
            {
                result = eSpotOnBoard.player2;
            }
            return result;
        }
        private void restartEmptySpotsList()
        {
            m_EmptySpots.Clear();
            for (int i = 0; i < m_Board.BoardMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.BoardMatrix.GetLength(1); j++)
                {
                    m_EmptySpots.Add(Tuple.Create(i, j));
                }
            }
        }
        public void RestartGame()
        {
            restartEmptySpotsList();
            m_Board.RestartBoard();
            m_TurnsCounter = 0;
        }
    }
}
