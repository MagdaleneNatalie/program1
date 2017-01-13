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
        public void Test()
        {
            Assert.Equal(0, 1);
        }

        [Fact]
        public void AddGames()
        {
            var game1 = new Game();
            var game2 = new Game();

            Assert.NotEqual(game1.Id.ToString(), string.Empty);
            Assert.NotEqual(game2.Id.ToString(),string.Empty) ;
        }


    }
}
