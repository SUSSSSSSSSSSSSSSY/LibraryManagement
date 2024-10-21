namespace LibraryManagement
{
    using LibraryManagement;
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LibraryContext())
            {

                int genreId = 1;
                int booksInGenre = context.Books.Count(b => b.GenreId == genreId);
                Console.WriteLine($"Books in Genre {genreId}: {booksInGenre}");

                int authorId = 1;
                decimal minPrice = context.Books.Where(b => b.AuthorId == authorId).Min(b => b.Price);
                Console.WriteLine($"Min price for Author {authorId}: {minPrice}");

                decimal avgPrice = context.Books.Where(b => b.GenreId == genreId).Average(b => b.Price);
                Console.WriteLine($"Avg price in Genre {genreId}: {avgPrice}");

                decimal totalCost = context.Books.Where(b => b.AuthorId == authorId).Sum(b => b.Price);
                Console.WriteLine($"Total cost for Author {authorId}: {totalCost}");

                var booksByGenre = context.Books.GroupBy(b => b.Genre.Name);
                foreach (var group in booksByGenre)
                {
                    Console.WriteLine($"Genre: {group.Key}, Books: {group.Count()}");
                }

                var titlesInGenre = context.Books.Where(b => b.GenreId == genreId).Select(b => b.Title).ToList();
                titlesInGenre.ForEach(t => Console.WriteLine(t));

                var otherBooks = context.Books.Except(context.Books.Where(b => b.GenreId == genreId)).ToList();
                otherBooks.ForEach(b => Console.WriteLine(b.Title));

                var combinedBooks = context.Books.Where(b => b.AuthorId == 1).Union(context.Books.Where(b => b.AuthorId == 2)).ToList();
                combinedBooks.ForEach(b => Console.WriteLine(b.Title));

                var top5ExpensiveBooks = context.Books.OrderByDescending(b => b.Price).Take(5).ToList();
                top5ExpensiveBooks.ForEach(b => Console.WriteLine(b.Title));

                var booksSkipTake = context.Books.OrderBy(b => b.Id).Skip(10).Take(5).ToList();
                booksSkipTake.ForEach(b => Console.WriteLine(b.Title));
            }
        }
    }

}
