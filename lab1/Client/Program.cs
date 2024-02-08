using System.Net.Sockets;
using System.Net;
using System.Text;



class Program
{
     static void Main()
     {

          IPAddress serverIp = IPAddress.Parse("127.0.0.1");
          int serverPort = 9000;

          using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
          {
               client.Connect(new IPEndPoint(serverIp, serverPort));
               Console.WriteLine("Connected to the server");

               while (true)
               {
                    Console.WriteLine("Enter text:");
                    string text = Console.ReadLine();

                    if (string.IsNullOrEmpty(text))
                    {
                         text = "CLIENT SENT NULL DATA";
                    }

                    byte[] byteData = Encoding.UTF8.GetBytes(text);
                    client.Send(byteData);

                    string receivedText = "";
                    do
                    {
                         byte[] receivedBuffer = new byte[1024];
                         int bytesRead = client.Receive(receivedBuffer);
                         receivedText += Encoding.UTF8.GetString(receivedBuffer, 0, bytesRead);
                    } while (client.Available > 0);

                    Console.WriteLine($"Server: {receivedText}\n");
               }
          }
     }
}