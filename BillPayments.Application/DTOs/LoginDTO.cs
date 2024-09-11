using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.DTOs
{
    public class LoginDTO
    {
        // username
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "userName must be at least 5 characters long.")]
        public string UserName { get; set; }



        // password
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password must be at least 5 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
