using FleXFrame.AcademicSuite.Services;
using FleXFrame.AuthHub;
using FleXFrame.AcademicSuite;
using FleXFrame.AuthHub.DTOs;
using FleXFrame.AuthHub.DTOs.UserDtos;
using FleXFrame.AuthHub.Helpers;
using FleXFrame.AuthHub.Models;
using FleXFrame.AuthHub.Services;
using FleXFrame.UtilityHub;
using FleXFrame.UtilityHub.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using FleXFrame.AcademicSuite.Interfaces.IRepositories;
using FleXFrame.AcademicSuite.Repositories;
using FleXFrame.AuthHub.Interfaces.IRepositories;
using FleXFrame.AuthHub.Interfaces.IServices;
using FleXFrame.AuthHub.Repositories;
using FleXFrame.AcademicSuite.Interfaces.IServices;

namespace FleXFrame.ConsoleApp
{
    internal class Program
    {
        private static ServiceProvider? _serviceProvider; // Nullable _serviceProvider

        static async Task Main(string[] args)
        {
            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Set up DI container
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Apply migrations
            ApplyMigrations();

            // Dispose service provider at the end of the application lifecycle
            await _serviceProvider.DisposeAsync();
        }

        /// <summary>
        /// Configures the services for dependency injection.
        /// </summary>
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register multiple DbContexts with the same connection string
            services.AddDbContext<AuthHubDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            services.AddDbContext<AcademicSuiteDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

            // Add AutoMapper
            services.AddAutoMapper(typeof(MappingProfiles));

            // Add services and repositories for AuthHub
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Add services and repositories for AcademicSuite
            //services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
        }

        /// <summary>
        /// Applies pending migrations for all registered DbContexts.
        /// </summary>
        private static void ApplyMigrations()
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("Service provider is not initialized.");

            using (var scope = _serviceProvider.CreateScope())
            {
                ApplyDbContextMigrations<AuthHubDataContext>(scope, "AuthHubDataContext");
                ApplyDbContextMigrations<AcademicSuiteDataContext>(scope, "AcademicSuiteDataContext");
            }
        }

        /// <summary>
        /// Generic method to apply migrations for a specific DbContext.
        /// </summary>
        private static void ApplyDbContextMigrations<TContext>(IServiceScope scope, string contextName) where TContext : DbContext
        {
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            var pendingMigrations = context.Database.GetPendingMigrations();

            if (pendingMigrations.Any())
            {
                Console.WriteLine($"Pending migrations for {contextName}:");
                foreach (var migration in pendingMigrations)
                {
                    Console.WriteLine($" - {migration}");
                }
                context.Database.Migrate();
                Console.WriteLine($"{contextName} migrations applied.");
            }
            else
            {
                Console.WriteLine($"No pending migrations for {contextName}.");
            }
        }
    }

}
