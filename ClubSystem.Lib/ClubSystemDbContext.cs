using ClubSystem.Lib.Model.Club;
using ClubSystem.Lib.Model.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib
{
    public class ClubSystemDbContext : DbContext
    {
        public ClubSystemDbContext(DbContextOptions<ClubSystemDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
    }
}
