using System.Net;
using DnsClient;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LDNS
{
     public class DnsServices
     {

          private static ILookupClient _lookupClient = new LookupClient();

          public static void FindIpFromDomain(string domainName)
          {
               try
               {
                    var result = _lookupClient.Query(domainName, QueryType.A);
                    var addressRecords = result.Answers.ARecords().Select(ptr => ptr.Address).ToList();
                    if (addressRecords.Any())
                    {
                         foreach (var record in result.Answers.ARecords())
                         {
                              Console.WriteLine($"IP: {record.Address}");
                         }
                    }
                    else
                    {
                         Console.WriteLine("Invalid Domain");
                    }
               }
               catch (Exception e)
               {
                    Console.WriteLine($"Err: {e.Message}");
               }
          }

          public static void FindDomainFromIp(string ipAddress)
          {
               try
               {
                    var result = _lookupClient.QueryReverse(IPAddress.Parse(ipAddress));
                    var domains = result.Answers.PtrRecords().Select(ptr => ptr.PtrDomainName.Value).ToList();

                    if (domains.Any())
                    {
                         foreach (var domainName in domains)
                         {
                              Console.WriteLine($"Domain : {domainName}");
                         }
                    }
                    else
                    {
                         Console.WriteLine("Invalid domain.");
                    }
               }
               catch (Exception e)
               {
                    Console.WriteLine($"Exception : {e}");
               }
          }

          public static string ChangeDnsServers(IPAddress dnsServer)
          {
               if (CheckDNS(dnsServer))
               {
                    var options = new LookupClientOptions(dnsServer);
                    _lookupClient = new LookupClient(options);
                    DisplayDnsServer();
                    return "Serverul DNS a fost schimbat cu succes.";
               }

               DisplayDnsServer();
               return "Nu s-a putut schimba serverul DNS. Verificați disponibilitatea serverului.";
          }

          private static bool IsDnsServer(IPAddress ipAddress)
          {
               try
               {
                    // Query a known domain (e.g., "example.com") against the DNS server
                    IPAddress[] resolvedAddresses = Dns.GetHostAddresses("dns.google", ipAddress.ToString());

                    // If we received a response and it's not empty, assume it's a DNS server
                    return resolvedAddresses != null && resolvedAddresses.Length > 0;
               }
               catch (Exception)
               {
                    return false;
               }
          }


          public static void DisplayDnsServer()
          {

               var nameServer = _lookupClient.NameServers;

               foreach (var DNSServers in _lookupClient.NameServers)
               {
                    Console.WriteLine($"DNS Server IP: {DNSServers.Address}");
               }
               
          }
     }
}
