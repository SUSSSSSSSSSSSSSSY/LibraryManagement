using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-9PK656A\SQLEXPRESS;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "George Orwell" },
                new Author { Id = 2, Name = "J.K. Rowling" }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Dystopian" },
                new Genre { Id = 2, Name = "Fantasy" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "1984", AuthorId = 1, GenreId = 1, Price = 15.99m },
                new Book { Id = 2, Title = "Animal Farm", AuthorId = 1, GenreId = 1, Price = 10.99m },
                new Book { Id = 3, Title = "Harry Potter", AuthorId = 2, GenreId = 2, Price = 25.99m }
            );
        }
    }
}

