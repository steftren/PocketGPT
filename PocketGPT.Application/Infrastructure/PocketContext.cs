using Microsoft.EntityFrameworkCore;
using System;

public class PocketContext : DbContext
{
    // TODO: Add your DbSets

    public PocketContext(DbContextOptions<PocketContext> opt) : base(opt) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Additional config

        // Generic config for all entities
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // ON DELETE RESTRICT instead of ON DELETE CASCADE
            foreach (var key in entityType.GetForeignKeys())
                key.DeleteBehavior = DeleteBehavior.Restrict;

            foreach (var prop in entityType.GetDeclaredProperties())
            {
                // Define Guid as alternate key. The database can create a guid fou you.
                if (prop.Name == "Guid")
                {
                    modelBuilder.Entity(entityType.ClrType).HasAlternateKey("Guid");
                    prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                }
                // Default MaxLength of string Properties is 255.
                if (prop.ClrType == typeof(string) && prop.GetMaxLength() is null) prop.SetMaxLength(255);
                // Seconds with 3 fractional digits.
                if (prop.ClrType == typeof(DateTime)) prop.SetPrecision(3);
                if (prop.ClrType == typeof(DateTime?)) prop.SetPrecision(3);
            }
        }

    }
    /// <summary>
    /// Initialize the database with some values (holidays, ...).
    /// Unlike Seed, this method is also called in production.
    /// </summary>
    private void Initialize()
    {
        // Seed logic.
    }
    /// <summary>
    /// Generates random values for testing the application. This method is only called in development mode.
    /// </summary>    
    private void Seed()
    {
        // Seed logic.
    }
    /// <summary>
    /// Creates the database. Called once at application startup.
    /// </summary>    
    public void CreateDatabase(bool isDevelopment)
    {
        if (isDevelopment) { Database.EnsureDeleted(); }
        // EnsureCreated only creates the model if the database does not exist or it has no
        // tables. Returns true if the schema was created.  Returns false if there are
        // existing tables in the database. This avoids initializing multiple times.
        if (Database.EnsureCreated()) { Initialize(); }
        if (isDevelopment) Seed();
    }
}