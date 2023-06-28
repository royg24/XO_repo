using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Logics
{
    public class GameManager
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private List<Tuple<int, int>> m_EmptySpots;
        private int m_TurnsCounter;
        public Player Player1
        {
            get
            {
                return m_Player1;
            }
            set
            {
                m_Player1 = value;
            }
        }
        public Player Player2
        {
            get
            {
                return m_Player2;
            }
            set
            {
                m_Player2 = value;
            }
        }
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
        public void CreatePlayer2(bool i_PlayerOrComputer)
        {
            m_Player2 = new Player(eSpotOnBoard.player2, i_PlayerOrComputer);
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
        public eGameSituations CheckResultOfTurn(AllGamesData i_Data, eGameSituations i_CurrentPlayer, int i_Row, int i_Column)
        {
            eGameSituations result = CheckForASequence(i_CurrentPlayer, i_Row - 1, i_Column - 1);
            if (result != eGameSituations.notFinished)
            {
                i_Data.UpdateScoreIfPlayerLost(i_CurrentPlayer);
                changePlayer(result);
            }
            else if (CheckIfBoardFull())
            {
                i_Data.NumberOfDraws++;
                result = eGameSituations.tie;
            }
            return result;
        }
        public bool Turn(string i_Row, string i_Column, eGameSituations i_CurrentPlayer, ref eGameSituations io_Result, AllGamesData i_Data) 
        {
            bool result = true; 
            if (CheckIfAPlayerQuits(i_CurrentPlayer, i_Row))
            {
                i_Data.UpdateScoreIfPlayerLost(i_CurrentPlayer);
                io_Result = changePlayer(i_CurrentPlayer);
            }
            else
            {
                int row = int.Parse(i_Row);
                int column = int.Parse(i_Column);
                if (m_TurnsCounter % 2 == 0)
                {
                    result = humanTurn(m_Player1, row, column);
                }
                else
                {
                    if (m_Player2.ComputerOrPerson == true)
                    {
                        computerTurn(out row, out column);
                        result = true;
                    }
                    else
                    {
                        result = humanTurn(m_Player2, row, column);
                    }
                }
                if (io_Result == eGameSituations.notFinished)
                {
                    io_Result = CheckResultOfTurn(i_Data, i_CurrentPlayer, row, column);
                }
            }
            return result;
        }
        public eGameSituations CheckForASequence (eGameSituations i_CurrentPlayer, int i_Row, int i_Column)
        {
            int sizeOfRow = m_Board.BoardMatrix.GetLength(0);
            eGameSituations result = eGameSituations.notFinished;
            if(m_Board.IsRowIdentical(i_Row))
            {
                result = changePlayer(i_CurrentPlayer);
            }
            else
            {
                if (m_Board.IsColumnIdentical(i_Column))
                {
                    result = changePlayer(i_CurrentPlayer);
                }
                else
                {
                    if (i_Row == i_Column)
                    {
                        if (m_Board.IsMainDiagonalIdentical())
                        {
                            result = changePlayer(i_CurrentPlayer);
                        }
                        else if(i_Row + i_Column == m_Board.BoardMatrix.GetLength(0) - 1)
                        {
                            if(m_Board.IsSecondaryDiagonalIdentical())
                            {
                                result = changePlayer(i_CurrentPlayer);
                            }
                        }
                    }
                    else if(i_Row + i_Column == m_Board.BoardMatrix.GetLength(0) - 1)
                    {
                        if (m_Board.IsSecondaryDiagonalIdentical())
                        {
                            result = changePlayer(i_CurrentPlayer);
                        }
                    } 
                }
            }
            return result;
        }
        public bool CheckIfAPlayerQuits(eGameSituations o_QuitingPlayer, string i_Input)
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
        private eGameSituations changePlayer(eGameSituations i_Player)
        {
            if (i_Player == eGameSituations.player1)
            {
                i_Player = eGameSituations.player2;
            }
            else if(i_Player == eGameSituations.player2)
            {
                i_Player = eGameSituations.player1;
            }
            return i_Player;
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
        public eGameSituations CheckCurrentPlayer()
        {
            eGameSituations result;
            if (m_TurnsCounter % 2 == 0)
            {
                result = eGameSituations.player1;
            }
            else
            {
                result = eGameSituations.player2;
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
