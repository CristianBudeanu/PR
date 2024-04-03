using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.Requests
{
     public class SharedRequests
     {
          public static async Task checkConn(string url)
          {

               using (HttpClient httpClient = new HttpClient())
               {
                    try
                    {
                         HttpResponseMessage response = await httpClient.GetAsync(url);

                         if (response.IsSuccessStatusCode)
                         {
                              Console.WriteLine($"URL {url} is active and responsive.");
                         }
                         else
                         {
                              Console.WriteLine($"URL {url} returned status code: {response.StatusCode}. The URL might be inactive or not accessible.");
                         }
                    }
                    catch (Exception ex)
                    {
                         Console.WriteLine($"An error occurred while checking the URL {url}: {ex.Message}");
                    }
               }
          }
     }
}
