using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RewardApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class businessMain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reward",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RewardKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RewardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Repeat = table.Column<short>(type: "smallint", nullable: false),
                    Mod = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reward_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "winner",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "assignment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assignment_reward_RewardId",
                        column: x => x.RewardId,
                        principalSchema: "dbo",
                        principalTable: "reward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignment_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rewarduser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    Mod = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rewarduser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rewarduser_reward_RewardId",
                        column: x => x.RewardId,
                        principalSchema: "dbo",
                        principalTable: "reward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rewarduser_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignment_RewardId",
                schema: "dbo",
                table: "assignment",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_assignment_UserId",
                schema: "dbo",
                table: "assignment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_reward_CreatedById",
                schema: "dbo",
                table: "reward",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_rewarduser_RewardId",
                schema: "dbo",
                table: "rewarduser",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_rewarduser_UserId",
                schema: "dbo",
                table: "rewarduser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "rewarduser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "winner",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "reward",
                schema: "dbo");
        }
    }
}
