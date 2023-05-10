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
        public GameManager(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
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
        public bool IsGameOver(Player i_CurrentPlaye)
        {
            //1. identical row
            //2. identical column
            //3. identical alachson
            //4. full board
            //. player quits
            return true;
        }
    }
}
