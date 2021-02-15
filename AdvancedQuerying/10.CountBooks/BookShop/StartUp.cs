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
                var result = CountBooks(db, 12);
                Console.WriteLine(result);
            }
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
            .Where(b => b.Title.Length > lengthCheck)
            .ToList();

            var result = books.Count();

            return result;
        }
    }
}
