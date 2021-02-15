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
                var result = GetBooksNotReleasedIn(db, 2000);
                Console.WriteLine(result);
            }
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
              .Where(b => b.ReleaseDate.Value.Year != year)
              .OrderBy(b => b.BookId)
              .Select(b => b.Title)
              .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }
    }
        
}
