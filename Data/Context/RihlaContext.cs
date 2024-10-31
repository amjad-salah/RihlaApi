using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Context;

public class RihlaContext(DbContextOptions<RihlaContext> options) : DbContext(options)
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entities = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
            }
            
            ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Journey>().HasOne(j => j.DepCity)
            .WithMany(c => c.DepJourneys)
            .HasForeignKey(j => j.DepCityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Journey>().HasOne(j => j.ArrCity)
            .WithMany(c => c.ArrJourneys)
            .HasForeignKey(j => j.ArrCityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Journey>().HasOne(j => j.Vehicle)
            .WithMany(v => v.Journeys)
            .HasForeignKey(j => j.VehicleId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Journey>().HasOne(j => j.Driver)
            .WithMany(d => d.Journeys)
            .HasForeignKey(j => j.DriverId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Journey>().HasOne(j => j.Company)
            .WithMany(c => c.Journeys)
            .HasForeignKey(j => j.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Journey> Journeys { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}