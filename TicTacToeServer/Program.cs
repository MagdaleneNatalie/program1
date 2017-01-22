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


                var sendTask = new Task(() =>
                {
                    while (client.Connected)
                    {
                        Console.WriteLine("Wyśli wiadomosć: ");
                        var s = Console.ReadLine();
                        var msg = Encoding.ASCII.GetBytes(s);
                        stream.Write(msg, 0, msg.Length);
                    }
                });

                var readTask = new Task(() =>
                {
                    while (client.Connected)
                    {
                        Console.WriteLine("Odebrano widomosć: ");
                        var bufer = new byte[8];
                        stream.Read(bufer, 0, 8);
                        var s1 = Encoding.ASCII.GetString(bufer);
                        Console.WriteLine(s1);
                    }
                });

                sendTask.Start();
                readTask.Start();

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
    }
}
