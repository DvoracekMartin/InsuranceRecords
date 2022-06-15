using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceRecordsWeb.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[,]
            {
                { "8f04fd4c-0e74-4647-9767-d75478798b1e", "User", "USER", "594fd5cf-edfe-48ae-b4a0-60febea24b05" },
                { "e01cf6ba-bd8c-4446-a085-bfb03b534332", "Administrator", "ADMINISTRATOR", "f5b61ddf-5e3f-43a0-8513-e71a75d0fcc5" }
            });

            migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[]
            {
                "Id",
                "Name",
                "LastName",
                "StreetName",
                "BuildingNumber",
                "CityName",
                "ZipCode",
                "Email",
                "TelephoneNumber",
                "UserName",
                "NormalizedUserName",
                "NormalizedEmail",
                "EmailConfirmed",
                "PasswordHash",
                "SecurityStamp",
                "ConcurrencyStamp",
                "PhoneNumber",
                "PhoneNumberConfirmed",
                "TwoFactorEnabled",
                "LockoutEnd",
                "LockoutEnabled",
                "AccessFailedCount"
            },
            values: new object[]
            {
                "472f8bb0-3e16-4e03-9635-9250038bd255",
                "Karel",
                "Svoboda",
                "Náměstí 28. října",
                "1843/7",
                "Brno",
                "60200",
                "admin@test.com",
                "123456789",
                "admin@test.com",
                "ADMIN@TEST.COM",
                "ADMIN@TEST.COM",
                Convert.ToBoolean(0),
                "AQAAAAEAACcQAAAAEMhP8zhpQobLkSrVLNqEbdgCB3X643M5Ie1Ant7CNr9Zr4MkhbyVvu4KxoJh6z8xoA==",
                "NAIGI6QZQYH5NDFBINC3HQCALSDMCQHG",
                "1161a309-9178-4aaa-a472-92c884f0b3f0",
                null,
                Convert.ToBoolean(0),
                Convert.ToBoolean(0),
                null,
                Convert.ToBoolean(1),
                0
            });

            migrationBuilder.InsertData(
            table: "AspNetUserRoles",
            columns: new[] { "UserId", "RoleId" },
            values: new object[]
            { "472f8bb0-3e16-4e03-9635-9250038bd255", "e01cf6ba-bd8c-4446-a085-bfb03b534332" }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
