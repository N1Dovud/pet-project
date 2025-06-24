using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "todo_list",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_list", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "todo_list_permission",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    todo_list_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    access_level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_list_permission", x => x.id);
                    table.ForeignKey(
                        name: "FK_todo_list_permission_todo_list_todo_list_id",
                        column: x => x.todo_list_id,
                        principalTable: "todo_list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "todo_task",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creation_date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    due_date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    task_status = table.Column<int>(type: "int", nullable: false),
                    assignee = table.Column<long>(type: "bigint", nullable: false),
                    todo_list_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_todo_task_todo_list_todo_list_id",
                        column: x => x.todo_list_id,
                        principalTable: "todo_list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text_note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    task_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    creation_date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    last_edit_date_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_comment_todo_task_task_id",
                        column: x => x.task_id,
                        principalTable: "todo_task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_tag",
                columns: table => new
                {
                    task_id = table.Column<long>(type: "bigint", nullable: false),
                    tag_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_tag", x => new { x.task_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_task_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_tag_todo_task_task_id",
                        column: x => x.task_id,
                        principalTable: "todo_task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_task_id",
                table: "comment",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_tag_tag_id",
                table: "task_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_todo_list_permission_todo_list_id",
                table: "todo_list_permission",
                column: "todo_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_todo_task_todo_list_id",
                table: "todo_task",
                column: "todo_list_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "task_tag");

            migrationBuilder.DropTable(
                name: "todo_list_permission");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "todo_task");

            migrationBuilder.DropTable(
                name: "todo_list");
        }
    }
}
