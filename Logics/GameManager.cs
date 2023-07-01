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
        private AllGamesData m_AllGamesData;
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
        public AllGamesData Data
        {
            get
            {
                return m_AllGamesData;
            }
        }
        public GameManager(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
            m_AllGamesData = new AllGamesData();
            m_EmptySpots = new List<Tuple<int, int>>(i_BoardSize * i_BoardSize);
            Player1 = new Player(eSpotOnBoard.player1, false);
            m_TurnsCounter = 0;
        }
        public Board GameBoard { 
            get
            {
                return m_Board;
            }
        }
        private bool humanTurnInner(eSpotOnBoard i_CurrentPlayer, int i_Row, int i_Column)
        {
            bool result;
            if (m_Board.BoardMatrix[i_Row - 1, i_Column - 1] == eSpotOnBoard.empty)
            {
                m_Board.BoardMatrix[i_Row - 1, i_Column - 1] = i_CurrentPlayer;
                m_EmptySpots.Remove(new Tuple<int, int>(i_Row - 1, i_Column - 1));
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public bool HumanTurn(eSpotOnBoard i_CurrentPlayer, string i_Row, string i_Column)
        {
            return humanTurnInner(i_CurrentPlayer, int.Parse(i_Row), int.Parse(i_Column));
        }
        public void CreatePlayer2(bool i_PlayerOrComputer)
        {
            m_Player2 = new Player(eSpotOnBoard.player2, i_PlayerOrComputer);
        }
        private void computerTurnInner(out string o_Row, out string o_Column)
        {
            int row, column;
            Random random = new Random();
            int index;
            index = random.Next(0, m_EmptySpots.Count);
            row = m_EmptySpots[index].Item1;
            column = m_EmptySpots[index].Item2;
            m_Board.BoardMatrix[row, column] = eSpotOnBoard.player2;
            m_EmptySpots.Remove(new Tuple<int, int>(row, column));
            row++;
            column++;
            o_Row = row.ToString();
            o_Column = column.ToString();
        }
        public void ComputerTurn(out string o_Row, out string o_Column, ref eGameSituations io_Result, eSpotOnBoard i_CurrentPlayer, AllGamesData i_Data)
        {
            computerTurnInner(out o_Row, out o_Column);
        }
        private eGameSituations checkResultOfTurnInner(AllGamesData i_Data, eSpotOnBoard i_CurrentPlayer, int i_Row, int i_Column)
        {
            eGameSituations result = CheckForASequence(i_CurrentPlayer, i_Row - 1, i_Column - 1);
            if (result != eGameSituations.notFinished)
            {
                i_Data.UpdateScoreIfPlayerLost(i_CurrentPlayer);
                getWinner(i_CurrentPlayer);
            }
            else if (CheckIfBoardFull())
            {
                i_Data.NumberOfDraws++;
                result = eGameSituations.tie;
            }
            return result;
        }
        public void CheckResultOfTurn(string i_Row, string i_Column, eSpotOnBoard i_CurrentPlayer, ref eGameSituations io_Result, AllGamesData i_Data) 
        {
            int row = int.Parse(i_Row);
            int column = int.Parse(i_Column);
            if (io_Result == eGameSituations.notFinished)
            {
                io_Result = checkResultOfTurnInner(i_Data, i_CurrentPlayer, row, column);
            }
        }
        public eGameSituations CheckForASequence (eSpotOnBoard i_CurrentPlayer, int i_Row, int i_Column)
        {
            int sizeOfRow = m_Board.BoardMatrix.GetLength(0);
            eGameSituations result = eGameSituations.notFinished;
            if(m_Board.IsRowIdentical(i_Row))
            {
                result = getWinner(i_CurrentPlayer);
            }
            else
            {
                if (m_Board.IsColumnIdentical(i_Column))
                {
                    result = getWinner(i_CurrentPlayer);
                }
                else
                {
                    if (i_Row == i_Column)
                    {
                        if (m_Board.IsMainDiagonalIdentical())
                        {
                            result = getWinner(i_CurrentPlayer);
                        }
                        else if(i_Row + i_Column == m_Board.BoardMatrix.GetLength(0) - 1)
                        {
                            if(m_Board.IsSecondaryDiagonalIdentical())
                            {
                                result = getWinner(i_CurrentPlayer);
                            }
                        }
                    }
                    else if(i_Row + i_Column == m_Board.BoardMatrix.GetLength(0) - 1)
                    {
                        if (m_Board.IsSecondaryDiagonalIdentical())
                        {
                            result = getWinner(i_CurrentPlayer);
                        }
                    } 
                }
            }
            return result;
        }
        public bool CheckIfAPlayerQuits(eSpotOnBoard i_CurrentPlayer, string i_Input, ref eGameSituations io_Result)
        {
            bool result = false;
            i_CurrentPlayer = CheckCurrentPlayer();
            if(i_Input == null)
            {
                result = true;
                m_AllGamesData.UpdateScoreIfPlayerLost(i_CurrentPlayer);
                io_Result = declareWiner(i_CurrentPlayer);
            }
            return result;
        }
        private eGameSituations declareWiner(eSpotOnBoard i_Loser)
        {
            eGameSituations winner = eGameSituations.notFinished;
            if(i_Loser == eSpotOnBoard.player1)
            {
                winner = eGameSituations.player2Won;
            }
            else if(i_Loser == eSpotOnBoard.player2)
            {
                winner = eGameSituations.player1Won;
            }
            return winner;
        }
        private eGameSituations getWinner(eSpotOnBoard i_Loser)
        {
            eGameSituations winner = eGameSituations.notFinished;
            if (i_Loser == eSpotOnBoard.player1)
            {
                winner = eGameSituations.player2Won;
            }
            else if(i_Loser == eSpotOnBoard.player2)
            {
                winner = eGameSituations.player1Won;
            }
            return winner;
        }
        public void ChangePlayer(ref eSpotOnBoard i_Player)
        {
            if(i_Player == eSpotOnBoard.player1)
            {
                i_Player = eSpotOnBoard.player2;
            }
            else if(i_Player == eSpotOnBoard.player2)
            {
                i_Player = eSpotOnBoard.player1;
            }
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
