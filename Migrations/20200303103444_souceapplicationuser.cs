using Microsoft.EntityFrameworkCore.Migrations;

namespace Vegetable_Grocery_MISC.Migrations
{
    public partial class souceapplicationuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "source",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "source",
                table: "AspNetUsers");
        }
    }
}
