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
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        //public string UserID { get; set; }

        public int DesignationId { get; set; } = 0;
        public int DepartmentId { get; set; } = 0;
        public int UpdateBy { get; set; }
        public int CreateBy { get; set; }
        public int IsActive { get; set; } 
        public int ThemeId { get; set; } = 0;
        public string LandingPage { get; set; } = "";


    }
}
