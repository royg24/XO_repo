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
        public const string QuitString = "Q";
        private Board m_Board;
        private int m_TurnsCounter;
        public GameManager(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
            m_TurnsCounter = 0;
        }
        public Board GameBoard { 
            get
            {
                return m_Board;
            }
        }
        public bool Turn(Player i_CurrentPlayer, int i_Row, int i_Column)
        {
            bool result;
            if (m_Board.BoardMatrix[i_Row - 1, i_Column - 1] == eSpotOnBoard.empty)
            {
                m_Board.BoardMatrix[i_Row - 1, i_Column - 1] = i_CurrentPlayer.Title;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public bool PlayGame(Player i_Player1, Player i_Player2, int i_Row, int i_Column) 
        {
            bool result;
            if(m_TurnsCounter % 2 == 1)
            {
                result = Turn(i_Player1, i_Row,i_Column);
            }
            else
            {
                result = Turn(i_Player2, i_Row, i_Column);
            }
            m_TurnsCounter++;
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
                    }
                    //add secondary
                }
            }
            return result;
        }
        public bool CheckIfAPlayerQuit(out eSpotOnBoard o_QuitingPlayer, string i_Input)
        {
            bool result;
            o_QuitingPlayer = CheckCurrentPlayer();
            if(i_Input == QuitString)
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
            if(m_TurnsCounter == m_Board.BoardMatrix.Length)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public eSpotOnBoard CheckCurrentPlayer()
        {
            eSpotOnBoard result;
            if (m_TurnsCounter % 2 == 1)
            {
                result = eSpotOnBoard.player1;
            }
            else
            {
                result = eSpotOnBoard.player2;
            }
            return result;
        }
    }
}
