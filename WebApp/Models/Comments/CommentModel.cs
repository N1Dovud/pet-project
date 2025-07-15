using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Services.Database.Entities;

namespace WebApp.Models.Comments;

public class CommentModel
{
    public long Id { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    public DateTime LastEditDateTime { get; set; } = DateTime.Now;
}
