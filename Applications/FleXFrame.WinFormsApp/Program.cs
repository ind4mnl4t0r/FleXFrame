using FleXFrame.AuthHub.Services;
using FleXFrame.AuthHub;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using FleXFrame.AuthHub.Helpers;
using FleXFrame.AuthHub.Repositories;
using FleXFrame.AuthHub.Interfaces.IRepositories;
using FleXFrame.AuthHub.Interfaces.IServices;
using FleXFrame.UtilityHub.WinForms.Services;
using FleXFrame.AcademicSuite;
using FleXFrame.AcademicSuite.Repositories;
using FleXFrame.AcademicSuite.Interfaces.IRepositories;
using FleXFrame.UtilityHub.WinForms.Helpers;

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
            // Set up configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Set up DI container
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Set the service provider in the class library
            FormFactory.SetServiceProvider(_serviceProvider);

            // Apply migrations
            ApplyMigrations();

            // Start the application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = _serviceProvider.GetRequiredService<DashboardForm>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Retrieve values from configuration (e.g., appsettings.json)
            var secretKey = configuration["Jwt:SecretKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey is not configured in the application settings.");
            }

            var expiryInMinutes = int.Parse(configuration["Jwt:ExpiryInMinutes"] ?? "30"); // Default to 30 minutes if not found

            // Register multiple DbContexts with the same connection string
            services.AddDbContext<AuthHubDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            services.AddDbContext<AcademicSuiteDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

            // Add Automapper
            services.AddAutoMapper(typeof(MappingProfiles));

            // Add services and repositories for AuthHub
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddSingleton(new JwtHelper(secretKey, expiryInMinutes));
            services.AddSingleton<InteractionTracker>();

            // Add services and repositories for AcademicSuite
            services.AddScoped<IStudentRepository, StudentRepository>();

            // Register forms
            services.AddTransient<UserLoginForm>();
            services.AddTransient<DashboardForm>();
            services.AddTransient<UserRegistrationForm>();
            services.AddTransient<HomeForm>();
            services.AddTransient<StudentPaymentForm>();
            services.AddTransient<SettingsForm>();
        }

        private static void ApplyMigrations()
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("Service provider is not initialized.");

            using (var scope = _serviceProvider.CreateScope())
            {
                // Apply migrations for AuthHubDataContext
                var authHubContext = scope.ServiceProvider.GetRequiredService<AuthHubDataContext>();
                authHubContext.Database.Migrate();
                Console.WriteLine("AuthHubDataContext migrations applied.");

                // Apply migrations for AcademicSuiteDataContext
                var academicSuiteContext = scope.ServiceProvider.GetRequiredService<AcademicSuiteDataContext>();
                academicSuiteContext.Database.Migrate();
                Console.WriteLine("AcademicSuiteDataContext migrations applied.");
            }
        }

        public static Form ResolveForm<TForm>() where TForm : Form
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("Service provider is not initialized.");

            return _serviceProvider.GetRequiredService<TForm>();
        }
    }
}