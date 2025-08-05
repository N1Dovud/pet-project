using WebApp.Business.Helpers;
using WebApp.Models.Helpers;

namespace WebApp.Mappers;

public static class TaskHelperMapper
{
    public static EditTaskStatus? ToDomain(this EditTaskStatusModel model)
    {
        if (model == null)
        {
            return null;
        }

        return new EditTaskStatus
        {
            TaskId = model.TaskId,
            TaskStatus = model.TaskStatus,
        };
    }

    public static EditTaskStatusWebApiModel? ToApiModel(this EditTaskStatus model)
    {
        if (model == null)
        {
            return null;
        }

        return new EditTaskStatusWebApiModel
        {
            TaskId = model.TaskId,
            TaskStatus = model.TaskStatus,
        };
    }
}
