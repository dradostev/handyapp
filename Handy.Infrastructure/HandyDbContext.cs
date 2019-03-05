using Handy.Domain.AccountContext.Entities;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.ReminderContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Handy.Infrastructure
{
    public class HandyDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        
        public HandyDbContext(DbContextOptions<HandyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupAccountsTable(modelBuilder);
            SetupNotesTable(modelBuilder);
            SetupRemindersTable(modelBuilder);
        }

        private static void SetupAccountsTable(ModelBuilder modelBuilder)
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
                .Property(p => p.BotChatId)
                .HasColumnName("telegram_chat_id");
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

        private static void SetupNotesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .ToTable("notes");
            modelBuilder.Entity<Note>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Note>()
                .Property(p => p.AccountId)
                .HasColumnName("account_id");
            modelBuilder.Entity<Note>()
                .Property(p => p.Title)
                .HasColumnName("title");
            modelBuilder.Entity<Note>()
                .Property(p => p.Content)
                .HasColumnName("content");
            modelBuilder.Entity<Note>()
                .Property(p => p.MessageId)
                .HasColumnName("telegram_message_id");
            modelBuilder.Entity<Note>()
                .Property(p => p.Created)
                .HasColumnName("created");
            modelBuilder.Entity<Note>()
                .Property(p => p.Modified)
                .HasColumnName("modified");
            modelBuilder.Entity<Note>()
                .HasOne(p => p.Account)
                .WithMany(p => p.Notes)
                .HasForeignKey(p => p.AccountId);
        }

        private static void SetupRemindersTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reminder>()
                .ToTable("reminders");
            modelBuilder.Entity<Reminder>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Reminder>()
                .Property(p => p.AccountId)
                .HasColumnName("account_id");
            modelBuilder.Entity<Reminder>()
                .Property(p => p.MessageId)
                .HasColumnName("telegram_message_id");
            modelBuilder.Entity<Reminder>()
                .Property(p => p.Content)
                .HasColumnName("content");
            modelBuilder.Entity<Reminder>()
                .Property(p => p.FireOn)
                .HasColumnName("fire_on");
            modelBuilder.Entity<Reminder>()
                .Property(p => p.Enabled)
                .HasColumnName("enabled");
            modelBuilder.Entity<Reminder>()
                .Property(p => p.Created)
                .HasColumnName("created");
            modelBuilder.Entity<Reminder>()
                .Property(p => p.Modified)
                .HasColumnName("modified");
            modelBuilder.Entity<Reminder>()
                .HasOne(p => p.Account)
                .WithMany(p => p.Reminders)
                .HasForeignKey(p => p.AccountId);
        }
    }
}