using FleXFrameCore.UserAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace FleXFrameCore.UserAuth
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting a default schema for all tables in this context
            modelBuilder.HasDefaultSchema("UserAuth");

            // Configuring UserRole many-to-many relationship
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.UserRoleID); // Composite primary key if needed

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior if needed

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID)
                .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior if needed

            // Configuring RolePermission many-to-many relationship
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => rp.RolePermissionID); // Primary key for the RolePermission table

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleID)
                .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior if needed

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionID)
                .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior if needed

            base.OnModelCreating(modelBuilder);
        }

        // Constructor for configuring DbContext options
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
