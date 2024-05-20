using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "villa_id",
                table: "villa_number",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_villa_number_villa_id",
                table: "villa_number",
                column: "villa_id");

            migrationBuilder.AddForeignKey(
                name: "FK_villa_number_Villas_villa_id",
                table: "villa_number",
                column: "villa_id",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villa_number_Villas_villa_id",
                table: "villa_number");

            migrationBuilder.DropIndex(
                name: "IX_villa_number_villa_id",
                table: "villa_number");

            migrationBuilder.DropColumn(
                name: "villa_id",
                table: "villa_number");
        }
    }
}
