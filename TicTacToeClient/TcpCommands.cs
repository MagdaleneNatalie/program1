using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeClient
{
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;

    internal class TcpCommands
    {
        public TcpClient Client { get; }

        internal NetworkStream Stream { get; }

        internal BinaryFormatter BinaryFormatter { get; }

        public TcpCommands()
        {
            this.BinaryFormatter =  new BinaryFormatter();

            Client = new TcpClient("127.0.0.1", 13000);

            Stream = Client.GetStream();
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
