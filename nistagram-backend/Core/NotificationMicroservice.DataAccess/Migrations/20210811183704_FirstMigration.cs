using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationMicroservice.DataAccess.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FollowingUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follows_RegisteredUsers_FollowedByUserId",
                        column: x => x.FollowedByUserId,
                        principalTable: "RegisteredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_RegisteredUsers_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "RegisteredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsNotifiedByFollowRequests = table.Column<bool>(type: "bit", nullable: false),
                    IsNotifiedByMessages = table.Column<bool>(type: "bit", nullable: false),
                    IsNotifiedByPosts = table.Column<bool>(type: "bit", nullable: false),
                    IsNotifiedByStories = table.Column<bool>(type: "bit", nullable: false),
                    IsNotifiedByComments = table.Column<bool>(type: "bit", nullable: false),
                    LoggedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NotificationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationOptions_RegisteredUsers_LoggedUserId",
                        column: x => x.LoggedUserId,
                        principalTable: "RegisteredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationOptions_RegisteredUsers_NotificationByUserId",
                        column: x => x.NotificationByUserId,
                        principalTable: "RegisteredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegisteredUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_RegisteredUsers_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "RegisteredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Contents",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123412341234"), "Post" },
                    { new Guid("12345678-1234-1234-1234-123412341235"), "Story" }
                });

            migrationBuilder.InsertData(
                table: "RegisteredUsers",
                columns: new[] { "Id", "ProfilePicturePath", "Username" },
                values: new object[,]
                {
                    { new Guid("4ddf3498-2204-4a40-b2d4-501c44307e46"), "", "diplomski" },
                    { new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"), "", "diplomskiv2" }
                });

            migrationBuilder.InsertData(
                table: "Follows",
                columns: new[] { "Id", "FollowedByUserId", "FollowingUserId" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"), new Guid("4ddf3498-2204-4a40-b2d4-501c44307e46") },
                    { new Guid("12345678-1234-1234-1234-123412341235"), new Guid("4ddf3498-2204-4a40-b2d4-501c44307e46"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc") }
                });

            migrationBuilder.InsertData(
                table: "NotificationOptions",
                columns: new[] { "Id", "IsNotifiedByComments", "IsNotifiedByFollowRequests", "IsNotifiedByMessages", "IsNotifiedByPosts", "IsNotifiedByStories", "LoggedUserId", "NotificationByUserId" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), true, false, false, true, true, new Guid("4ddf3498-2204-4a40-b2d4-501c44307e46"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc") });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "ContentId", "RegisteredUserId", "TimeStamp" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341234"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"), new DateTime(2021, 8, 11, 20, 37, 4, 166, DateTimeKind.Local).AddTicks(1768) },
                    { new Guid("12345678-1234-1234-1234-123412341235"), new Guid("12345678-1234-1234-1234-123412341235"), new Guid("44406823-9cf7-4bf6-8950-efbcd5bd2bdc"), new DateTime(2021, 8, 11, 20, 37, 4, 169, DateTimeKind.Local).AddTicks(532) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowedByUserId",
                table: "Follows",
                column: "FollowedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowingUserId",
                table: "Follows",
                column: "FollowingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationOptions_LoggedUserId",
                table: "NotificationOptions",
                column: "LoggedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationOptions_NotificationByUserId",
                table: "NotificationOptions",
                column: "NotificationByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContentId",
                table: "Notifications",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RegisteredUserId",
                table: "Notifications",
                column: "RegisteredUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "NotificationOptions");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "RegisteredUsers");
        }
    }
}
