using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Models
{
    [Table("core_Role")]

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}
