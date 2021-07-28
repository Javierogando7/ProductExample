using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProductExample.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base()
        {
            Database.SetInitializer<ProductContext>(new DropCreateDatabaseAlways<ProductContext>());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ColorPrice> Colors { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasKey(p => p.Id)
                .HasMany<ColorPrice>(p => p.ColorPrices)
                .WithRequired().HasForeignKey(c => c.ProductId);
        }
    }
}