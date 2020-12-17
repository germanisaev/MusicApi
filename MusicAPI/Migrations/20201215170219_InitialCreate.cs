using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlbumDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Artist = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Album = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Genres = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Created = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Song = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Album = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    MusicFile = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongsPlatlistDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Song = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Playlist = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    MusicFile = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongsPlatlistDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SortDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Artists = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Albums = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Songs = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Genres = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Created = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Created = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SortedId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistDetails_SortDetails_SortedId",
                        column: x => x.SortedId,
                        principalTable: "SortDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistDetails_SortedId",
                table: "PlaylistDetails",
                column: "SortedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumDetails");

            migrationBuilder.DropTable(
                name: "PlaylistDetails");

            migrationBuilder.DropTable(
                name: "SongDetails");

            migrationBuilder.DropTable(
                name: "SongsPlatlistDetails");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "UserDtos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SortDetails");
        }
    }
}
