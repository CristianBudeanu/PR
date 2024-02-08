using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
     public class ServerSide
     {
          private TcpListener server;
          private List<TcpClient> connectedClients;

          public ServerSide()
          {
               this.server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9000);
               this.connectedClients = new List<TcpClient>();
               StartListening(); 
          }

          public void StartListening()
          {
               server.Start();

               Thread listenerThread = new Thread(Listener);
               listenerThread.Start();
          }

          public void Listener()
          {
               try
               {
                    while (true)
                    {
                         Console.WriteLine("Waiting for a connection...");
                         TcpClient client = server.AcceptTcpClient();
                         connectedClients.Add(client);
                         var clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                         string clientInfo = $"Connected! Client IP: {clientEndPoint.Address}, Port: {clientEndPoint.Port}";
                         Console.WriteLine(clientInfo);
                         
                         Thread clientThread = new Thread(() => WorkWithClient(client));
                         clientThread.Start();

                    }
               }
               catch (SocketException ex)
               {
                    Console.WriteLine($"Error: {ex.Message}");
                    server.Stop();
               }
          }

          public void WorkWithClient(TcpClient client)
          {
               Console.WriteLine($"Running : Thread ID: {Thread.CurrentThread.ManagedThreadId}");
               try
               {
                    using (var stream = client.GetStream())
                    {
                         byte[] buffer = new byte[1024];
                         int bytesRead;

                         while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) >= 0)
                         {
                              string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                              if (string.IsNullOrEmpty(receivedData))
                              {
                                   Console.WriteLine("Received an empty message. Not processing.");
                                   continue; // Skip further processing for empty messages
                              }

                              ConsoleSection(client,receivedData);

                              var clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;

                              byte[] responseBytes = Encoding.ASCII.GetBytes($"Server got your message PORT : {clientEndPoint.Port}");

                              stream.Write(responseBytes, 0, responseBytes.Length);
                              stream.Flush();
                         }
                    }
               }
               catch (Exception e)
               {
                    Console.WriteLine($"Exception: {e.Message}");

                    if (e is IOException || (e is SocketException se && se.SocketErrorCode == SocketError.ConnectionReset))
                    {
                         lock (connectedClients)
                         {
                              connectedClients.RemoveAll(client => !client.Connected);
                         }
                    }
               }
               finally
               {
                    //client.Close();
               }
          }

          private void ConsoleSection(TcpClient senderClient, string receivedData)
          {
               Console.Clear();

               int sectionWidth = Console.WindowWidth / connectedClients.Count;
               int senderSection = connectedClients.IndexOf(senderClient);

               if (senderSection >= 0 && senderSection < connectedClients.Count)
               {
                    var client = connectedClients[senderSection];
                    var clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    string clientInfo = $"Client {senderSection + 1}: IP: {clientEndPoint.Address}, Port: {clientEndPoint.Port}";

                    int horizontalStart = senderSection * sectionWidth;
                    int verticalStart = 1;

                    Console.SetCursorPosition(horizontalStart, verticalStart);
                    Console.Write(clientInfo);
                    Console.SetCursorPosition(horizontalStart, verticalStart + 1);
                    Console.Write($"Received from {client.Client.RemoteEndPoint}: {receivedData}");


               }
               else
               {
                    Console.WriteLine("Sender client not found in the connected clients list.");
               }
          }
     }
}
