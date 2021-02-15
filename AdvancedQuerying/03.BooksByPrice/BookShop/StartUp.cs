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
                var result = GetBooksByPrice(db);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
               .Where(b => b.Price > 40)
               .OrderByDescending(b => b.Price)
               .Select(b => new { b.Title, b.Price })
               .ToList();

            var result = string.Join(Environment.NewLine, books.Select(b => $"{b.Title} - ${b.Price:F2}"));

            return result;
        }
    }
}
