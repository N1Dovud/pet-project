using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Service.Database;

public class ToDoListDbContext : DbContext
{
    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
        : base(options)
    {
    }

    public DbSet<ToDoListEntity> ToDoLists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<ToDoListEntity>()
            .HasKey(t => t.Id);
        _ = modelBuilder.Entity<ToDoListEntity>()
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
