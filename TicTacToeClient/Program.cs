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

        internal static TcpCommands TcpCommands { get; set; } 

        internal static string opponentName;

        static void Main(string[] args)
        {
            TcpCommands = new TcpCommands();

            try
            {
                Console.WriteLine("Podaj swoje nick i naciśnij ENTER ");

                TcpCommands.SendNickToServer(Console.ReadLine());
                
                opponentName = TcpCommands.GetOpponentName();

                Console.WriteLine("Grasz z {0}", opponentName);

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

            Console.ReadKey();

        }

        private static void Play()
        {
            while (TcpCommands.Client.Connected)
            {
                var board = TcpCommands.GetBoardFormServer();

                Helper.DrawBoard(board);

                if (board[9] != 0)
                {
                    Console.WriteLine("Wygrał {0}", Enum.GetName(typeof(Mark), board[9]));
                    break;
                }

                Console.WriteLine("Twój ruch...");

                var move = Helper.GetMoveFromUser();

                board[move] = (int)Mark.X;

                Helper.DrawBoard(board);

                Console.WriteLine("Czekam na ruch od: {0}", opponentName);

                TcpCommands.SendMoveToServer(move);
            }

            Console.ReadKey();
        }
    }
}
