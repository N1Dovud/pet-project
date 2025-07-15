using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Business.ListTasks;
using WebApi.Business.ToDoLists;
using WebApi.Mappers;
using WebApi.Services.Database;
using WebApi.Services.Database.Entities;

namespace WebApi.Services.TaskServices;

public class ListTaskService(ToDoListDbContext context) : IListTaskService
{
    public async Task<Result> AddTaskAsync(TaskDetails task, long userId, long listId)
    {
        var list = await context.ToDoLists
                .Include(l => l.Tasks)
                .FirstOrDefaultAsync(l => l.Id == listId);
        if (list == null)
        {
            return Result.NotFound("list not found");
        }

        if (list.OwnerId != userId)
        {
            return Result.Forbidden("You do not own this list.");
        }

        var entity = task.ToEntity(userId);
        list.Tasks.Add(entity);

        int rows = await context.SaveChangesAsync();
        if (rows == 0)
        {
            return Result.Error("Nothing added");
        }

        return Result.Success("Task added successfully.");
    }

    public async Task<Result> DeleteTaskAsync(long taskId, long userId, long listId)
    {
        var list = await context.ToDoLists
                .Include(l => l.Tasks)
                .FirstOrDefaultAsync(l => l.Id == listId);
        if (list == null)
        {
            return Result.NotFound("list not found");
        }

        if (list.OwnerId != userId)
        {
            return Result.Forbidden("You do not own this list.");
        }

        var task = list.Tasks.FirstOrDefault(t => t.Id == taskId);

        if (task == null)
        {
            return Result.NotFound("task not found");
        }

        _ = list.Tasks.Remove(task);

        int rows = await context.SaveChangesAsync();
        if (rows == 0)
        {
            return Result.Error("delete failed");
        }

        return Result.Success("Task deleted successfully.");
    }

    public async Task<ListTaskInfo?> GetAllTasksAsync(long userId, long listId)
    {
        ToDoListEntity? list = await context.ToDoLists
            .FindAsync(listId);
        if (list == null)
        {
            return null;
        }

        if (list.OwnerId != userId)
        {
            return null;
        }

        return list.ToListTask();
    }

    public async Task<TaskDetails?> GetTaskAsync(long userId, long taskId)
    {
        var task = await context.Tasks
                .FindAsync(taskId);
        if (task == null)
        {
            return null;
        }

        return task.ToTaskDetails();
    }

    public async Task<Result> UpdateTaskAsync(TaskDetails task, long userId, long listId)
    {
        if (task == null)
        {
            return Result.Error("task is null");
        }

        var taskEntity = await context.Tasks
            .FindAsync(task.Id);
        if (taskEntity == null)
        {
            return Result.NotFound("task not found");
        }

        var ownerId = await context.ToDoLists
                .Where(l => l.Id == listId)
                .Select(l => l.OwnerId)
                .FirstOrDefaultAsync();
        if (ownerId == 0)
        {
            return Result.NotFound("list not found");
        }

        if (ownerId != userId)
        {
            return Result.Forbidden("You do not own this list.");
        }

        if (taskEntity.ToDoListId != listId)
        {
            return Result.Error("Task does not belong to the specified list.");
        }

        if (string.IsNullOrEmpty(task.Description) || string.IsNullOrEmpty(task.Title))
        {
            return Result.Error("Task title and description cannot be null.");
        }

        taskEntity.Title = task.Title;
        taskEntity.Description = task.Description;
        taskEntity.DueDateTime = task.DueDateTime;
        taskEntity.TaskStatus = task.TaskStatus;

        int rows = await context.SaveChangesAsync();
        if (rows == 0)
        {
            return Result.Error("update failed");
        }

        return Result.Success("Task updated successfully.");
    }
}
