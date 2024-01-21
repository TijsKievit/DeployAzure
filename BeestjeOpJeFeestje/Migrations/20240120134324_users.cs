using Microsoft.EntityFrameworkCore.Migrations;

namespace BeestjeOpJeFeestje.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "Animal",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Animal",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "imageLink",
                table: "Animal",
                newName: "ImageLink");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Animal",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Card",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adres",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Card",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Animal",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Animal",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "ImageLink",
                table: "Animal",
                newName: "imageLink");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Animal",
                newName: "name");
        }
    }
}
