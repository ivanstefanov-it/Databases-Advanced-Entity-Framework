namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var result = GetBooksByCategory(db, "horror mystery drama");
                Console.WriteLine(result);
            }
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var books = context.Books
             .Where(bc => bc.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
             .Select(t => t.Title)
             .OrderBy(t => t)
             .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }
    }
}
