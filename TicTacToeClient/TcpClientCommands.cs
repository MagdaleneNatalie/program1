
namespace TicTacToeClient
{
    using System;
    using System.Text;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;

    internal class TcpClientCommands
    {
        public TcpClient Client { get; }

        internal NetworkStream Stream { get; }

        internal BinaryFormatter BinaryFormatter { get; }

        public TcpClientCommands(string ip, int port)
        {
            this.BinaryFormatter =  new BinaryFormatter();

            while (true)
            {
                try
                {
                    this.Client = new TcpClient(ip, port);

                    this.Stream = Client.GetStream();

                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Brak serwera gry. Następna próba za 2s");
                    Thread.Sleep(2000);
                }
            }

           
        }

        public void SendNickToServer(string name)
        {
            var nick = Encoding.ASCII.GetBytes(name);

            Stream.Write(nick, 0, nick.Length);
        }

        public string GetOpponentName()
        {
            var bufferNick = new byte[20];

            Stream.Read(bufferNick, 0, 20);

            return Encoding.ASCII.GetString(bufferNick);
        }

        public int[] GetBoardFormServer()
        {
            var boardGridByteArray = new byte[68];

            Stream.Read(boardGridByteArray, 0, boardGridByteArray.Length);

            var ms = new MemoryStream(boardGridByteArray);

            return (int[])BinaryFormatter.Deserialize(ms);
        }


        public void SendMoveToServer(int space)
        {
            var spaceByte = Encoding.ASCII.GetBytes(space.ToString());

            Stream.Write(spaceByte, 0, spaceByte.Length);
        }

    }
}
