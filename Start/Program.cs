using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Start
{
    class Program
    {
        static void Evaluate(Expression ex)
        {
            Expression express=ex;
            //switch (ex.Oper)
            //{
            //    case Operators.Add:
            //        express = Add(ex);
            //        break;
            //    case Operators.Subtract:
            //        express = Subtract(ex);
            //        break;
            //    case Operators.Multiply:
            //        express = Multiply(ex);
            //        break;
            //    case Operators.Divide:
            //        express = Divide(ex);
            //        break;
            //    default:
            //        break;
            //}
            Console.WriteLine(express.Numerator+"/"+express.Denominator);
            Console.ReadKey();
        }
        //static Expression Add(Expression ex)
        //{
        //    int numerator = (ex.Ex1.Numerator*ex.Ex2.Denominator)+(ex.Ex2.Numerator*ex.Ex1.Denominator);
        //    int denominator = ex.Ex1.Denominator * ex.Ex2.Denominator;
        //    return new Expression(numerator, denominator);
        //}
        //static Expression Subtract(Expression ex)
        //{
        //    int numerator = (ex.Ex1.Numerator * ex.Ex2.Denominator) - (ex.Ex2.Numerator * ex.Ex1.Denominator);
        //    int denominator = ex.Ex1.Denominator * ex.Ex2.Denominator;
        //    return new Expression(numerator, denominator);
        //}
        //static Expression Multiply(Expression ex)
        //{
        //    int numerator = ex.Ex1.Numerator * ex.Ex2.Numerator;
        //    int denominator = ex.Ex1.Denominator * ex.Ex2.Denominator;
        //    return new Expression(numerator, denominator);
        //}
        //static Expression Divide(Expression ex)
        //{
        //    int numerator = ex.Ex1.Numerator*ex.Ex2.Denominator;
        //    int denominator = ex.Ex1.Denominator*ex.Ex2.Numerator;
        //    return new Expression(numerator, denominator);
        //}
        static Socket sockAdd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket sockSub = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket sockMult = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket sockDiv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            sockAdd.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001));
            //sockSub.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8002));
            //sockMult.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8003));
            //sockDiv.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8004));

            Expression e1 = new Expression(2, 4);
            Expression e21 = new Expression(1, 3);
            Expression er1 = new Expression(e1, e21, Operators.Multiply);

            Expression e2 = new Expression(2, 4);
            Expression e22 = new Expression(1, 3);
            Expression er2 = new Expression(e2, e22, Operators.Add);

            Expression e3 = new Expression(2, 4);
            Expression e23 = new Expression(1, 3);
            Expression er3 = new Expression(e3, e23, Operators.Subtract);

            Expression e4 = new Expression(2, 4);
            Expression e24 = new Expression(1, 3);
            Expression er4 = new Expression(e4, e24, Operators.Divide);

            Stream streamAdd = new NetworkStream(sockAdd);
            //Stream streamSub = new NetworkStream(sockSub);
            //Stream streamMult = new NetworkStream(sockMult);
            //Stream streamDiv = new NetworkStream(sockDiv);

            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(Expression));
            formatter.WriteObject(streamAdd, er1);
           // streamAdd.Close();
            Stream streamAddReceive = new NetworkStream(sockAdd);
            Expression received = (Expression)formatter.ReadObject(streamAddReceive);
           // streamAddReceive.Close();

            Evaluate(received);
            //Evaluate(er2);
            //Evaluate(er3);
            //Evaluate(er4);

        }
    }
}
