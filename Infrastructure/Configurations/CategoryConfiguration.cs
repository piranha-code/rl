using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Entities;

namespace Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable("Categories");

            // configures key
            builder.HasKey(m => m.CategoryId)
                .HasName("PK_Categories");

            builder.Property(m => m.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            builder.Ignore(m => m.Picture);
            
            // configures one-to-many relationship
            builder.HasMany(m => m.Products)
                .WithOne()
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}