using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Models
{
   public class LoginRequest
    {
        [Required]
        public string LoginId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
