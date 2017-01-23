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
    using System.ComponentModel;

    class Program
    {
        internal static Game game { get; private set; }

        internal static TcpServerCommands TcpCommand { get; set; }

        public static void Main()
        {
            try
            {
                Console.WriteLine("Podaj swoj nick i naciśnij ENTER ");

                var serverPlayer = new Player
                {
                    Name = Console.ReadLine(),
                    Mark = Mark.O
                };

                Console.WriteLine("Oczekiwanie na przeciwnika...");

                TcpCommand = new TcpServerCommands();

                var oponnentPlayer = GetOpponentPlayer();

                Console.WriteLine("Przeciwnik podłączony {0}", oponnentPlayer.Name);

                TcpCommand.SendNickToClient(serverPlayer.Name);

                game = new Game(oponnentPlayer, serverPlayer);

                Console.WriteLine("Rozpoczęcie gry...");

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
            finally
            {
                TcpCommand.Server.Stop();
            }

            Console.ReadLine();
        }

        private static Player GetOpponentPlayer()
        {
            return new Player
            {
                Name = TcpCommand.GetOpponentPlayer(),
                Mark = Mark.X
            };
        }

        private static void Play()
        {
            while (TcpCommand.Client.Connected)
            {
                Helper.DrawBoard(game.Board.Grid);

                Console.WriteLine("Twój ruch...");

                var move = Helper.GetMoveFromUser();

                game.MarkSpace(Mark.O, move);

                Helper.DrawBoard(game.Board.Grid);

                if (CheckWin() != Mark.Empty)
                {
                    Console.WriteLine("Koniec gry");
                    break;
                }

                TcpCommand.SendBoardToClient(game.Board.Grid, (int)Mark.Empty);

                Console.WriteLine("Czekam na ruch od: {0}", game.PlayerX.Name);

                var moveFromClient = TcpCommand.GetMoveFromClient();

                game.MarkSpace(Mark.X, moveFromClient);

                if (CheckWin() != Mark.Empty)
                {
                    Console.WriteLine("Koniec gry");
                    break;
                }
            }

            TcpCommand.Client.Close();
        }


        private static Mark CheckWin()
        {
            var winResult = game.CheckWin();

            switch (winResult)
            {
                case Mark.O:
                case Mark.X:
                    Console.WriteLine("Wygrał {0}", winResult);
                    TcpCommand.SendBoardToClient(game.Board.Grid, (int) winResult);
                    return winResult;
                case Mark.None:
                    Console.WriteLine("Remis");
                    TcpCommand.SendBoardToClient(game.Board.Grid, (int) winResult);
                    return winResult;
                default:
                    return Mark.Empty;

            }
        }
    }
}
