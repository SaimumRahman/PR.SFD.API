using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Models
{
    [Table("CORE_USERS")]
    public class User : IdentityUser<Guid>
    {
        //public string LoginId { get; set; }
        public int UserId { get; set; }
        public int IsFirstLogin { get; set; }
        //public string UserID { get; set; }
        public int DesignationId { get; set; } = 0;
        public int DepartmentId { get; set; } = 0;
        public int UpdateBy { get; set; }
        public int CreateBy { get; set; }
        public int IsActive { get; set; } = 1;
        public int ThemeId { get; set; } = 0;
        public string LandingPage { get; set; } = "";
        public string FullName { get; set; }
        public string PASSWORDHASH { get; set; }

        //public string PhoneNumber { get; set; }
        //public string Email { get; set; }



        public DateTime LastLoginDate { get; set; }
        public DateTime CreateDate { get; set; }

        public int IsExpired { get; set; }

    }
}
