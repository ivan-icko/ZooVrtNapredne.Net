using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddedPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalPackage_Package_PackagesPackageId",
                table: "AnimalPackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Package",
                table: "Package");

            migrationBuilder.RenameTable(
                name: "Package",
                newName: "Packages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packages",
                table: "Packages",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalPackage_Packages_PackagesPackageId",
                table: "AnimalPackage",
                column: "PackagesPackageId",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalPackage_Packages_PackagesPackageId",
                table: "AnimalPackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packages",
                table: "Packages");

            migrationBuilder.RenameTable(
                name: "Packages",
                newName: "Package");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Package",
                table: "Package",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalPackage_Package_PackagesPackageId",
                table: "AnimalPackage",
                column: "PackagesPackageId",
                principalTable: "Package",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
