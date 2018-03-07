using BYOBlanketAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYOBlanketAPI.Data
{
    public class BYOBDbContext : IdentityDbContext
    {
        public BYOBDbContext(DbContextOptions<BYOBDbContext> options)
        : base(options)
        {
        }
        public DbSet<NapSpace> NapSpace{ get; set; }
        public DbSet<Reservation> Reservation{ get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
