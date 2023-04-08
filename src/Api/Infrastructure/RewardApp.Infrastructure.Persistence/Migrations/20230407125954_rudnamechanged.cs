using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RewardApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class rudnamechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RewardUserDetail",
                schema: "dbo",
                table: "rewarduser",
                newName: "RewardUserDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RewardUserDetails",
                schema: "dbo",
                table: "rewarduser",
                newName: "RewardUserDetail");
        }
    }
}
