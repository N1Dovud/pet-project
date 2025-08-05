using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Business.Helpers;
using WebApi.Business.ListTasks;
using WebApi.Business.ToDoLists;
using WebApi.Mappers;
using WebApi.Models.Enums;
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

    public async Task<Result> DeleteTaskAsync(long taskId, long userId)
    {
        var list = await context.ToDoLists
                .Include(l => l.Tasks)
                .FirstOrDefaultAsync(l => l.Tasks.Any(t => t.Id == taskId));
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

    public async Task<Result> EditTaskStatusAsync(long userId, EditTaskStatus model)
    {
        if (model == null)
        {
            return Result.Error("model is null");
        }

        var taskEntity = await context.Tasks
            .Include(t => t.ToDoList)
            .SingleOrDefaultAsync(t => t.Id == model.TaskId);
        if (taskEntity == null)
        {
            return Result.NotFound("task not found");
        }

        if (taskEntity.ToDoList?.OwnerId == userId || taskEntity.Assignee == userId)
        {
            taskEntity.TaskStatus = model.TaskStatus;
            int rows = await context.SaveChangesAsync();
            if (rows == 0)
            {
                return Result.Error("update failed");
            }

            return Result.Success("Task updated successfully.");
        }

        return Result.Forbidden("You cannot edit this task");
    }

    public async Task<ListTaskInfo?> GetAllTasksAsync(long userId, long listId)
    {
        ToDoListEntity? list = await context.ToDoLists
            .Include(l => l.Tasks)
            .FirstOrDefaultAsync(l => l.Id == listId);
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

    public async Task<List<TaskSummary?>?> GetAssignedTasks(long userId)
    {
        var tasks = await context.Tasks
            .Where(t => t.Assignee == userId)
            .ToListAsync();

        return [.. tasks.Select(t => t.ToDomain())];
    }

    public async Task<List<TaskSummary?>?> GetOverdueTasks(long userId)
    {
        var tasks = await context.Tasks
            .Include(t => t.ToDoList)
            .Where(t => t.ToDoList.OwnerId == userId)
            .Where(t => t.TaskStatus != ToDoListTaskStatus.Completed)
            .Where(t => t.DueDateTime < DateTime.Now)
            .ToListAsync();

        return [.. tasks.Select(t => t.ToDomain())];
    }

    public async Task<TaskDetails?> GetTaskAsync(long userId, long taskId)
    {
        var task = await context.Tasks
            .Include(t => t.ToDoList)
            .SingleOrDefaultAsync(t => t.Id == taskId);
        if (task == null)
        {
            return null;
        }

        if (task.ToDoList?.OwnerId == userId || task.Assignee == userId)
        {
            return task.ToTaskDetails();
        }

        return null;
    }

    public async Task<Result> UpdateTaskAsync(TaskDetails task, long userId)
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
                .Where(l => l.OwnerId == userId)
                .Select(l => l.OwnerId)
                .FirstOrDefaultAsync();
        if (ownerId == 0)
        {
            return Result.NotFound("list not found");
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
