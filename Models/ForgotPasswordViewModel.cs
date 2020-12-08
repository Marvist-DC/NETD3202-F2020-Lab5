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
    // This will provide a template for password reset functionality
    public class ForgotPasswordViewModel
    {
        // Email is required
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Password is required and must meet requirements
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        // Password confirmation. This field must match the password field
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
