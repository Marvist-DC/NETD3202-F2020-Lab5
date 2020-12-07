using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BooksForYou.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksForYou.Models
{
    public class RecommendedBooksContext : DbContext
    {
        public RecommendedBooksContext(DbContextOptions<RecommendedBooksContext> options) : base(options)
        {

        }

        // DbSet to create tables
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
            
    }
}
