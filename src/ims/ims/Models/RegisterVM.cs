using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ims.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Enter UserName")]
        [Display(Name = "Username")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid Username (only a-z or A-Z characters)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Something's wrong with your email address. Please try again.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [RegularExpression(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*[~@#$%^&*()_+]).{6,})$", ErrorMessage = "Invalid Password (at least 6 characters long, at least one lowercase letter, one uppercase letter, one digit, no special symbols eg. ~!@#$%^&*()_+;)")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}