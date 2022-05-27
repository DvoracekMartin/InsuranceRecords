using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceRecordsWeb.Migrations
{
    public partial class AddPolicyHolderUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Insured",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Insured");           
        }
    }
}
