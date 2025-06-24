using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi.Service.Database;

public class ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : DbContext(options)
{
    public DbSet<ToDoListEntity> ToDoLists { get; set; }

    public DbSet<CommentEntity> Comments { get; set; }

    public DbSet<TagEntity> Tags { get; set; }

    public DbSet<ToDoListPermissionEntity> ListPermissions { get; set; }

    public DbSet<ToDoListTaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        _ = modelBuilder.Entity<ToDoListTaskEntity>()
            .HasMany(t => t.Tags)
            .WithMany(t => t.Tasks)
            .UsingEntity<Dictionary<string, object>>(
                "task_tag",
                right => right.HasOne<TagEntity>()
                                .WithMany()
                                .HasForeignKey("tag_id")
                                .OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<ToDoListTaskEntity>()
                                .WithMany()
                                .HasForeignKey("task_id"),
                join =>
                {
                    _ = join.HasKey("task_id", "tag_id");
                });
    }
}
