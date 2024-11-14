using FleXFrame.Core;
using FleXFrame.Core.Models;
using Microsoft.EntityFrameworkCore;

public class DatabaseFixture : IDisposable
{
    public DataContext Context { get; private set; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer("Data Source=MSI;Integrated Security=True;Database=TestDB;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False") // Replace with your SQL Server connection string
            .Options;

        Context = new DataContext(options);

        // Ensure the database is created and apply migrations
        Context.Database.Migrate();

        // Seed the database with initial data for tests
        SeedDatabase();
    }

    private void SeedDatabase()
    {
        //Context.Users.Add(new User { UserID = "789", Username = "testuser", PasswordHash = new byte[0], PasswordSalt = new byte[0], Name = "test", CreatedBy = "testdata" });
        //Context.SaveChanges();
    }

    public void Dispose()
    {
        // Clean up the database after tests
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
