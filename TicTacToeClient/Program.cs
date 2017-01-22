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
        internal static BinaryFormatter binaryFormatter = new BinaryFormatter();

        internal static TcpClient client = new TcpClient("127.0.0.1", 13000);

        internal static NetworkStream stream = client.GetStream();

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Podaj swoje nick i naciśnij ENTER ");

                var nickByte = Encoding.ASCII.GetBytes(Console.ReadLine());

                stream.Write(nickByte, 0, nickByte.Length);

                var bufer = new byte[20];
                stream.Read(bufer, 0, 20);
                var s1 = Encoding.ASCII.GetString(bufer);
                Console.WriteLine($"Grasz z {s1}");

                var gameTask = new Task(GameTask);

                gameTask.Start();
             
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.ReadKey();

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

        private static void GameTask()
        {
            while (client.Connected)
            {
                var boardGridByteArray = new byte[64];

                stream.Read(boardGridByteArray, 0, boardGridByteArray.Length);

                var ms = new MemoryStream(boardGridByteArray);

                var obj = binaryFormatter.Deserialize(ms);

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

            }

            stream.Close();
            client.Close();

        }
    
    }
}
