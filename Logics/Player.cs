using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;

namespace Logics
{
    public class Player
    {
        private readonly eSpotOnBoard m_FirstOrSecondPlayer;
        private readonly bool m_ComputerOrPerson;
        public Player(eSpotOnBoard i_FirstOrSecondPlayer, bool computerOrPerson)
        {
            m_FirstOrSecondPlayer = i_FirstOrSecondPlayer;
            m_ComputerOrPerson = computerOrPerson;
        }
        public eSpotOnBoard Title
        {
            get
            {
                return m_FirstOrSecondPlayer;
            }
        }
    }
}
