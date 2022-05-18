using Microsoft.EntityFrameworkCore.Migrations;

namespace SM.Entity.Migrations
{
    /// <summary>
    /// Adding resetpasswordcode coloum in user entity
    /// </summary>
    public partial class addedResetPasswordCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordCode",
                table: "Users");
        }
    }
}
