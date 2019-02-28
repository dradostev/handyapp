using Handy.Domain.AccountContext.Entities;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.TodoContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Handy.Infrastructure
{
    public class HandyDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Todo> Todos { get; set; }
        
        public HandyDbContext(DbContextOptions<HandyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupAccountsTable(modelBuilder);
            SetupNotesTable(modelBuilder);
            SetupTodoListsTable(modelBuilder);
            SetupTodosTable(modelBuilder);
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

        private static void SetupTodoListsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>()
                .ToTable("todo_lists");
            modelBuilder.Entity<TodoList>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<TodoList>()
                .Property(p => p.AccountId)
                .HasColumnName("account_id");
            modelBuilder.Entity<TodoList>()
                .Property(p => p.Created)
                .HasColumnName("created");
            modelBuilder.Entity<TodoList>()
                .Property(p => p.Modified)
                .HasColumnName("modified");
            modelBuilder.Entity<TodoList>()
                .HasOne(p => p.Account)
                .WithMany(p => p.TodoLists)
                .HasForeignKey(p => p.AccountId);
        }

        private static void SetupTodosTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .ToTable("todo_items");
            modelBuilder.Entity<Todo>()
                .Property(p => p.Id)
                .HasColumnName("id");
            modelBuilder.Entity<Todo>()
                .Property(p => p.TodoListId)
                .HasColumnName("todo_list_id");
            modelBuilder.Entity<Todo>()
                .Property(p => p.Title)
                .HasColumnName("title");
            modelBuilder.Entity<Todo>()
                .Property(p => p.Done)
                .HasColumnName("done");
            modelBuilder.Entity<Todo>()
                .Property(p => p.Created)
                .HasColumnName("created");
            modelBuilder.Entity<Todo>()
                .Property(p => p.Modified)
                .HasColumnName("modified");
            modelBuilder.Entity<Todo>()
                .HasOne(p => p.TodoList)
                .WithMany(p => p.Todos)
                .HasForeignKey(p => p.TodoListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}