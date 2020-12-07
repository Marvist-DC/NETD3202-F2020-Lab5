/*
 * Author: Domenic Catalano
 * Date: Sunday, December 6, 2020
 * Program Name: BooksForYou (Lab5)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksForYou.Models
{
    // This class will create an author object
    public class Author
    {
        public int authorId { get; set; }           // Primary Key
        public string firstName { get; set; }
        public string lastName { get; set; }

        // Navigation
        public ICollection<Book> Books { get; set; }
    }
}
