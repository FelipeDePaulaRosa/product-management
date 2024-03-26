using Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Code)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR ProductSequence");
            
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(x => x.IsActive)
                .IsRequired();
            
            builder.Property(x => x.ManufactureDate)
                .IsRequired();
            
            builder.Property(x => x.DueDate)
                .IsRequired();
            
            builder.OwnsOne(x => x.Supplier, supplier =>
            {
                supplier.Property(x => x.Code)
                    .IsRequired()
                    .HasMaxLength(10);
                
                supplier.Property(x => x.Description)
                    .IsRequired()
                    .HasMaxLength(100);
                
                supplier.Property(x => x.Cnpj)
                    .IsRequired()
                    .HasMaxLength(14);
            });
        }
    }
}