using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Start;

namespace Multiply
{
    class Program
    {
        static Expression Multiply(Expression ex)
        {
            int numerator = ex.Ex1.Numerator * ex.Ex2.Numerator;
            int denominator = ex.Ex1.Denominator * ex.Ex2.Denominator;
            return new Expression(numerator, denominator);
        }

        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, 8003));

            while (true)
            {
                socket.Listen(100);
                socket.Accept();
                Stream stream = new NetworkStream(socket);
                DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(Expression));
                Expression received = (Expression)formatter.ReadObject(stream);
                Expression send = Multiply(received);
                stream.Close();
            }
        }
    }
}
