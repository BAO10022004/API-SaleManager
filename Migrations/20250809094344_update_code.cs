using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleManagerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class update_code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "verified_at ",
                table: "code_vetify",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verified_at ",
                table: "code_vetify");
        }
    }
}
