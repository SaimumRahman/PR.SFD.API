using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using JM.AuthServer.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using JM.Domain.Entities;
using JM.Application.Common.Generic;
using System.Data;

namespace JM.Persistence
{
    public class ApplicationDbContext : DbContext, IDbContextCore
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }
        public DbContext Instance { get => this; }
      
        public virtual DbSet<Bank> CoreBank { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }




        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
