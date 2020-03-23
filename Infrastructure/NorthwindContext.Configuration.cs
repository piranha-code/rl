using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Infrastructure
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext() : base() { }

        public Lazy<ModelBuilder> ModelBuilder => new Lazy<ModelBuilder>(() =>
        {
            var conventionSet = new ConventionSet();
            var modelBuilder = new ModelBuilder(conventionSet);
            OnModelCreating(modelBuilder);
            return modelBuilder;
        }, true);
    }
}