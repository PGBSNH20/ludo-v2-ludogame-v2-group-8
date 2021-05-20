using LudoGameApi.Models;
using LudoGameApi.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Data
{
    public class LudoGameContext : DbContext
    {
        public LudoGameContext(DbContextOptions<LudoGameContext> options) : base(options)
        {

        }

        public virtual DbSet<GameSession> SessionName { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<GamePiece> Pieces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Parking>()
            //    .Property(x => x.StartTime)
            //    .HasDefaultValueSql("getdate()");
        }

    }
}
