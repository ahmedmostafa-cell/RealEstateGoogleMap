using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class archiveSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "ArchiveReasone",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentWay",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaserName",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaserPhoneNumber",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchasingValue",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchiveReasone",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "PaymentWay",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "PurchaserName",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "PurchaserPhoneNumber",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "PurchasingValue",
                table: "TbOffer");

           
        }
    }
}
