using System;
using System.Diagnostics;

class Program
{
     static void Main()
     {
         // Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
          bool exitRequested = false;
          int count = 0;
          do
          {
               Console.ForegroundColor = ConsoleColor.DarkGreen;
               PrintLinesTopCenter("TCP SERVER <-> CLIENT APP");
               Console.ResetColor();
               PrintLinesInCenter("Choose one:", "1) Start Client", "2) Start Server", "3) Exit", "Enter : ");
               
               string choice = 
                    Console.ReadLine();

               switch (choice)
               {
                    case "1":
                         StartClient();
                         Console.Clear();
                         break;

                    case "2":
                         if (count == 0)
                         {
                           StartServer();
                              count++;
                              Console.Clear();
                         }
                         else
                         {
                              Console.Clear();
                              Console.ForegroundColor = ConsoleColor.Red;
                              PrintLinesInCenter("You can create only one server per port.");
                              Console.ResetColor();
                              Console.ReadKey();
                              Console.Clear();
                         }
                         
                         break;

                    case "3":
                         exitRequested = true;
                         break;

                    default:
                         Console.ForegroundColor = ConsoleColor.Red;
                         PrintLinesInCenter("Invalid choice. Please enter a number between 1 and 3.");
                         Console.ResetColor();
                         Console.ReadKey();
                         Console.Clear();
                         break;
               }

          } while (!exitRequested);
     }

     static void StartClient()
     {
          //Console.WriteLine("Starting Client...");
          // Replace with your actual path to the Client project
          StartProcessInNewConsole("D:\\UTM\\III\\PR\\lab1\\Client\\release\\Client.exe");
     }

     static void StartServer()
     {
          //Console.WriteLine("Starting Server...");
          // Replace with your actual path to the Server project
          StartProcessInNewConsole("D:\\UTM\\III\\PR\\lab1\\Server\\release\\Server.exe");
     }


     static void StartProcessInNewConsole(string filePath)
     {
          ProcessStartInfo startInfo = new ProcessStartInfo
          {
               FileName = "cmd.exe",
               RedirectStandardInput = true,
               UseShellExecute = false,
               CreateNoWindow = true
          };

          Process process = new Process
          {
               StartInfo = startInfo
          };

          process.Start();

          using (StreamWriter sw = process.StandardInput)
          {
               if (sw.BaseStream.CanWrite)
               {
                    sw.WriteLine($"start {filePath}");
                    sw.WriteLine("exit");
               }
          }
     }

     private static void PrintLinesInCenter(params string[] lines)
     {
          int verticalStart = (Console.WindowHeight - lines.Length) / 2;
          int verticalPosition = verticalStart;
          foreach (var line in lines)
          {
               int horizontalStart = (Console.WindowWidth - line.Length) / 2;
               Console.SetCursorPosition(horizontalStart, verticalPosition);
               Console.Write(line);
               ++verticalPosition;
          }
     }

     private static void PrintLinesTopCenter(params string[] lines)
     {
          int verticalStart = (Console.WindowHeight - lines.Length);
          int verticalPosition = 0;
          foreach (var line in lines)
          {
               int horizontalStart = (Console.WindowWidth - line.Length)/2;
               Console.SetCursorPosition(horizontalStart, verticalPosition);
               Console.Write(line);
               ++verticalPosition;
          }
     }
}
