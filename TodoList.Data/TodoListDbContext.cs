using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;
using TodoList.Data.Configurations;

namespace TodoList.Data
{
    public class TodoListDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}