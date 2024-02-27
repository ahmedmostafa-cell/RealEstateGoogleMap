using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class offersellingstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellingStatus",
                table: "TbOfferBooking",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellingStatus",
                table: "TbOfferBooking");
        }
    }
}
