﻿using System;
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
        Empty = 3
    }

    public class Game
    {
        public Guid Id { get; }
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

            result.Add(this.CheckGrid(0, 1, 2));
            result.Add(this.CheckGrid(3, 4, 5));
            result.Add(this.CheckGrid(6, 7, 8));
            result.Add(this.CheckGrid(0, 3, 6));
            result.Add(this.CheckGrid(1, 4, 7));
            result.Add(this.CheckGrid(2, 5, 8));
            result.Add(this.CheckGrid(0, 4, 8));
            result.Add(this.CheckGrid(6, 4, 2));

            Mark? r = result.FirstOrDefault(m => m != Mark.Empty);

            return r == null ? Mark.None : (Mark)r;
        }

        private Mark CheckGrid(int place1, int place2, int place3)
        {
            if ((this.Board.Grid[place1] == this.Board.Grid[place2]) && (this.Board.Grid[place2] == this.Board.Grid[place3]))
            {
                return (Mark)this.Board.Grid[place1];
            }
            return Mark.None;
        }
    }
}
