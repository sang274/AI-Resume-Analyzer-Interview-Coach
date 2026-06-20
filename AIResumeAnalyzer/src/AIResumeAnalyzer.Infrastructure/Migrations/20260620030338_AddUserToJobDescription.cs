using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIResumeAnalyzer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToJobDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "JobDescriptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JobDescriptions_UserId",
                table: "JobDescriptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobDescriptions_Users_UserId",
                table: "JobDescriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobDescriptions_Users_UserId",
                table: "JobDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_JobDescriptions_UserId",
                table: "JobDescriptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobDescriptions");
        }
    }
}
