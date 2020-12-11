using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Domain
{
    public class PBLContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Mentor> Mentors { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=PBL;Trusted_Connection = True;");
        }
    }
}
