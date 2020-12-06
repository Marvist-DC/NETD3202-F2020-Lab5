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
    // This class will create a book object
    public class Book
    {
        public int bookId { get; set; }
        public Author author { get; set; }
        public string bookName { get; set; }
        public int yearPublished { get; set; }
    }
}
