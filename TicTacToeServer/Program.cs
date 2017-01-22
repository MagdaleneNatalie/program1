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
        internal static BinaryFormatter binaryFormatter = new BinaryFormatter();

        internal static TcpClient client { get; private set; }

        internal static NetworkStream stream { get; private set; }

        internal static Game game { get; private set; }

        public static void Main()
        {
            TcpListener server = null;

            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), 13000);
                server.Start();

                Console.WriteLine("Podaj swoj nick i naciśnij ENTER ");

                var serverPlayer = new Player
                {
                    Name = Console.ReadLine(),
                    Mark = Mark.O
                };

                Console.WriteLine("Oczekiwanie na przeciwnika...");

                client = server.AcceptTcpClient();
                stream = client.GetStream();

                var oponnentPlayer = GetOpponentPlayer();

                Console.WriteLine($"Przeciwnik podłączony {oponnentPlayer.Name}" );

                SendNickToClient(serverPlayer.Name);

                game = new Game(oponnentPlayer, serverPlayer);

                Console.WriteLine($"Stworzenie gry {game.Id}");
                Console.WriteLine("Rozpoczęcie gry...");

                var sendTask = new Task(Game);

                sendTask.Start();
                sendTask.Wait();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            Console.ReadLine();
        }

        private static void SendNickToClient(string name)
        {
            var nick = Encoding.ASCII.GetBytes(name);

            stream.Write(nick, 0, nick.Length);
        }

        private static Player GetOpponentPlayer()
        {
            var bufferNick = new byte[20];

            stream.Read(bufferNick, 0, 20);

            return new Player
            {
                Name = Encoding.ASCII.GetString(bufferNick),
                Mark = Mark.X
            };
        }

        private static void ShowBoard(int[] grid)
        {
            Console.WriteLine("-------");
            for (int i = 0; i < grid.Length; i+=3)
            {
                Console.WriteLine($"|{grid[i]}|{grid[i+1]}|{grid[i+2]}|");
            }
            Console.WriteLine("-------");
        }

        private static void Game()
        {
            while (client.Connected)
            {
                ShowBoard(game.Board.Grid);
                Console.WriteLine("Twój ruch...");

                var space = int.Parse(Console.ReadLine());

                game.MarkSpace(Mark.O, space);

                var winSign = game.CheckWin();

                if (winSign != Mark.None)
                {
                    Console.WriteLine($"Wygrał {winSign}");
                    var ms1 = new MemoryStream(64);
                    var bf1 = new BinaryFormatter();

                    bf1.Serialize(ms1, winSign);

                    stream.Write(ms1.GetBuffer(), 0, 64);
                }

                var ms = new MemoryStream();
                var bf = new BinaryFormatter();

                bf.Serialize(ms, game.Board.Grid);

                stream.Write(ms.GetBuffer(), 0, (int)ms.Length);

                Console.WriteLine($"Czekam na ruch {game.PlayerX.Name}");

                var bufer = new byte[1];
                stream.Read(bufer, 0, 1);

                var space1 = Encoding.ASCII.GetString(bufer);

                game.MarkSpace(Mark.X, int.Parse(space1));

            }

            client.Close();
        }
    }
}
