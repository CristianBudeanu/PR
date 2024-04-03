using System.Data;
using System.Drawing;
using lab4.Requests;

internal class Program
{
     private static void Main(string[] args)
     {
          ShowMenu();
     }

      private static void ShowMenu()
     {
          var command = "-1";

          while (command != "9")
          {
               
               CenterText("Console Menu", ConsoleColor.Yellow);
               CenterText("1. GetCategories", ConsoleColor.White);
               CenterText("2. AddCategory", ConsoleColor.White);
               CenterText("3. GetCategoryById", ConsoleColor.White);
               CenterText("4. DeleteCategory", ConsoleColor.White);
               CenterText("5. GetProductsByCategoryId", ConsoleColor.White);
               CenterText("6. AddProductByCategoryId", ConsoleColor.White);
               CenterText("7. SearchIdByCategoryTitle", ConsoleColor.White);
               CenterText("8. UpdateCategoryNameById", ConsoleColor.White);
               CenterText("9. Exit", ConsoleColor.Red);

               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine("Choose: ");
               Console.ResetColor();
               command = Console.ReadLine();

               string? localCategoryId;
               switch (command)
               {
                    case "1":
                         GetMethods.UniversalGetMethod(ApiUrls.CategoriesUrl());
                         Console.ReadKey();
                         Console.Clear();
                         break;
                    case "2":

                         Console.WriteLine("Enter Title for Category:");
                         string? name = Console.ReadLine();

                         var dataTitle = new
                         {
                              title = name
                         };
                         PostMethods.UniversalPostMethod(ApiUrls.CategoriesUrl(), dataTitle);
                         Console.ReadKey();
                         Console.Clear();
                         break;
                    case "3":
                         Console.WriteLine("Enter Id of Category:");
                         localCategoryId = Console.ReadLine();
                         GetMethods.UniversalGetMethod(ApiUrls.CategoriesByIdUrl(localCategoryId));
                         Console.ReadKey();
                         Console.Clear();
                         break;
                    case "4":
                         Console.WriteLine("Enter Id of Category:");
                         localCategoryId = Console.ReadLine();
                         DeleteMethods.DeleteCategoryById(ApiUrls.CategoriesByIdUrl(localCategoryId));
                         Console.ReadKey();
                         Console.Clear();
                         break;
                    case "5":
                         Console.WriteLine("Enter Id of Category:");
                         localCategoryId = Console.ReadLine();
                         GetMethods.UniversalGetMethod(ApiUrls.ProductsByCategoryIdUrl(localCategoryId));
                         Console.ReadKey();
                         Console.Clear();
                         break;
                    case "6":
                         Console.WriteLine("Enter Id of Category:");
                         localCategoryId = Console.ReadLine();

                         Console.WriteLine("Product : ");
                         Console.WriteLine("Id : ");
                         var productId = Console.ReadLine();
                         Console.WriteLine("Title : ");
                         var productTitle = Console.ReadLine();
                         Console.WriteLine("Price : ");
                         var productPrice = Console.ReadLine();

                         var dataProduct = new
                         {
                            id = productId,
                            title = productTitle,
                            price = productPrice,
                            categoryId = localCategoryId
                         };
                         PostMethods.UniversalPostMethod(ApiUrls.ProductsByCategoryIdUrl(localCategoryId),dataProduct);
                         Console.ReadKey();
                         Console.Clear();
                         break;
                    case "7":
                         Console.WriteLine("Enter Title of Category:");
                         var categoryTitle = Console.ReadLine();
                         GetMethods.UniversalGetMethod(ApiUrls.CategoryIdByNameUrl(categoryTitle));
                         Console.ReadKey();
                         Console.Clear();
                         break;
                    case "8":
                         Console.WriteLine("Enter Id of Category:");
                         localCategoryId = Console.ReadLine();
                         Console.WriteLine("New Title : ");
                         var newCategoryTitle = Console.ReadLine();
                         var catTitle = new
                         {
                              title = newCategoryTitle
                         };
                         PutMethods.UniversalPutMethod(ApiUrls.ChangeTitleByIdUrl(localCategoryId),catTitle);
                         Console.ReadKey();
                         Console.Clear();
                         break;
                    case "9":
                         Environment.Exit(0);
                         break;
               }
          }
     }

     private static void CenterText(string text, ConsoleColor color)
     {
          Console.ForegroundColor = color;
          Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
          Console.WriteLine(text);
          Console.ResetColor();
     }
}
