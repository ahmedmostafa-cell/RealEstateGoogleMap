using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class mapapply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "contract_number",
                table: "TbRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contract_type",
                table: "TbRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customer_name",
                table: "TbRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "TbRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "lat",
                table: "TbRequest",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "lng",
                table: "TbRequest",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfferEndTime",
                table: "TbOffer",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "contract_number",
                table: "TbOffer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contract_type",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customer_name",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "lat",
                table: "TbOffer",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "lng",
                table: "TbOffer",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "contract_number",
                table: "TbMap",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contract_number",
                table: "TbRequest");

            migrationBuilder.DropColumn(
                name: "contract_type",
                table: "TbRequest");

            migrationBuilder.DropColumn(
                name: "customer_name",
                table: "TbRequest");

            migrationBuilder.DropColumn(
                name: "icon",
                table: "TbRequest");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "TbRequest");

            migrationBuilder.DropColumn(
                name: "lng",
                table: "TbRequest");

            migrationBuilder.DropColumn(
                name: "contract_number",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "contract_type",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "customer_name",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "icon",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "lng",
                table: "TbOffer");

            migrationBuilder.AlterColumn<string>(
                name: "OfferEndTime",
                table: "TbOffer",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "contract_number",
                table: "TbMap",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
