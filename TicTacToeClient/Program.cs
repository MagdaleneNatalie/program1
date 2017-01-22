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

                stream.Write(ms.GetBuffer(), 0, (int)ms.Length);

                Console.WriteLine("Aktywacja użytkownika");
                Console.ReadKey();
                stream.Flush();

                stream.Write(ms.GetBuffer(), 0, (int)ms.Length);

                var ms2 = new MemoryStream();
                Byte[] bytes = new Byte[64];

                //var i = stream.Read(bytes, 0, bytes.Length);

                BinaryFormatter bf = new BinaryFormatter();

                //var ms1 = new MemoryStream();

                //ms1.Write(bytes, 0, bytes.Length);

              
                Console.ReadKey();









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
