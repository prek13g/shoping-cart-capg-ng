using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Password_Hash.services;
using Shoping_cart.Models;

namespace Shoping_cart.DatabaseContext
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
      public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
       public DbSet<Cart> Carts { get; set; }
       public DbSet<Order> Orders { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Password_Hasher hasher = new Password_Hasher();
            string password = hasher.HashPassword("@Aa12345678");

            //Seed data into Admin
            modelBuilder.Entity<Admin>().HasData(new Admin() { AdminId = 123, AdminName = "Anu", AdminEmail = "Anu@gmail.com", AdminPassword = password, Role = "Admin" });

            // Configure Product-Category relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Category_id)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes

            // Configure Cart-User relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.User_id)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes

            // Configure Cart-Product relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.Product_id)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes

            // Configure Order-User relationship
           modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.User_id)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes

            // Configure Order-Product relationship
           modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany() // No navigation property on Product
                .HasForeignKey(o => o.Product_id)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes

            // Configure Order-Cart relationship
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Cart)
            //    .WithMany(c => c.Orders)
            //    .HasForeignKey(o => o.Cart_id)
            //    .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes
        }
    }
}

