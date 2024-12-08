using FleXFrame.AuthHub.Models;
using Microsoft.EntityFrameworkCore;

namespace FleXFrame.AuthHub
{
    public class AuthHubDataContext : DbContext
    {
        // Constructor for configuring DbContext options
        public AuthHubDataContext(DbContextOptions<AuthHubDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring tables with schema "UserAuth"
            modelBuilder.Entity<User>().ToTable("Users", "UserAuth");
            modelBuilder.Entity<Role>().ToTable("Roles", "UserAuth");
            modelBuilder.Entity<Permission>().ToTable("Permissions", "UserAuth");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles", "UserAuth");
            modelBuilder.Entity<RolePermission>().ToTable("RolePermissions", "UserAuth");
            modelBuilder.Entity<Session>().ToTable("Sessions", "UserAuth");

            // Define Primery Keys
            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleID);
            modelBuilder.Entity<Permission>().HasKey(p => p.PermissionID);
            modelBuilder.Entity<UserRole>().HasKey(ur => ur.UserRoleID);
            modelBuilder.Entity<RolePermission>().HasKey(rp => rp.RolePermissionID);
            modelBuilder.Entity<Session>().HasKey(s => s.Token);

            // Configuring UserRole many-to-many relationship
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring RolePermission many-to-many relationship
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring Session entity
            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)  // Assuming User has a collection of Sessions
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a user will delete their sessions as well
            modelBuilder.Entity<Session>()
                .Property(s => s.SessionStatus)
                .HasConversion<string>();  // Convert enum to string
            modelBuilder.Entity<Session>()
                .HasIndex(s => s.Token)
                .HasDatabaseName("IX_Sessions_Token")
                .IsUnique();  // Ensure Token is unique
            modelBuilder.Entity<Session>()
                .Property(s => s.ExpiryTime)
                .IsRequired();
            modelBuilder.Entity<Session>()
                .Property(s => s.SessionStatus)
                .HasDefaultValue(Session.SessionStatuses.Active);  // Default session status to Active



            modelBuilder.Entity<User>()
                .Property(u => u.UserStatus)
                .HasConversion<string>();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserID)
                .HasDatabaseName("IX_Users_UserID")
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .HasDatabaseName("IX_Users_Username")
                .IsUnique();



            modelBuilder.Entity<User>()
                .Property(u => u.DateCreated)
                .HasDefaultValueSql("GETDATE()");



            base.OnModelCreating(modelBuilder);
        }
    }


}
