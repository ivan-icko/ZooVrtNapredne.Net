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
        public DbSet<User> Users { get; set; }
        public DbSet<Package>Packages { get; set; }
        public DbSet<PU> PU { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZooNapredneNet;Integrated Security=True;");*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Animal>().ToTable("Animals");

            modelBuilder.Entity<Package>().HasMany(p => p.Animals).WithMany(a => a.Packages);



            //modelBuilder.Entity<Package>().HasMany(p => p.Users).WithMany(u => u.Packages);


            /* modelBuilder.Entity<PackageUser>().HasMany(p => p.Packages).WithOne(p => p.PackageUser);

             modelBuilder.Entity<PackageUser>().HasMany(p => p.Users).WithOne(p => p.PackageUser);*/
            /*
                        modelBuilder.Entity<PackageUser>().ToTable("PackUser");

                        modelBuilder.Entity<User>().HasOne(p => p.PackageUser).WithMany().HasForeignKey(u => u.Id);

                        modelBuilder.Entity<Package>().HasOne(p => p.PackageUser).WithMany().HasForeignKey(p => p.PackageId);*/

            modelBuilder.Entity<PU>().ToTable("PU");
            modelBuilder.Entity<PU>().HasKey(p => new { p.PackageId, p.UserId, p.TimeOfReservation });
            modelBuilder.Entity<PU>().HasOne(p => p.Package).WithMany(p => p.PUs).HasForeignKey(p => p.PackageId);
            modelBuilder.Entity<PU>().HasOne(p => p.User).WithMany(p => p.PUs).HasForeignKey(p => p.UserId);

            /* modelBuilder.Entity<User>().HasOne(p => p.PackageUser).WithMany().HasForeignKey(u => u.Id);*/





        }
    }
}
