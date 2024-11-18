using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using JM.AuthServer.API.Models;
using JM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace JM.Application.Common.Generic
{
    public interface IDbContextCore
    {
        public DbContext Instance { get; }
     
        DbSet<Bank> CoreBank { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
      

        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
