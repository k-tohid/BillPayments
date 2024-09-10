using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.DTOs
{
    public class RegisterDTO
    {
        // username
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "userName must be at least 5 characters long.")]
        public string UserName { get; set; }

        // email
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        public string Email { get; set; }

        // password
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password must be at least 5 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // confirm password
        [Required(ErrorMessage = "Please repeat your password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }
    }
}
