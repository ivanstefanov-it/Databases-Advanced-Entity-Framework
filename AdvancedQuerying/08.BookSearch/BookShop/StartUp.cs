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
                var result = GetBookTitlesContaining(db, "sK");
                Console.WriteLine(result);
            }
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
             .Where(a => a.Title.Contains(input.ToLower()))
             .Select(a => a.Title)
             .OrderBy(a => a)
             .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }
    }
}
