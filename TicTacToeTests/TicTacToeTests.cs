using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame;
using Xunit;

namespace TicTacToeTests
{
    public class TicTacToeTests
    {
        [Fact]
        public void AddGames()
        {
            var game1 = new Game(null,null);
            var game2 = new Game(null, null);

            Assert.NotEqual(game1.Id.ToString(), string.Empty);
            Assert.NotEqual(game2.Id.ToString(),string.Empty) ;
        }

        [Fact]
        public void Check_Win_Conditions_First_Row()
        {
            var p1 = new Player
            {
                Name = "Przemek",
                Mark = Mark.X
            };

            var p2 = new Player
            {
                Name = "Magda",
                Mark = Mark.O
            };

            var game1 = new Game(p1, p2);

            game1.MarkSpace(Mark.O, 0);
            game1.MarkSpace(Mark.O, 1);
            game1.MarkSpace(Mark.O, 2);

            Assert.Equal(Mark.O,game1.CheckWin());
        }

        [Fact]
        public void Check_Win_Conditions_Second_Row()
        {
            var p1 = new Player
            {
                Name = "Przemek",
                Mark = Mark.X
            };

            var p2 = new Player
            {
                Name = "Magda",
                Mark = Mark.O
            };

            var game1 = new Game(p1, p2);

            game1.MarkSpace(Mark.O, 3);
            game1.MarkSpace(Mark.O, 4);
            game1.MarkSpace(Mark.O, 5);

            Assert.Equal(Mark.O, game1.CheckWin());
        }

        [Fact]
        public void Check_Win_Conditions_Third_Row()
        {
            var p1 = new Player
            {
                Name = "Przemek",
                Mark = Mark.X
            };

            var p2 = new Player
            {
                Name = "Magda",
                Mark = Mark.O
            };

            var game1 = new Game(p1, p2);

            game1.MarkSpace(Mark.O, 6);
            game1.MarkSpace(Mark.O, 7);
            game1.MarkSpace(Mark.O, 8);

            Assert.Equal(Mark.O, game1.CheckWin());
        }

        [Fact]
        public void Check_Win_Conditions_First_Column()
        {
            var p1 = new Player
            {
                Name = "Przemek",
                Mark = Mark.X
            };

            var p2 = new Player
            {
                Name = "Magda",
                Mark = Mark.O
            };

            var game1 = new Game(p1, p2);

            game1.MarkSpace(Mark.O, 0);
            game1.MarkSpace(Mark.O, 3);
            game1.MarkSpace(Mark.O, 6);

            Assert.Equal(Mark.O, game1.CheckWin());
        }

        [Fact]
        public void Check_Win_Conditions_Second_Column()
        {
            var p1 = new Player
            {
                Name = "Przemek",
                Mark = Mark.X
            };

            var p2 = new Player
            {
                Name = "Magda",
                Mark = Mark.O
            };

            var game1 = new Game(p1, p2);

            game1.MarkSpace(Mark.O, 1);
            game1.MarkSpace(Mark.O, 4);
            game1.MarkSpace(Mark.O, 7);

            Assert.Equal(Mark.O, game1.CheckWin());
        }

        [Fact]
        public void Check_Win_Conditions_Third_Column()
        {
            var p1 = new Player
            {
                Name = "Przemek",
                Mark = Mark.X
            };

            var p2 = new Player
            {
                Name = "Magda",
                Mark = Mark.O
            };

            var game1 = new Game(p1, p2);

            game1.MarkSpace(Mark.O, 2);
            game1.MarkSpace(Mark.O, 5);
            game1.MarkSpace(Mark.O, 8);

            Assert.Equal(Mark.O, game1.CheckWin());
        }

        [Fact]
        public void Check_Win_Conditions_Slash()
        {
            var p1 = new Player
            {
                Name = "Przemek",
                Mark = Mark.X
            };

            var p2 = new Player
            {
                Name = "Magda",
                Mark = Mark.O
            };

            var game1 = new Game(p1, p2);

            game1.MarkSpace(Mark.O, 2);
            game1.MarkSpace(Mark.O, 4);
            game1.MarkSpace(Mark.O, 6);

            Assert.Equal(Mark.O, game1.CheckWin());
        }

        [Fact]
        public void Check_Win_Conditions_BackSlash()
        {
            var p1 = new Player
            {
                Name = "Przemek",
                Mark = Mark.X
            };

            var p2 = new Player
            {
                Name = "Magda",
                Mark = Mark.O
            };

            var game1 = new Game(p1, p2);

            game1.MarkSpace(Mark.O, 0);
            game1.MarkSpace(Mark.O, 4);
            game1.MarkSpace(Mark.O, 8);

            Assert.Equal(Mark.O, game1.CheckWin());
        }


    }
}
