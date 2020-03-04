using Microsoft.EntityFrameworkCore.Migrations;

namespace Vegetable_Grocery_MISC.Migrations
{
    public partial class categoryimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Categories");
        }
    }
}
