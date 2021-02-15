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
                var result = GetBooksByAuthor(db, "R");
                Console.WriteLine(result);
            }
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksWithAuthor = context.Books
             .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
             .OrderBy(b => b.BookId)
             .Select(b => new { b.Title, b.Author.FirstName, b.Author.LastName })
             .ToList();

            var result = String.Join(Environment.NewLine, booksWithAuthor.Select(b => $"{b.Title} ({b.FirstName} {b.LastName})"));

            return result;
        }
    }
}
