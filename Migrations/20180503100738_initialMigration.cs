using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Genre = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    PageCount = table.Column<int>(nullable: false),
                    Publisher = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    RatingCount = table.Column<int>(nullable: false),
                    ReleaseYear = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
