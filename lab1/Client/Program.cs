using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

class Program
{
     private static Socket client;
     private static volatile bool receivingMessages = true;

     static void Main()
     {
          IPAddress serverIp = IPAddress.Parse("127.0.0.1");
          int serverPort = 9000;

          client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          client.Connect(new IPEndPoint(serverIp, serverPort));
          Console.WriteLine("Connected to the server");

          // Start a separate thread to handle receiving messages
          Thread receiveThread = new Thread(ReceiveMessages);
          receiveThread.Start();

          while (true)
          {
               Console.WriteLine("Enter text (type 'exit' to close):");
               string text = Console.ReadLine();

               if (text.ToLower() == "exit")
               {
                    receivingMessages = false; // Set the flag to stop receiving messages
                    break; // Exit the loop and close the application
               }

               byte[] byteData = Encoding.UTF8.GetBytes(text);
               client.Send(byteData);
          }

          // Properly close the socket
          client.Close();
     }

     static void ReceiveMessages()
     {
          while (receivingMessages)
          {
               try
               {
                    byte[] receivedBuffer = new byte[1024];
                    int bytesRead = client.Receive(receivedBuffer);
                    string receivedText = Encoding.UTF8.GetString(receivedBuffer, 0, bytesRead);
                    Console.WriteLine($"Server: {receivedText}\n");
               }
               catch (Exception ex)
               {
                    if (receivingMessages)
                         Console.WriteLine($"Error receiving message: {ex.Message}");
                    break;
               }
          }
     }
}
