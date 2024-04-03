using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.Requests
{
     public class GetMethods
     {
          public static void UniversalGetMethod(string url) //GetCategories, GetCategoryById, GetProductsByCategoryId, GetCategoryIdByName
          {
               using (HttpClient httpClient = new HttpClient())
               {
                    try
                    {
                         var response = httpClient.GetAsync(url).Result;

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
