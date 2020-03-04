using Microsoft.EntityFrameworkCore.Migrations;

namespace Vegetable_Grocery_MISC.Migrations
{
    public partial class productcategoryid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "subcategoryid",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "subsubcategoryid",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subcategoryid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "subsubcategoryid",
                table: "Products");
        }
    }
}
