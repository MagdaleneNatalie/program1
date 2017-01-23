

namespace TicTacToeServer
{
    using System.Text;

    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;

    internal class TcpServerCommands
    {
        public TcpClient Client { get; private set; }

        internal NetworkStream Stream { get; private set; }

        internal BinaryFormatter BinaryFormatter { get; private set; }

        internal TcpListener Server { get; private set; }

        public TcpServerCommands()
        {
            this.BinaryFormatter =  new BinaryFormatter();

            Server = new TcpListener(IPAddress.Parse("127.0.0.1"), 13000);

            Server.Start();

            Client = Server.AcceptTcpClient();

            Stream = Client.GetStream();
        }

        public void SendNickToClient(string name)
        {
            var nick = Encoding.ASCII.GetBytes(name);

            Stream.Write(nick, 0, nick.Length);
        }

        public string GetOpponentPlayer()
        {
            var bufferNick = new byte[20];

            Stream.Read(bufferNick, 0, 20);

            return Encoding.ASCII.GetString(bufferNick);
        }

        public void SendBoardToClient(int[] board, int winMark)
        {
            var msg = new int[10];

            board.CopyTo(msg, 0);

            msg[9] = winMark;

            var ms = new MemoryStream();

            BinaryFormatter.Serialize(ms, msg);

            Stream.Write(ms.GetBuffer(), 0, (int)ms.Length);
        }

        public int GetMoveFromClient()
        {
            var bufer = new byte[1];

            Stream.Read(bufer, 0, 1);

            return int.Parse(Encoding.ASCII.GetString(bufer));
        } 


    }
}
