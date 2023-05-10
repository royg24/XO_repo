using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;

namespace Logics
{
    public class Board
    {
        private eSpotOnBoard[,] m_BoardMatrix;
        public Board(int i_BoardSize)
        {
            m_BoardMatrix = new eSpotOnBoard[i_BoardSize, i_BoardSize];
        }
        public eSpotOnBoard[,] BoardMatrix
        {
            get 
            {
                return m_BoardMatrix; 
            }
        }
        public bool IsSpotTaken(int i_Row, int i_Column)
        {
            bool res;
            if (m_BoardMatrix[i_Row, i_Column] == eSpotOnBoard.empty)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }
        public bool IsRowIdentical(int i_Row)
        {
            bool res = true;
            int index = 0;
            if (m_BoardMatrix[0, i_Row] != eSpotOnBoard.empty)
            {
                foreach (eSpotOnBoard element in m_BoardMatrix)
                {
                    if ((index - i_Row) % m_BoardMatrix.GetLength(0) == 0)
                    {
                        if (m_BoardMatrix[index, i_Row] != m_BoardMatrix[0, i_Row])
                        {
                            res = false;
                            break;
                        }
                    }
                    index++;
                }
            }
            return res;
        }
    }
}
