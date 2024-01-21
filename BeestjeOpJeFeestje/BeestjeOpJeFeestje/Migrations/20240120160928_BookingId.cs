using Microsoft.EntityFrameworkCore.Migrations;

namespace BeestjeOpJeFeestje.Migrations
{
    public partial class BookingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Bookings_BookingId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_BookingId",
                table: "Animals");

            migrationBuilder.AlterColumn<string>(
                name: "BookingId",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId1",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_BookingId1",
                table: "Animals",
                column: "BookingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Bookings_BookingId1",
                table: "Animals",
                column: "BookingId1",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Bookings_BookingId1",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_BookingId1",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "BookingId1",
                table: "Animals");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Animals",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_BookingId",
                table: "Animals",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Bookings_BookingId",
                table: "Animals",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
