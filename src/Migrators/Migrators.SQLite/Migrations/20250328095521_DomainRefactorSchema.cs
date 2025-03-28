using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulsePlaylist.Migrators.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class DomainRefactorSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    SpotifyId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Artist = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    DurationMs = table.Column<int>(type: "INTEGER", nullable: false),
                    SpotifyUri = table.Column<string>(type: "TEXT", nullable: true),
                    Tempo = table.Column<double>(type: "REAL", nullable: false),
                    Energy = table.Column<double>(type: "REAL", nullable: false),
                    Danceability = table.Column<double>(type: "REAL", nullable: false),
                    Valence = table.Column<double>(type: "REAL", nullable: false),
                    Instrumentalness = table.Column<double>(type: "REAL", nullable: false),
                    Key = table.Column<int>(type: "INTEGER", nullable: false),
                    Mode = table.Column<int>(type: "INTEGER", nullable: false),
                    Loudness = table.Column<double>(type: "REAL", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_SpotifyId",
                table: "Tracks",
                column: "SpotifyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tracks");
        }
    }
}
