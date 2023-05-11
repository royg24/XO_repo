using Logics;
using UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Logics.BoardSpot;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            UserInterface.InterFace uf = new UserInterface.InterFace();
            // uf.DurationOfGame();
            Board b = new Board(5);
           Player p1 = new Player(eSpotOnBoard.player1, true);

            b.BoardMatrix[0, 4] = eSpotOnBoard.player2;
            b.BoardMatrix[1, 3] = eSpotOnBoard.player2;
            b.BoardMatrix[2, 2] = eSpotOnBoard.player1;
            b.BoardMatrix[3, 1] = eSpotOnBoard.player2;
            b.BoardMatrix[4, 0] = eSpotOnBoard.player1;
            
            



            uf.PrintBoard(b.BoardMatrix);
            
        }
    }
}
