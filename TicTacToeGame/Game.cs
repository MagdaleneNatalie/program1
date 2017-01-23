
namespace TicTacToeGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public enum Mark
    {
        Empty = 0,
        X = 1,
        O = 2,
        None = 3
    }

    public class Game
    {
        public Guid Id { get; set; }
        public Board Board { get; set; }

        public Player PlayerX { get; private set; }
        public Player PlayerO { get; private set; }

        public Game(Player player1, Player player2)
        {
            switch (player1.Mark)
            {
                case Mark.O:
                  this.PlayerO = player1;
                  this.PlayerX = player2;
                    break;
                case Mark.X:
                    this.PlayerX = player1;
                    this.PlayerO = player2;
                    break;
                default:
                    throw new Exception("Gracz musi mieć wybraną stronę");
            }

            this.Id = new Guid();
            this.Board = new Board();
        }

        public void MarkSpace(Mark mark, int place)
        {
            this.Board.Grid[place] = (int)mark;
        }

        public Mark CheckWin()
        {
            var result = new List<Mark>();

            result.Add(this.CheckXoWinResult(0, 1, 2));
            result.Add(this.CheckXoWinResult(3, 4, 5));
            result.Add(this.CheckXoWinResult(6, 7, 8));
            result.Add(this.CheckXoWinResult(0, 3, 6));
            result.Add(this.CheckXoWinResult(1, 4, 7));
            result.Add(this.CheckXoWinResult(2, 5, 8));
            result.Add(this.CheckXoWinResult(0, 4, 8));
            result.Add(this.CheckXoWinResult(6, 4, 2));
            result.Add(this.CheckDraftResult());

            return result.FirstOrDefault(m => m != Mark.Empty);
        }

        private Mark CheckDraftResult()
        {
            if (this.Board.Grid.All(m => m != (int)Mark.Empty))
            {
                return Mark.None;
            }

            return Mark.Empty;
        }

        private Mark CheckXoWinResult(int place1, int place2, int place3)
        {
            if ((this.Board.Grid[place1] == (int)Mark.O) && 
                (this.Board.Grid[place2] == (int)Mark.O) && 
                (this.Board.Grid[place3] == (int)Mark.O))
            {
                return Mark.O;
            }

            if ((this.Board.Grid[place1] == (int)Mark.X) && 
                (this.Board.Grid[place2] == (int)Mark.X) && 
                (this.Board.Grid[place3] == (int)Mark.X))
            {
                return Mark.X;
            }

            return Mark.Empty;
        }
    }
}
