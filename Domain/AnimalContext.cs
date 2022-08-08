using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain
{
    public class AnimalContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public AnimalContext([NotNull] DbContextOptions options) : base(options)
        {

        }



        public DbSet<Animal> Animals { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<User> Employees { get; set; }
        public DbSet<Package>Packages { get; set; }
       


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZooNapredneNet;Integrated Security=True;");*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Animal>().ToTable("Animals");

            modelBuilder.Entity<Package>().HasMany(p => p.Animals).WithMany(a => a.Packages);
            modelBuilder.Entity<Package>().HasMany(p => p.Users).WithMany(u => u.Packages);
            

        }
    }
}
