using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    internal class Board
    {
        public int[] Grid { get; private set; }

        public Board()
        {
            this.Grid = new int[9] { (int)Mark.None, (int)Mark.None, (int)Mark.None, (int)Mark.None, (int)Mark.None, (int)Mark.None, (int)Mark.None, (int)Mark.None, (int)Mark.None };
        }
    }
}
