using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPreviousStatusToContactMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousStatus",
                table: "ContactMessages",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousStatus",
                table: "ContactMessages");
        }
    }
}
