using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIResumeAnalyzer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInterviewSessionSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImprovementSuggestions",
                table: "InterviewSessions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "InterviewSessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OverallFeedback",
                table: "InterviewSessions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Strengths",
                table: "InterviewSessions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weaknesses",
                table: "InterviewSessions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImprovementSuggestions",
                table: "InterviewSessions");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "InterviewSessions");

            migrationBuilder.DropColumn(
                name: "OverallFeedback",
                table: "InterviewSessions");

            migrationBuilder.DropColumn(
                name: "Strengths",
                table: "InterviewSessions");

            migrationBuilder.DropColumn(
                name: "Weaknesses",
                table: "InterviewSessions");
        }
    }
}
