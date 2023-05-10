using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Turn(Player i_CurrentPlayer, int i_Row, int i_Column)
        {

        }
        public void PlayGame(Player i_Player1, Player i_Player2, int i_Row, int i_Column) 
        {
            if(m_TurnsCounter % 2 == 0)
            {
                Turn(i_Player1, i_Row,i_Column);
            }
            else
            {
                Turn(i_Player2, i_Row, i_Column);
            }
            m_TurnsCounter++;
        }
        public bool CheckForASequence (Player i_CurrentPlayer, int i_Row, int i_Column)
        {
            //1. identical row
            //2. identical column
            //3. identical alachson
            return true;
        }
        public bool CheckIfAPlayerQuit(Player i_CurrentPlayer, string i_Input)
        {
            if(i_Input == QuitString)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckIfBoardFull()
        {
            if(m_TurnsCounter == m_Board.BoardMatrix.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
