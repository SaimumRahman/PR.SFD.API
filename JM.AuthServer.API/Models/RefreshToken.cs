using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Models
{
    [Table("core_RefreshToken")]

    public class RefreshToken
    {
      //  public Guid Id { get; set; }
      //  public string Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
      //  public Guid UserId { get; set; }
    }
}
