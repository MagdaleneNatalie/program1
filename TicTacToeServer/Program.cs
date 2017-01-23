using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame;

namespace TicTacToeServer
{
    class Program
    {
        internal static Game game { get; private set; }

        internal static TcpCommands TcpCommand { get; set; }

        public static void Main()
        {
            try
            {
                Console.WriteLine("Podaj swoj nick i naciśnij ENTER ");

                var serverPlayer = new Player
                {
                    Name = Console.ReadLine(),
                    Mark = Mark.O
                };

                Console.WriteLine("Oczekiwanie na przeciwnika...");

                TcpCommand = new TcpCommands();

                var oponnentPlayer = GetOpponentPlayer();

                Console.WriteLine($"Przeciwnik podłączony {oponnentPlayer.Name}" );

                TcpCommand.SendNickToClient(serverPlayer.Name);

                game = new Game(oponnentPlayer, serverPlayer);

                Console.WriteLine($"Stworzenie gry {game.Id}");
                Console.WriteLine("Rozpoczęcie gry...");

                Play();


            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                TcpCommand.Server.Stop();
            }

            Console.ReadLine();
        }

        private static Player GetOpponentPlayer()
        {
            return new Player
            {
                Name = TcpCommand.GetOpponentPlayer(),
                Mark = Mark.X
            };
        }

        private static void ShowBoard(int[] grid)
        {
            Console.Clear();
            Console.WriteLine("-------");
            for (int i = 0; i < grid.Length-1; i+=3)
            {
                Console.WriteLine($"|{grid[i]}|{grid[i+1]}|{grid[i+2]}|");
            }
            Console.WriteLine("-------");
        }

        private static void Play()
        {
            while (TcpCommand.Client.Connected)
            {
                ShowBoard(game.Board.Grid);

                Console.WriteLine("Twój ruch...");

                game.MarkSpace(Mark.O, int.Parse(Console.ReadLine()));

                ShowBoard(game.Board.Grid);

                CheckWin(); 

                TcpCommand.SendBoardToClient(game.Board.Grid, 0);

                Console.WriteLine($"Czekam na ruch od: {game.PlayerX.Name}");

                var move = TcpCommand.GetMoveFromClient();

                game.MarkSpace(Mark.X, move);

                CheckWin();
            }

            TcpCommand.Client.Close();
        }

        private static void CheckWin()
        {
            var winSign = game.CheckWin();

            if (winSign != Mark.None)
            {
                Console.WriteLine($"Wygrał {winSign}");
                
                TcpCommand.SendBoardToClient(game.Board.Grid, (int)winSign);
            }
        }
    }
}
