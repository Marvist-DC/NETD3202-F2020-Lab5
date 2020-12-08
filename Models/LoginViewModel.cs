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
using System.ComponentModel.DataAnnotations;

namespace BooksForYou.Models
{
    // This class will handle login functionality
    // It requires email and password to be inputted
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
