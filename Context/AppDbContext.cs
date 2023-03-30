using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.ViewModel.Get;

namespace ShoppingCart.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductOwner> ProductOwner { get; set; }
        public DbSet<CartViewModel> Cart { get; set; }
    
       
        public DbSet<ProductViewModel> ProductViewModel { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductViewModel>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(10, 2);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
           
            modelBuilder.Entity<ProductOwner>().ToTable("ProductOwner");
            modelBuilder.Entity<CartViewModel>().ToTable("Cart");
            modelBuilder.Entity<ProductViewModel>().Property(p => p.Price).HasPrecision(10, 2);
            modelBuilder.Entity<ProductViewModel>().ToTable("ProductViewModel");
            modelBuilder.Entity<ProductViewModel>().Property<bool>("IsDeleted");
            modelBuilder.Entity<ProductViewModel>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);
        }

}
}