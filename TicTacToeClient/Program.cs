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

        internal static string opponentName;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Podaj swoje nick i naciśnij ENTER ");

                var nickByte = Encoding.ASCII.GetBytes(Console.ReadLine());

                stream.Write(nickByte, 0, nickByte.Length);

                var bufer = new byte[20];
                stream.Read(bufer, 0, 20);
                opponentName = Encoding.ASCII.GetString(bufer);
                Console.WriteLine($"Grasz z {opponentName}");

                var gameTask = new Task(GameTask);

                gameTask.Start();

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();

        }
        private static void ShowBoard(int[] grid)
        {
            Console.Clear();

            for (int i = 0; i < grid.Length - 1; i += 3)
            {
                Console.WriteLine(" -----------");
                Console.WriteLine("| {0} | {1} | {2} |", DrawSign(grid[i]), DrawSign(grid[i + 1]), DrawSign(grid[i + 2]));
            }

            Console.WriteLine(" -----------");

        }

        private static string DrawSign(int i)
        {
            switch (i)
            {
                case 1:
                    return "X";
                case 2:
                    return "O";
                default:
                    return " ";
            }
        }

        private static void GameTask()
        {
            while (client.Connected)
            {
                var boardGridByteArray = new byte[68];

                stream.Read(boardGridByteArray, 0, boardGridByteArray.Length);

                var ms = new MemoryStream(boardGridByteArray);

                var obj = (int[])binaryFormatter.Deserialize(ms);

                ShowBoard(obj);

                if (obj[9] != 0)
                {
                    Console.WriteLine($"Wygrał {Enum.GetName(typeof(Mark), obj[9])}");
                    break;
                }

                Console.WriteLine("Twój ruch...");

                var space = Console.ReadLine();

                var spaceByte = Encoding.ASCII.GetBytes(space);

                var board = obj;

                board[int.Parse(space)] = (int)Mark.X;

                ShowBoard(board);

                Console.WriteLine($"Czekam na ruch od: {opponentName}");

                stream.Write(spaceByte, 0, spaceByte.Length);

            }

            Console.ReadKey();
            stream.Close();
            client.Close();

        }
    
    }
}
