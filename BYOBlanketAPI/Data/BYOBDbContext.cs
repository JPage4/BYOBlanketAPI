using BYOBlanketAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYOBlanketAPI.Data
{
    public class BYOBDbContext : DbContext
    {
        public BYOBDbContext(DbContextOptions<BYOBDbContext> options)
        : base(options)
        {
        }
        public DbSet<NapSpace> NapSpaces{ get; set; }
        public DbSet<Reservation> Reservations{ get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
