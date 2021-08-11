using Microsoft.EntityFrameworkCore;
using NotificationMicroservice.Core.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationMicroservice.DataAccess.NotificationMicroserviceDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationOptions> NotificationOptions { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Content> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisteredUser diplomski = new RegisteredUser(new Guid("4ddf3498-2204-4a40-b2d4-501c44307e46"), "diplomski", "");
            RegisteredUser diplomskiv2 = new RegisteredUser(new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"), "diplomskiv2", "");
            modelBuilder.Entity<RegisteredUser>().HasData(
                diplomski,
                diplomskiv2

            );

            modelBuilder.Entity<Follow>().HasData(
                new Follow(new Guid("12345678-1234-1234-1234-123412341234"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"), new Guid("4ddf3498-2204-4a40-b2d4-501c44307e46")),
                new Follow(new Guid("12345678-1234-1234-1234-123412341235"), new Guid("4ddf3498-2204-4a40-b2d4-501c44307e46"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"))
            );

            modelBuilder.Entity<Content>().HasData(
                new Content(new Guid("12345678-1234-1234-1234-123412341234"), "Post"),
                new Content(new Guid("12345678-1234-1234-1234-123412341235"), "Story")
            );

            modelBuilder.Entity<NotificationOptions>().HasData(
               new NotificationOptions(new Guid("12345678-1234-1234-1234-123412341234"), false, false, true, true, true, new Guid("4ddf3498-2204-4a40-b2d4-501c44307e46"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"))
            );

            modelBuilder.Entity<Notification>().HasData(
               new Notification(new Guid("12345678-1234-1234-1234-123412341234"), DateTime.Now, new Guid("12345678-1234-1234-1234-123412341234"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc")),
               new Notification(new Guid("12345678-1234-1234-1234-123412341235"), DateTime.Now, new Guid("12345678-1234-1234-1234-123412341235"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"))
            );
        }

        public override int SaveChanges()
        {
            //UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }
    }
}