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
        private readonly eSpotOnBoard r_FirstOrSecondPlayer;
        private readonly bool r_ComputerOrPerson;
        public Player(eSpotOnBoard i_FirstOrSecondPlayer, bool i_ComputerOrPerson)
        {
            r_FirstOrSecondPlayer = i_FirstOrSecondPlayer;
            r_ComputerOrPerson = i_ComputerOrPerson;
        }
        public eSpotOnBoard Title
        {
            get
            {
                return r_FirstOrSecondPlayer;
            }
        }
        public bool ComputerOrPerson
        {
            get
            {
                return r_ComputerOrPerson;
            }
        }
    }
}
