using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulsePlaylist.Migrators.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurePlaylistSettingsAndFixNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Danceability",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Energy",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Instrumentalness",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Loudness",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Tempo",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Valence",
                table: "Tracks");

            migrationBuilder.AddColumn<decimal>(
                name: "Settings_DiscoveryLevel",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Settings_MaxTempo",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Settings_MinTempo",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Settings_RecentTrackThreshold",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AudioFeatures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    TrackId = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    Bpm = table.Column<int>(type: "INTEGER", nullable: false),
                    Energy = table.Column<float>(type: "REAL", nullable: false),
                    Valence = table.Column<float>(type: "REAL", nullable: false),
                    SpotifyId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioFeatures_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    IsPublic = table.Column<bool>(type: "INTEGER", nullable: false),
                    SpotifyId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutSessions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlaylistItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    TrackId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    PlaylistId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistItems_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistItems_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutIntensitySnapshots",
                columns: table => new
                {
                    WorkoutSessionId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    HeartRate = table.Column<double>(type: "REAL", nullable: false),
                    IntensityScore = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutIntensitySnapshots", x => new { x.WorkoutSessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_WorkoutIntensitySnapshots_WorkoutSessions_WorkoutSessionId",
                        column: x => x.WorkoutSessionId,
                        principalTable: "WorkoutSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutTrackPlaybacks",
                columns: table => new
                {
                    WorkoutSessionId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    SpotifyTrackId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PositionInSession = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTrackPlaybacks", x => new { x.WorkoutSessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_WorkoutTrackPlaybacks_WorkoutSessions_WorkoutSessionId",
                        column: x => x.WorkoutSessionId,
                        principalTable: "WorkoutSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AudioFeatures_SpotifyId",
                table: "AudioFeatures",
                column: "SpotifyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AudioFeatures_TrackId",
                table: "AudioFeatures",
                column: "TrackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistItems_PlaylistId",
                table: "PlaylistItems",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistItems_TrackId",
                table: "PlaylistItems",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_OwnerId",
                table: "Playlists",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_SpotifyId",
                table: "Playlists",
                column: "SpotifyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutIntensitySnapshots_Timestamp",
                table: "WorkoutIntensitySnapshots",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_ApplicationUserId",
                table: "WorkoutSessions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_StartTime",
                table: "WorkoutSessions",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_UserId",
                table: "WorkoutSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTrackPlaybacks_PlayedAt",
                table: "WorkoutTrackPlaybacks",
                column: "PlayedAt");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTrackPlaybacks_SpotifyTrackId",
                table: "WorkoutTrackPlaybacks",
                column: "SpotifyTrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioFeatures");

            migrationBuilder.DropTable(
                name: "PlaylistItems");

            migrationBuilder.DropTable(
                name: "WorkoutIntensitySnapshots");

            migrationBuilder.DropTable(
                name: "WorkoutTrackPlaybacks");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "WorkoutSessions");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_Artist",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_Name",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Settings_DiscoveryLevel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Settings_MaxTempo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Settings_MinTempo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Settings_RecentTrackThreshold",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<double>(
                name: "Danceability",
                table: "Tracks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Energy",
                table: "Tracks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Instrumentalness",
                table: "Tracks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "Tracks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Loudness",
                table: "Tracks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "Tracks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Tempo",
                table: "Tracks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Valence",
                table: "Tracks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
