/*
 * Author: Domenic Catalano
 * Date: Sunday, December 7, 2020
 * Program Name: BooksForYou (Lab5)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Added
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BooksForYou.Models;

namespace BooksForYou.Data
{
    // Inheriting form IdentityDbContext, AppUser model
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        // Constructor for ApplicationDbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }

    }
}
