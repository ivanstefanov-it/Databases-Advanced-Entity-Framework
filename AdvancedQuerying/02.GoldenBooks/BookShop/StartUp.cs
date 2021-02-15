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
                var result = GetGoldenBooks(db);
                Console.WriteLine(result);
            }
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = Enum.Parse<EditionType>("gold", true);
            var books = context.Books
                .Where(a => a.EditionType == goldenBooks && a.Copies <= 5000)
                .OrderBy(x => x.BookId)
                .Select(t => t.Title)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }
    }
}
