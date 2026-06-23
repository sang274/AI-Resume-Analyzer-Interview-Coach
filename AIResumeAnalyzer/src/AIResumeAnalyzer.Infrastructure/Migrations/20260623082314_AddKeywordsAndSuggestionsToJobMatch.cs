using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIResumeAnalyzer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKeywordsAndSuggestionsToJobMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatchedKeywords",
                table: "JobMatches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Suggestions",
                table: "JobMatches",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchedKeywords",
                table: "JobMatches");

            migrationBuilder.DropColumn(
                name: "Suggestions",
                table: "JobMatches");
        }
    }
}
