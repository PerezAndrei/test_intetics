using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ims.Models
{
    public class LogInVM
    {
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Something's wrong with your email address. Please try again.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}