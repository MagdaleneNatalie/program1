using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame;

namespace TicTacToeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player
            {
                Name = "Przemek"
            };

            BinaryFormatter formatter = new BinaryFormatter();

            var ms = new MemoryStream();

            formatter.Serialize(ms, player);

            try
            {
                Int32 port = 13000;
                TcpClient client = new TcpClient("127.0.0.1", port);

                NetworkStream stream = client.GetStream();

                Console.WriteLine("Podaj swoje nick i naciśnij ENTER ");
                var nick = Console.ReadLine();
                var nickByte = Encoding.ASCII.GetBytes(nick);
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


















                //data = new Byte[256];

                //String responseData = String.Empty;

                //Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }
    }
}
