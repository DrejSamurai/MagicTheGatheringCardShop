using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratedSystems.Repository.Migrations
{
    /// <inheritdoc />
    public partial class card_img : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardImage",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardImage",
                table: "Cards");
        }
    }
}
