using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;


// Pulled directly from https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener?view=net-5.0, may be used for something

class MyTCPListener
{
   public static void Main()
    {
        TcpListener server = null;
        try
        {
            // Set the TcpListener on port 13000
            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(localAddr, port);

            server.Start();

            Byte[] bytes = new byte[256];
            String data = null;

            while (true)
            {
                Console.Write("Waiting for a connection... ");

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                NetworkStream stream = client.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }

                client.Close();
            }
        }

        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            server.Stop();
        }

        Console.WriteLine("\nHit enter to continue... ");
        Console.Read();
    }
}
