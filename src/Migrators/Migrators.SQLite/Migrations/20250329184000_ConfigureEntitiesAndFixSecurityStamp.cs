using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulsePlaylist.Migrators.SQLite.Migrations;

public partial class ConfigureEntitiesAndFixSecurityStamp : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Fix NULL security stamps first
        migrationBuilder.Sql(
            @"UPDATE AspNetUsers 
              SET SecurityStamp = LOWER(HEX(RANDOMBLOB(16))) 
              WHERE SecurityStamp IS NULL");

        // Make SecurityStamp required
        migrationBuilder.AlterColumn<string>(
            name: "SecurityStamp",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: false,
            defaultValue: Guid.NewGuid().ToString(),
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        // Add missing indexes to existing tables
        migrationBuilder.CreateIndex(
            name: "IX_Tracks_Artist",
            table: "Tracks",
            column: "Artist");

        migrationBuilder.CreateIndex(
            name: "IX_Tracks_Name",
            table: "Tracks",
            column: "Name");

        migrationBuilder.CreateIndex(
            name: "IX_AudioFeatures_Bpm",
            table: "AudioFeatures",
            column: "Bpm");

        migrationBuilder.CreateIndex(
            name: "IX_AudioFeatures_Energy_Valence",
            table: "AudioFeatures",
            columns: new[] { "Energy", "Valence" });

        // Create PlaylistSettings table with proper constraints
        migrationBuilder.CreateTable(
            name: "PlaylistSettings",
            columns: table => new
            {
                ApplicationUserId = table.Column<string>(type: "TEXT", nullable: false),
                MinTempo = table.Column<int>(type: "INTEGER", nullable: false),
                MaxTempo = table.Column<int>(type: "INTEGER", nullable: false),
                DiscoveryLevel = table.Column<float>(type: "REAL", nullable: false),
                RecentTrackThreshold = table.Column<int>(type: "INTEGER", nullable: false),
                GenreWeights = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PlaylistSettings", x => x.ApplicationUserId);
                table.ForeignKey(
                    name: "FK_PlaylistSettings_AspNetUsers_ApplicationUserId",
                    column: x => x.ApplicationUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        // Create index for tempo range queries
        migrationBuilder.CreateIndex(
            name: "IX_PlaylistSettings_MinTempo_MaxTempo",
            table: "PlaylistSettings",
            columns: new[] { "MinTempo", "MaxTempo" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Remove PlaylistSettings table
        migrationBuilder.DropTable(
            name: "PlaylistSettings");

        // Remove indexes
        migrationBuilder.DropIndex(
            name: "IX_Tracks_Artist",
            table: "Tracks");

        migrationBuilder.DropIndex(
            name: "IX_Tracks_Name",
            table: "Tracks");

        migrationBuilder.DropIndex(
            name: "IX_AudioFeatures_Bpm",
            table: "AudioFeatures");

        migrationBuilder.DropIndex(
            name: "IX_AudioFeatures_Energy_Valence",
            table: "AudioFeatures");

        // Revert SecurityStamp to nullable
        migrationBuilder.AlterColumn<string>(
            name: "SecurityStamp",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: false,
            oldDefaultValue: "");
    }
}
