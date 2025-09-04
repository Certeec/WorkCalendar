using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskState",
                table: "WorkPlannerTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                UPDATE WorkPlannerTasks 
                SET TaskState = 
                    CASE TaskType
                        WHEN 'Availabe' THEN 0
                        WHEN 'Planned' THEN 1
                        WHEN 'Done' THEN 2
                        WHEN 'Unavailabe' THEN 3
                        ELSE 0
                    END
            ");

            migrationBuilder.DropColumn(
                name: "TaskType",
                table: "WorkPlannerTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskType",
                table: "WorkPlannerTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                UPDATE WorkPlannerTasks 
                SET TaskType = 
                    CASE TaskState
                        WHEN 0 THEN 'Availabe'
                        WHEN 1 THEN 'Planned'
                        WHEN 2 THEN 'Done'
                        WHEN 3 THEN 'Unavailabe'
                        ELSE 'Availabe'
                    END
            ");

            migrationBuilder.DropColumn(
                name: "TaskState",
                table: "WorkPlannerTasks");
        }
    }
}