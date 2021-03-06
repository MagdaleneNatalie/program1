﻿
namespace TicTacToeClient
{
    using System;
    using System.Net.Sockets;
    using TicTacToeGame;

    class Program
    {
        internal static TcpClientCommands TcpCommands { get; set; } 

        private static string _opponentName;

        static void Main(string[] args)
        {
            TcpCommands = new TcpClientCommands("127.0.0.1", 13000);

            try
            {
                Console.WriteLine("Podaj swoje nick i naciśnij ENTER ");

                TcpCommands.SendNickToServer(Console.ReadLine());
                
                _opponentName = TcpCommands.GetOpponentName();

                Console.WriteLine("Grasz z {0}", _opponentName);

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

                if (board[9] == (int)Mark.None)
                {
                    Console.WriteLine("Remis");
                    break;
                }

                if (board[9] == (int)Mark.X || board[9] == (int)Mark.O)
                {
                    Console.WriteLine("Wygrał {0}", Enum.GetName(typeof(Mark), board[9]));
                    break;
                }

                Console.WriteLine("Twój ruch...");

                var move = Helper.GetMoveFromUser();

                board[move] = (int)Mark.X;

                Helper.DrawBoard(board);

                Console.WriteLine("Czekam na ruch od: {0}", _opponentName);

                TcpCommands.SendMoveToServer(move);
            }

            Console.WriteLine("Koniec gry");

            Console.ReadKey();
        }
    }
}
