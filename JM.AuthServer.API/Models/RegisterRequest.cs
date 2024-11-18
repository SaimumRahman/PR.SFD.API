using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Models
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        public string Username { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

      


    }
}
