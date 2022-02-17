using HappyTravel.TestConnector.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyTravel.TestConnector.Data;

public class TestConnectorContext : DbContext
{
    public TestConnectorContext(DbContextOptions<TestConnectorContext> options) : base(options)
    {
    }


    public DbSet<Booking> Bookings => Set<Booking>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(builder =>
        {
            builder.HasKey(b => b.ReferenceCode);
            builder.Property(b => b.Rooms).HasColumnType("jsonb");
        });
    }
}