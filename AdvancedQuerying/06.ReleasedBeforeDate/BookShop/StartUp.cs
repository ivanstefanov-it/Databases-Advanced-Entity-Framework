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
                var result = GetBooksReleasedBefore(db, "12-04-1992");
                Console.WriteLine(result);
            }
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime releaseDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var books = context.Books
              .Where(b => b.ReleaseDate < releaseDate)
              .OrderByDescending(b => b.ReleaseDate)
              .Select(b => new { b.Title, b.EditionType, b.Price })
              .ToList();

            var result = string.Join(Environment.NewLine, books.Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}"));

            return result;
        }
    }
}
