/*
 * Author: Domenic Catalano
 * Date: Sunday, December 6, 2020
 * Program Name: BooksForYou (Lab5)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BooksForYou.Models
{
    public class BookContext : DbContext
    {
        // Constructor for BookContext
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

    }
}
