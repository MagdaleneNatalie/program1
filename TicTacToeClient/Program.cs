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




            try
            {
                Int32 port = 13000;
                TcpClient client = new TcpClient("127.0.0.1", port);

                NetworkStream stream = client.GetStream();

                Console.WriteLine("Podaj swoje nick i naciśnij ENTER ");
                var nick = Console.ReadLine();
                var nickByte = Encoding.ASCII.GetBytes(nick);
                stream.Write(nickByte, 0, nickByte.Length);

                var bufer = new byte[20];
                stream.Read(bufer, 0, 20);
                var s1 = Encoding.ASCII.GetString(bufer);
                Console.WriteLine($"Grasz z {s1}");


                var sendTask = new Task(() =>
                {
                    while (client.Connected)
                    {
                        //var ms = new MemoryStream(64);

                        //stream.Read(ms.GetBuffer(), 0, 64);

                        //var bf = new BinaryFormatter();

                        //var board = (int[])bf.Deserialize(ms);

                        //ShowBoard(board);

                        var bytea = new byte[64];

                        stream.Read(bytea, 0, 64);

                        var ms = new MemoryStream(bytea);

                        var bf = new BinaryFormatter();
                        
                        var obj = bf.Deserialize(ms);

                        if (obj is int[])
                        {
                            ShowBoard((int[])obj);
                        }

                        if (obj is Mark)
                        {
                            Console.WriteLine($"Wygrał {(Mark)obj}");
                            Console.ReadKey();
                            break;
                        }


                        Console.WriteLine("Twój ruch...");

                        var spaceByte = Encoding.ASCII.GetBytes(Console.ReadLine());

                        stream.Write(spaceByte, 0, spaceByte.Length);

                    } });


               

                sendTask.Start();
               

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
        private static void ShowBoard(int[] grid)
        {
            Console.WriteLine("-------");
            for (int i = 0; i < grid.Length; i += 3)
            {
                Console.WriteLine($"|{grid[i]}|{grid[i + 1]}|{grid[i + 2]}|");
            }
            Console.WriteLine("-------");
        }
    }
}
