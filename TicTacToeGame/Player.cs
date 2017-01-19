using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public Mark Mark { get; set; }
    }

}
