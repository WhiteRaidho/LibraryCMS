using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class ApplicationDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrow>()
                .HasOne(x => x.User)
                .WithMany(x => x.Borrows)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Borrow>()
                .HasOne(x => x.Librarian)
                .WithMany(x => x.BorrowedBooks)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Borrow>()
                .HasOne(x => x.ReturnLibrarian)
                .WithMany(x => x.ReturnedBooks)
                .OnDelete(DeleteBehavior.SetNull);
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
