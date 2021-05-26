using LudoGameV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LudoGameV2.Data
{
    public class LudoWebApplicationContext : IdentityDbContext
    {
        public DbSet<LudoGame> LudoCurrentState { get; set; }
        public LudoWebApplicationContext(DbContextOptions<LudoWebApplicationContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
