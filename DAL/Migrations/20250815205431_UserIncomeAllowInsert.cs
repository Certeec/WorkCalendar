using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserIncomeAllowInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSchedulerDefaultHourIncome",
                table: "UserSchedulerDefaultHourIncome");


            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSchedulerDefaultHourIncome");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserSchedulerDefaultHourIncome",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSchedulerDefaultHourIncome",
                table: "UserSchedulerDefaultHourIncome",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSchedulerDefaultHourIncome",
                table: "UserSchedulerDefaultHourIncome");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSchedulerDefaultHourIncome");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserSchedulerDefaultHourIncome",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"); 

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSchedulerDefaultHourIncome",
                table: "UserSchedulerDefaultHourIncome",
                column: "UserId");
        }
    }
}
