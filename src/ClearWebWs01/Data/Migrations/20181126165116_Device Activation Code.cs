using Microsoft.EntityFrameworkCore.Migrations;

namespace ClearWebWs01.Data.Migrations
{
    public partial class DeviceActivationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivationCode",
                table: "Device",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "Device");
        }
    }
}
