using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.GameManager;

namespace Logics
{
    public class AllGamesData
    {
        private int m_NumberOfGames;
        private int m_NumberOfWinsToPlayer1;
        private int m_NumberOfWinsToPlayer2;
        private int m_NumberOfDraws;
        public AllGamesData()
        {
            m_NumberOfDraws = 0;
            m_NumberOfGames = 0;
            m_NumberOfWinsToPlayer1 = 0;
            m_NumberOfWinsToPlayer2 = 0;
        }
        public int NumberOfGames
        {
            get 
            {
                return m_NumberOfGames; 
            }
            set
            {
                m_NumberOfGames = value;
            }
        }
        public int NumberOfDraws
        {
            get
            {
                return m_NumberOfDraws;
            }
            set
            { 
                m_NumberOfDraws = value; 
            }
        }
        public int NumberOfWinsToPlayer1
        {
            get
            {
                return m_NumberOfWinsToPlayer1;
            }
            set
            {
                m_NumberOfWinsToPlayer1 = value;
            }
        }
        public int NumberOfWinsToPlayer2
        {
            get
            {
                return m_NumberOfWinsToPlayer2;
            }
            set
            {
                m_NumberOfWinsToPlayer2 = value;
            }
        }
        public void UpdateScoreIfPlayerLost(eGameSituations i_Loser)
        {
            if (i_Loser == eGameSituations.player1)
            {
                NumberOfWinsToPlayer2++;
            }
            else
            {
                NumberOfWinsToPlayer1++;
            }
        }
    }
}
