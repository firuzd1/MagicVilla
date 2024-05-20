using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumberToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "villa_number",
                columns: table => new
                {
                    villa_No = table.Column<int>(type: "integer", nullable: false),
                    special_detail = table.Column<string>(type: "text", nullable: false),
                    update_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villa_number", x => x.villa_No);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villa_number");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[] { 1, "", new DateTime(2024, 5, 7, 8, 36, 52, 674, DateTimeKind.Utc).AddTicks(6241), "great villa", "C:\\Users\\Firuz Vasl\\Pictures\\Screenshots\\Снимок экрана 2024-04-23 163311.png", "Royal Villa", 5, 200.0, 500, new DateTime(2024, 5, 7, 8, 36, 52, 674, DateTimeKind.Utc).AddTicks(6242) });
        }
    }
}
