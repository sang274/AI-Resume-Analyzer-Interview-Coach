using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIResumeAnalyzer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInterviewSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JobDescriptionId",
                table: "InterviewSessions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSessions_JobDescriptionId",
                table: "InterviewSessions",
                column: "JobDescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewSessions_JobDescriptions_JobDescriptionId",
                table: "InterviewSessions",
                column: "JobDescriptionId",
                principalTable: "JobDescriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewSessions_JobDescriptions_JobDescriptionId",
                table: "InterviewSessions");

            migrationBuilder.DropIndex(
                name: "IX_InterviewSessions_JobDescriptionId",
                table: "InterviewSessions");

            migrationBuilder.DropColumn(
                name: "JobDescriptionId",
                table: "InterviewSessions");
        }
    }
}
