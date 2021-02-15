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
                var result = GetAuthorNamesEndingIn(db, "e");
                Console.WriteLine(result);
            }
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
             .Where(a => a.FirstName.EndsWith(input))
             .Select(a => $"{a.FirstName} {a.LastName}")
             .OrderBy(a => a)
             .ToList();

            var result = string.Join(Environment.NewLine, authors);

            return result;
        }
    }
}
