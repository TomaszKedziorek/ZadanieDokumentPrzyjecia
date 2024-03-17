using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess;

public class AppDbContext : DbContext
{
  public DbSet<Warehouse> Warehouses { get; set; }
  public DbSet<Address> Addresses { get; set; }
  public DbSet<Supplier> Suppliers { get; set; }
  public DbSet<Label> Labels { get; set; }
  public DbSet<Commodity> Commodities { get; set; }
  public DbSet<AdmissionDocument> AdmissionDocuments { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Warehouse>()
        .HasMany(w => w.AdmissionDocuments)
        .WithOne(ad => ad.TargetWarehouse)
        .HasForeignKey(ad => ad.TargetWarehouseId)
        .OnDelete(DeleteBehavior.SetNull);

    modelBuilder.Entity<Supplier>()
        .HasMany(s => s.AdmissionDocuments)
        .WithOne(ad => ad.Supplier)
        .HasForeignKey(ad => ad.SupplierId)
        .OnDelete(DeleteBehavior.SetNull);

    modelBuilder.Entity<Supplier>()
        .HasOne(s => s.Address)
        .WithOne(a => a.Supplier)
        .HasForeignKey<Address>(a => a.SupplierId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Commodity>()
        .HasOne(c => c.AdmissionDocument)
        .WithMany(ad => ad.CommodityList)
        .HasForeignKey(c => c.AdmissionDocumentId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Label>()
        .HasMany(l => l.AdmissionDocuments)
        .WithMany(ad => ad.Labels);
  }
}
