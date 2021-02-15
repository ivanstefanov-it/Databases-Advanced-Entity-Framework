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
                var result = CountCopiesByAuthor(db);
                Console.WriteLine(result);
            }
        }
        
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BooksCount = x.Books.Sum(c => c.Copies)
                })
                .OrderByDescending(b => b.BooksCount)
                .ToList();


            var result = string.Join(Environment.NewLine, authors.Select(x => $"{x.FirstName} {x.LastName} - {x.BooksCount}"));

            return result;
        }
    }
}
