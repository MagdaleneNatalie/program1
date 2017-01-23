
namespace TicTacToeGame
{
    public class Board
    {
        public int[] Grid { get; private set; }

        public Board()
        {
            this.Grid = new int[9] { (int)Mark.Empty, (int)Mark.Empty, (int)Mark.Empty, (int)Mark.Empty, (int)Mark.Empty, (int)Mark.Empty, (int)Mark.Empty, (int)Mark.Empty, (int)Mark.Empty };
        }
    }
}
