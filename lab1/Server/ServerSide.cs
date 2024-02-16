using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
     public class ServerSide
     {
          private Socket serverSocket;
          private List<Socket> connectedClients;

          public ServerSide()
          {
               this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               this.connectedClients = new List<Socket>();
               StartListening();
          }

          public void StartListening()
          {
               serverSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000));
               serverSocket.Listen(10);

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
                         Socket clientSocket = serverSocket.Accept();
                         connectedClients.Add(clientSocket);
                         var clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
                         string clientInfo = $"Connected! Client IP: {clientEndPoint.Address}, Port: {clientEndPoint.Port}";
                         Console.WriteLine(clientInfo);

                         Thread clientThread = new Thread(() => WorkWithClient(clientSocket));
                         clientThread.Start();
                    }
               }
               catch (SocketException ex)
               {
                    Console.WriteLine($"Error: {ex.Message}");
                    serverSocket.Close();
               }
          }

          public void BroadcastMessage(string message, Socket senderSocket)
          {
               lock (connectedClients)
               {
                    foreach (var clientSocket in connectedClients)
                    {
                         if (clientSocket != senderSocket)
                         {
                              try
                              {
                                   byte[] buffer = Encoding.ASCII.GetBytes(message);
                                   clientSocket.Send(buffer);
                              }
                              catch (Exception e)
                              {
                                   Console.WriteLine($"Error broadcasting message to {clientSocket.RemoteEndPoint}: {e.Message}");
                              }
                         }
                    }
               }
          }

          public void WorkWithClient(Socket clientSocket)
          {
               Console.WriteLine($"Running : Thread ID: {Thread.CurrentThread.ManagedThreadId}");
               try
               {
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = clientSocket.Receive(buffer)) > 0)
                    {
                         string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                         if (string.IsNullOrEmpty(receivedData))
                         {
                              Console.WriteLine("Received an empty message. Not processing.");
                              continue; // Skip further processing for empty messages
                         }

                         ConsoleSection(clientSocket, receivedData);
                         BroadcastMessage($"Client {clientSocket.RemoteEndPoint}: {receivedData}", clientSocket);
                    }
               }
               catch (Exception e)
               {
                    Console.WriteLine($"Exception: {e.Message}");

                    if (e is SocketException se && se.SocketErrorCode == SocketError.ConnectionReset)
                    {
                         lock (connectedClients)
                         {
                              connectedClients.RemoveAll(socket => !socket.Connected);
                         }
                    }
               }
               finally
               {
                    //clientSocket.Close();
               }
          }

          private void ConsoleSection(Socket senderSocket, string receivedData)
          {
               Console.Clear();

               int sectionWidth = Console.WindowWidth / connectedClients.Count;
               int senderSection = connectedClients.IndexOf(senderSocket);

               if (senderSection >= 0 && senderSection < connectedClients.Count)
               {
                    var senderEndPoint = (IPEndPoint)senderSocket.RemoteEndPoint;
                    string senderInfo = $"Client {senderSection + 1}: IP: {senderEndPoint.Address}, Port: {senderEndPoint.Port}";

                    int horizontalStart = senderSection * sectionWidth;
                    int verticalStart = 1;

                    Console.SetCursorPosition(horizontalStart, verticalStart);
                    Console.Write(senderInfo);
                    Console.SetCursorPosition(horizontalStart, verticalStart + 1);
                    Console.Write($"Received from {senderSocket.RemoteEndPoint}: {receivedData}");
               }
               else
               {
                    Console.WriteLine("Sender client not found in the connected clients list.");
               }
          }
     }
}
