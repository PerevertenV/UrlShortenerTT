using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOfUser = table.Column<int>(type: "int", nullable: false),
                    LongUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urls_Users_IdOfUser",
                        column: x => x.IdOfUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Login", "Password", "Role" },
                values: new object[] { 1, "admin", "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXSviaP+MhkWco44Ognl56AAAAAACAAAAAAAQZgAAAAEAACAAAACyJUHBMGlxYBZfQLUph+MG7HAoGQ6KyvzExw3ymB9kdAAAAAAOgAAAAAIAACAAAADq+Qk6o9xCSGssQRqt1R7laWl5hfsbtySjX7t1VKpwAhAAAAAD/iqqmULivfii2YSOIstNQAAAAHKfj4xisTwSw1rEF0GSRBIHgHLlJEDU0vO4XIzWCB2PKOxxf97GpgjntB80KbRVjflFHhzugrpfTUV4ilBCvJY=", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Urls_IdOfUser",
                table: "Urls",
                column: "IdOfUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urls");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
