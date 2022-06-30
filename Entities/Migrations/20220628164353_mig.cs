using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Apartments_ApartmentsId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ApartmentsId",
                table: "Users",
                newName: "ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ApartmentsId",
                table: "Users",
                newName: "IX_Users_ApartmentId");

            migrationBuilder.RenameColumn(
                name: "PhotoLocation",
                table: "Apartments",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistanceFromCenter",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gym",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Parking",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pool",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Wifi",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Apartments_ApartmentId",
                table: "Users",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Apartments_ApartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gym",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Parking",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Pool",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Wifi",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "ApartmentId",
                table: "Users",
                newName: "ApartmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ApartmentId",
                table: "Users",
                newName: "IX_Users_ApartmentsId");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Apartments",
                newName: "PhotoLocation");

            migrationBuilder.AlterColumn<string>(
                name: "DistanceFromCenter",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Apartments_ApartmentsId",
                table: "Users",
                column: "ApartmentsId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }
    }
}
