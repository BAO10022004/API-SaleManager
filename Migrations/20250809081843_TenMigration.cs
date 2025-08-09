using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleManagerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class TenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "code_vetify",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    device_info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ip_address = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_code_vetify", x => x.Id);
                    table.ForeignKey(
                        name: "FK_code_vetify_account",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_email",
                table: "account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_username",
                table: "account",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auth_tokens_account_id",
                table: "auth_tokens",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_auth_tokens_token",
                table: "auth_tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_code_vetify_account_id",
                table: "code_vetify",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_code_vetify_code",
                table: "code_vetify",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth_tokens");

            migrationBuilder.DropTable(
                name: "code_vetify");

            migrationBuilder.DropTable(
                name: "account");
        }
    }
}
