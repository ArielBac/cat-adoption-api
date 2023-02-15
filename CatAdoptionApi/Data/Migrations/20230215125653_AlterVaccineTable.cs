using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatAdoptionApi.Data.Migrations
{
    public partial class AlterVaccineTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Applicated_at",
                table: "vaccines",
                newName: "Applied_at");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Applied_at",
                table: "vaccines",
                newName: "Applicated_at");
        }
    }
}
