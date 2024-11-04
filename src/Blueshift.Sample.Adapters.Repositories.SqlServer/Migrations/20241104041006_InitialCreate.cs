using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blueshift.Sample.Adapters.Repositories.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "BookLoans",
                columns: table => new
                {
                    BookLoanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BorrowerMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LentBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LoanTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLoans", x => x.BookLoanId);
                    table.ForeignKey(
                        name: "FK_BookLoans_Books_LentBookId",
                        column: x => x.LentBookId,
                        principalTable: "Books",
                        principalColumn: "BookId");
                    table.ForeignKey(
                        name: "FK_BookLoans_Members_BorrowerMemberId",
                        column: x => x.BorrowerMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_BorrowerMemberId",
                table: "BookLoans",
                column: "BorrowerMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_LentBookId",
                table: "BookLoans",
                column: "LentBookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLoans");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
