using CRUDOperations.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperations.Data
{
    public partial class InventoryContext : DbContext
    {
        
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.LastUpdate).HasColumnType("smalldatetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
                
            });

            //For Uniqueness
            // modelBuilder.Entity<Products>()
            //     .HasAlternateKey(a => a.Code);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
