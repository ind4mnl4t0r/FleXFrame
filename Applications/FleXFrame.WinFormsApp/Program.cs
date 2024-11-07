using FleXFrame.Core.Services;
using FleXFrame.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using FleXFrame.Core.Helpers;

namespace FleXFrame.WinFormsApp
{
    internal static class Program
    {
        private static ServiceProvider? _serviceProvider; // Make _serviceProvider nullable

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set up configuration to read from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Set up DI container
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Apply migrations (only in development, if required)
            ApplyMigrations();

            // Start the application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the main form using the DI provider
            var mainForm = _serviceProvider.GetRequiredService<DashboardForm>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with SQL Server
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

            // Register other services
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<UserService>();
            

            // Register the main form so DI can inject services into it
            services.AddScoped<DashboardForm>();
        }

        private static void ApplyMigrations()
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider is not initialized.");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
                Console.WriteLine("Migration completed");
            }
        }
    }
}