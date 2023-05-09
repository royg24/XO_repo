using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;

namespace Logics
{
    internal class Player
    {
        private int m_Score;
        private readonly eSpotOnBoard m_FirstOrSecondPlayer;
        private readonly bool m_ComputerOrPerson;
        public Player(eSpotOnBoard i_FirstOrSecondPlayer, bool computerOrPerson)
        {
            m_Score = 0;
            m_FirstOrSecondPlayer = i_FirstOrSecondPlayer;
            m_ComputerOrPerson = computerOrPerson;
        }
        public int Score
        {
            get
            {
                return m_Score;
            }
        }
        public string Title
        {
            get
            {
                return m_FirstOrSecondPlayer.ToString();
            }
        }
    }
}
