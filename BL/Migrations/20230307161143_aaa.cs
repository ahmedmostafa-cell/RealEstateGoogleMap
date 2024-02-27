using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class aaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.CreateTable(
                name: "TbMap",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    lat = table.Column<decimal>(type: "NUMERIC(38,16)", nullable: true),
                    lng = table.Column<decimal>(type: "NUMERIC(38,16)", nullable: true),
                    contract_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    contract_number = table.Column<int>(type: "int", nullable: true),
                    icon = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbMap", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbMap");

          
        }
    }
}
