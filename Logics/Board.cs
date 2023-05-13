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
            set 
            { 
                m_BoardMatrix = value;
            }
        }
        internal void RestartBoard()
        {
            for(int i = 0; i < m_BoardMatrix.GetLength(0); i++)
            {
                for(int j = 0; j < m_BoardMatrix.GetLength(1); j++)
                {
                    m_BoardMatrix[i, j] = eSpotOnBoard.empty;
                }
            }
        }
        internal bool IsSpotTaken(int i_Row, int i_Column)
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
        internal bool IsRowIdentical(int i_Row)
        {
            int index = 0;
            int currColumnInRequestedLine = 0;
            bool res = true;
           Nullable <eSpotOnBoard> firstSpotOnLine = null;
            foreach(eSpotOnBoard element in m_BoardMatrix)
            {
                if(currColumnInRequestedLine==m_BoardMatrix.GetLength(1))
                {
                    break;
                }
                if (index == i_Row * m_BoardMatrix.GetLength(0))
                {
                    firstSpotOnLine = element;
                    currColumnInRequestedLine++;
                }
               else if(currColumnInRequestedLine < m_BoardMatrix.GetLength(1) &&firstSpotOnLine!=null)
                {
                    currColumnInRequestedLine++;
                    if(firstSpotOnLine!=element)
                    {
                        res = false;
                        break;
                    }
                }
                index++;         
            }
            return res;
        }
        internal bool IsColumnIdentical(int i_Column)
        {
            int index = 0;
            Nullable<eSpotOnBoard> firstSpotOnColumn = null;
            bool res = true;
            foreach (eSpotOnBoard element in m_BoardMatrix)
            {
                
                if(index % m_BoardMatrix.GetLength(0) == i_Column)
                {
                    if(firstSpotOnColumn == null)
                    {
                        firstSpotOnColumn = element;
                    }
                    else
                    {
                        if(firstSpotOnColumn != element)
                        {
                            res = false;
                            break;
                        }
                    }
                }
                index++;
            }
            return res;
        }
        internal bool IsMainDiagonalIdentical()
        {
            int index = 0;
            int currRow = 0;
            Nullable<eSpotOnBoard> firstSpotOnDiagonal = null;
            bool res = true;
                foreach (eSpotOnBoard element in m_BoardMatrix)
                {
                currRow = index / m_BoardMatrix.GetLength(0);
                    if(index == currRow + m_BoardMatrix.GetLength(0) * currRow)
                    {
                        if(firstSpotOnDiagonal == null)
                        {
                            firstSpotOnDiagonal = element;
                        }
                        else
                        {
                            if(firstSpotOnDiagonal != element)
                            {
                                res = false;
                                break;

                            }
                        }
                    }
                    index++;
                }
            return res;
        }
        internal bool IsSecondaryDiagonalIdentical()
        {
            int row= 0;
            int column = 0;
            int index = 0;
            bool res = true;
            Nullable<eSpotOnBoard> firstSpotOnDiagonal = null;
            foreach (eSpotOnBoard element in m_BoardMatrix)
            {
                column = index % m_BoardMatrix.GetLength(0);
                if (index !=0 && index % m_BoardMatrix.GetLength(0) == 0)
                {
                    row++;
                }
                if (row + column == m_BoardMatrix.GetLength(0) - 1)
                {
                    if (firstSpotOnDiagonal == null)
                    {
                        firstSpotOnDiagonal = element;
                    }
                    else
                    {
                        if (firstSpotOnDiagonal != element)
                        {
                            res = false;
                            break;
                        }
                    }
                }
                index++;
            }
            return res;
        }
          
    }
}
