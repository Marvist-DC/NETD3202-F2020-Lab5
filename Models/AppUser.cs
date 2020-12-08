/*
 * Author: Domenic Catalano
 * Date: Monday, December 7, 2020
 * Program Name: BooksForYou (Lab5)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Added
using Microsoft.AspNetCore.Identity;

namespace BooksForYou.Models
{
    // This class will represent a user on the system
    // Inherits from IdentityUser
    public class AppUser : IdentityUser
    {

    }
}
