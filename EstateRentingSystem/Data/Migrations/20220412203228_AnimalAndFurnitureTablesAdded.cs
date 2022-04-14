using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateRentingSystem.Data.Migrations
{
    public partial class AnimalAndFurnitureTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Estates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FurnitureId",
                table: "Estates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Furnitures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnitures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estates_AnimalId",
                table: "Estates",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Estates_FurnitureId",
                table: "Estates",
                column: "FurnitureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Animals_AnimalId",
                table: "Estates",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Furnitures_FurnitureId",
                table: "Estates",
                column: "FurnitureId",
                principalTable: "Furnitures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Animals_AnimalId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Furnitures_FurnitureId",
                table: "Estates");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Furnitures");

            migrationBuilder.DropIndex(
                name: "IX_Estates_AnimalId",
                table: "Estates");

            migrationBuilder.DropIndex(
                name: "IX_Estates_FurnitureId",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "FurnitureId",
                table: "Estates");
        }
    }
}
