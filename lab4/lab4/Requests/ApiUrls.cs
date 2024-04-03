using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.Requests
{
     public class ApiUrls
     {

          public static string CategoriesUrl() //Get <-> POST(+BODY)
          {
               return "https://localhost:44370/api/Category/categories";
          }

          public static string CategoryIdByNameUrl(string title) //Get
          {
               return $"https://localhost:44370/api/Category/categories/search?categoryName={title}";
          }

          public static string CategoriesByIdUrl(string? id) //GET, DELETE, 
          {
               return $"https://localhost:44370/api/Category/categories/{id}";
          }

          public static string ProductsByCategoryIdUrl(string id) //GET, POST(ID+BODY)
          {
               return $"https://localhost:44370/api/Category/categories/{id}/products";
          }

          public static string ChangeTitleByIdUrl(string id) // PUT(ID+BODY)
          {
               return $"https://localhost:44370/api/Category/{id}";
          } 


     }
}
