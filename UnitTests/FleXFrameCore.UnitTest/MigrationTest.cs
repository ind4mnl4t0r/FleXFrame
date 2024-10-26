
using FleXFrameCore.UserAuth;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FleXFrameCore.UnitTest
{
    public class MigrationTest
    {
        private readonly DbContextOptions<DataContext> _options;

        public MigrationTest()
        {
            var connectionString = "Data Source=MSI;Integrated Security=True;Database=TestDB;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        [Fact]
        public void CanApplyMigration()
        {
            using (var context = new DataContext(_options))
            {
                // Apply migrations in your test
                context.Database.Migrate();

                // Perform assertions to ensure database setup
                Assert.True(context.Database.CanConnect(), "The database connection should be successful.");
            }
        }
    }
}
