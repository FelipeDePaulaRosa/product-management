﻿using Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Configurations.Products;

namespace Infrastructure.Contexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}