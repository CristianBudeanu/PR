using System;
using System.Net;
using LDNS;

class Program
{
     static void Main(string[] args)
     {
          int command = -1;

          while (command != 9)
          {
               Console.Clear();
               Console.WriteLine("".PadLeft(50, '='));
               Console.WriteLine("".PadLeft(21) + "DNS Utility Menu" + "".PadRight(21));
               Console.WriteLine("".PadLeft(50, '='));
               Console.WriteLine("1. Find IP by DNS");
               Console.WriteLine("2. Find DNS by IP");
               Console.WriteLine("3. Use different DNS than system");
               Console.WriteLine("4. Display current DNS server");
               Console.WriteLine("9. Exit");
               Console.WriteLine("".PadLeft(50, '='));
               Console.WriteLine("Command: ");
               command = int.Parse(Console.ReadLine());
               Menu(command);
          }
     }

     private static void Menu(int command)
     {
          switch (command)
          {
               case 1:
                    Console.Clear();
                    Console.WriteLine("".PadLeft(50, '='));
                    Console.WriteLine("".PadLeft(19) + "Find IP by DNS" + "".PadRight(19));
                    Console.WriteLine("".PadLeft(50, '='));
                    Console.WriteLine("Enter DNS: ");
                    var dns = Console.ReadLine();
                    DnsServices.FindIpFromDomain(dns);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
               case 2:
                    Console.Clear();
                    Console.WriteLine("".PadLeft(50, '='));
                    Console.WriteLine("".PadLeft(18) + "Find DNS by IP" + "".PadRight(18));
                    Console.WriteLine("".PadLeft(50, '='));
                    Console.WriteLine("Enter IPAddress: ");
                    var ipAddress = Console.ReadLine();
                    DnsServices.FindDomainFromIp(ipAddress);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
               case 3:
                    Console.Clear();
                    Console.WriteLine("".PadLeft(50, '='));
                    Console.WriteLine("".PadLeft(22) + "Change DNS Server" + "".PadRight(22));
                    Console.WriteLine("".PadLeft(50, '='));
                    Console.WriteLine("Enter new DNS server: ");
                    var dnsServer = Console.ReadLine();
                    try
                    {
                         DnsServices.ChangeDnsServers(IPAddress.Parse(dnsServer));
                    }
                    catch (Exception e)
                    {
                         Console.WriteLine("Invalid DNS Server.");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
               case 4:
                    Console.Clear();
                    Console.WriteLine("".PadLeft(50, '='));
                    Console.WriteLine("".PadLeft(20) + "Current DNS Server" + "".PadRight(20));
                    Console.WriteLine("".PadLeft(50, '='));
                    DnsServices.DisplayDnsServer();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
               case 9:
                    Environment.Exit(0);
                    break;
               default:
                    Console.WriteLine("Invalid command.");
                    break;
          }
     }
}
