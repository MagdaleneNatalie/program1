using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    public enum Mark
    {
        None = 0,
        X = 1,
        O = 2,
    }

    public class Game
    {
        public Guid Id { get; }
        public Board Board { get; set; }

        public Game(Player player1, Player player2)
        {
            this.Id = new Guid();
            this.Board = new Board();
        }

        public void MarkSpace(Mark mark, int place)
        {
            this.Board.Grid[place] = (int)mark;
        }


        public Mark CheckWin()
        {
          
        }
    }
}
