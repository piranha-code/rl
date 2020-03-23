using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Entities;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");
            
            builder.Property(m => m.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(m => m.QuantityPerUnit)
                .HasMaxLength(20);

            // configures key
            builder.HasKey(m => m.ProductId)
                .HasName("PK_Products");

            // configures one-to-many relationship
            builder.HasOne(m => m.Category)
                .WithMany(m => m.Products)
                .OnDelete(DeleteBehavior.NoAction);             
        }
    }
}