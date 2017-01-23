using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame;

namespace TicTacToeTests
{
    [TestFixture]
    public class TicTacToeTests
    {
         [Test]
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

            Assert.AreEqual(Mark.O, game1.CheckWin());
        }

         [Test]
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

            Assert.AreEqual(Mark.O, game1.CheckWin());
        }

         [Test]
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

            Assert.AreEqual(Mark.O, game1.CheckWin());
        }

         [Test]
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

            Assert.AreEqual(Mark.O, game1.CheckWin());
        }

         [Test]
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

            Assert.AreEqual(Mark.O, game1.CheckWin());
        }

         [Test]
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

            Assert.AreEqual(Mark.O, game1.CheckWin());
        }

         [Test]
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

            Assert.AreEqual(Mark.O, game1.CheckWin());
        }

         [Test]
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

            Assert.AreEqual(Mark.O, game1.CheckWin());
        }

         [Test]
        public void Check_Win_Conditions_None()
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
            game1.MarkSpace(Mark.O, 3);
            game1.MarkSpace(Mark.O, 4);
            game1.MarkSpace(Mark.O, 6);
            game1.MarkSpace(Mark.O, 8);

            game1.MarkSpace(Mark.X, 0);
            game1.MarkSpace(Mark.X, 2);
            game1.MarkSpace(Mark.X, 5);
            game1.MarkSpace(Mark.X, 7);

            Assert.AreEqual(Mark.None, game1.CheckWin());
        }


    }
}
