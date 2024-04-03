using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.Requests
{
     public class PutMethods
     {
          public static void UniversalPutMethod(string url, object data) // UpdateCategoryTitleById
          {

               var jsonData = JsonConvert.SerializeObject(data);

               using (HttpClient httpClient = new HttpClient())
               {
                    try
                    {
                         var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                         var response = httpClient.PutAsync(url, content).Result;

                         if (response.IsSuccessStatusCode)
                         {
                              string responseBody = response.Content.ReadAsStringAsync().Result;
                              string prettierResponseBody = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);
                              Console.WriteLine($"Response from {url}:");
                              Console.WriteLine(prettierResponseBody);
                         }
                         else
                         {
                              Console.WriteLine($"Failed to retrieve data from {url}. Status code: {response.StatusCode}");
                         }
                    }
                    catch (Exception ex)
                    {
                         Console.WriteLine($"An error occurred: {ex.Message}");
                    }
               }
          }
     }
}
