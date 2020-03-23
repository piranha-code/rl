using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Entities;

namespace Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.ToTable("Employees");
            
            builder.Ignore(m => m.Photo);
            
            builder.Ignore(m => m.Photo);          
            
            // configures key
            builder.HasKey(m => m.EmployeeId)
                .HasName("PK_Employees");

            // configures one-to-many relationship
            /*builder.HasMany(m => m.ReportsToEmployee)            
                .WithOne()
                .HasForeignKey(m => m.EmployeeID)
                .OnDelete(DeleteBehavior.NoAction);*/
        }
    }
}