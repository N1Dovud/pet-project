using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UseOwnerDirectlyInToDoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);
            _ = migrationBuilder.DropTable(
                name: "todo_list_permission");

            _ = migrationBuilder.AddColumn<long>(
                name: "owner_id",
                table: "todo_list",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);
            _ = migrationBuilder.DropColumn(
                name: "owner_id",
                table: "todo_list");

            _ = migrationBuilder.CreateTable(
                name: "todo_list_permission",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    todo_list_id = table.Column<long>(type: "bigint", nullable: false),
                    access_level = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_todo_list_permission", x => x.id);
                    _ = table.ForeignKey(
                        name: "FK_todo_list_permission_todo_list_todo_list_id",
                        column: x => x.todo_list_id,
                        principalTable: "todo_list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_todo_list_permission_todo_list_id",
                table: "todo_list_permission",
                column: "todo_list_id");
        }
    }
}
