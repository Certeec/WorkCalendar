using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserLogInHistoryNamesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "duration",
                table: "UserLoginLogs",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "ip",
                table: "UserLoginLogs",
                newName: "UserIp");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "UserLoginLogs",
                newName: "LoginDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "UserLoginLogs",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "UserIp",
                table: "UserLoginLogs",
                newName: "ip");

            migrationBuilder.RenameColumn(
                name: "LoginDate",
                table: "UserLoginLogs",
                newName: "date");
        }
    }
}
