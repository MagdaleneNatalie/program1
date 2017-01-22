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

            var players = new List<Player>();

            try
            {

                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);
                server.Start();

                TcpClient client = server.AcceptTcpClient();

                while (client.Connected)
                {
                    Console.Write("Czekanie na gracza... ");

                   
                    
                    Console.WriteLine("Połącznie");

                    NetworkStream stream = client.GetStream();
                                        
                    BinaryFormatter b = new BinaryFormatter();

                    var player = (Player)b.Deserialize(stream);

                    players.Add(player);

                    Console.WriteLine($"Dodanie gracza {player.Name}");

                    var game = new Game(players[0], new Player { Name = "Magda" });

                    game.MarkSpace(Mark.O, 2);
                    Console.WriteLine("Gramy");

                    var ms = new MemoryStream();

                    Console.WriteLine("Wysyłanie planszy");

                    b.Serialize(ms, game.Board.Grid);

                    stream.Write(ms.GetBuffer(),0,(int)ms.Length);

                    Console.WriteLine("Ruch");
                    Console.ReadKey();


                    game.MarkSpace(Mark.X, 1);
                    Console.WriteLine("Wysyłanie ruchu");

                    b.Serialize(ms, game.Board.Grid);

                    stream.Write(ms.GetBuffer(), 0, (int)ms.Length);

                    Console.ReadKey();

                    client.Close();
                }


               


               
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
    }
}
