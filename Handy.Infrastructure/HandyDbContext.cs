using Handy.Domain.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Handy.Infrastructure
{
    public class HandyDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        
        public HandyDbContext(DbContextOptions<HandyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .ToTable("accounts");
            modelBuilder.Entity<Account>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Account>()
                .Property(p => p.Login)
                .HasColumnName("login");
            modelBuilder.Entity<Account>()
                .Property(p => p.Password)
                .HasColumnName("password_hash");
            modelBuilder.Entity<Account>()
                .Property(p => p.ScreenName)
                .HasColumnName("screen_name");
            modelBuilder.Entity<Account>()
                .Property(p => p.Registered)
                .HasColumnName("registered");
            modelBuilder.Entity<Account>()
                .Property(p => p.Modified)
                .HasColumnName("modified");
            modelBuilder.Entity<Account>()
                .HasIndex(p => p.Login)
                .IsUnique();
        }
    }
}