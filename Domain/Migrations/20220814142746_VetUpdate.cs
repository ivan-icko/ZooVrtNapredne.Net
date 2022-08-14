using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class VetUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Vets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Vets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
