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
        public static void Main()
        {
            TcpListener server = null;

            

           

            try
            {

                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);
                server.Start();



                Console.WriteLine("Podaj swoje nick i naciśnij ENTER ");
                var nick = Console.ReadLine();
                var nickByte = Encoding.ASCII.GetBytes(nick);

                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();


                var bufferNick = new byte[20];
                stream.Read(bufferNick, 0, 20);
                var opponentNick = Encoding.ASCII.GetString(bufferNick);
                Console.WriteLine($"Podłączony gracz o nicku {opponentNick}");

               
                stream.Write(nickByte, 0, nickByte.Length);

                var game = new Game(new Player { Name = opponentNick }, new Player { Name = nick });

                Console.WriteLine($"Stworzenie gry {game.Id}");
                Console.WriteLine("Rozpoczęcie gry...");

                var sendTask = new Task(() =>
                {
                    while (client.Connected)
                    {
                        ShowBoard(game.Board.Grid);
                        Console.WriteLine("Twój ruch...");

                        var space = int.Parse(Console.ReadLine());

                        game.MarkSpace(Mark.X, space);

                        var ms = new MemoryStream();
                        var bf = new BinaryFormatter();

                        bf.Serialize(ms, game.Board.Grid);

                        stream.Write(ms.GetBuffer(), 0, (int)ms.Length);
                     
                        Console.WriteLine($"Czekam na ruch {opponentNick}");

                        var bufer = new byte[1];
                        stream.Read(bufer, 0, 1);

                        var space1 = Encoding.ASCII.GetString(bufer);

                        game.MarkSpace(Mark.X, int.Parse(space1));

                    }
                });

            

                sendTask.Start();
               

                while (true)
                {

                }

                client.Close();





            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {

                server.Stop();
            }


            
            Console.Read();
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
    }
}
