using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VocabularyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserWordFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWords_PartsOfSpeech_PartOfSpeechId",
                table: "UserWords");

            migrationBuilder.DropIndex(
                name: "IX_UserWords_UserId_WordId_PartOfSpeechId",
                table: "UserWords");

            migrationBuilder.DropColumn(
                name: "CustomDefinition",
                table: "UserWords");

            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "UserWords");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "UserWords");

            migrationBuilder.AlterColumn<int>(
                name: "PartOfSpeechId",
                table: "UserWords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserWords_UserId",
                table: "UserWords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWords_PartsOfSpeech_PartOfSpeechId",
                table: "UserWords",
                column: "PartOfSpeechId",
                principalTable: "PartsOfSpeech",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWords_PartsOfSpeech_PartOfSpeechId",
                table: "UserWords");

            migrationBuilder.DropIndex(
                name: "IX_UserWords_UserId",
                table: "UserWords");

            migrationBuilder.AlterColumn<int>(
                name: "PartOfSpeechId",
                table: "UserWords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomDefinition",
                table: "UserWords",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevel",
                table: "UserWords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "UserWords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserWords_UserId_WordId_PartOfSpeechId",
                table: "UserWords",
                columns: new[] { "UserId", "WordId", "PartOfSpeechId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWords_PartsOfSpeech_PartOfSpeechId",
                table: "UserWords",
                column: "PartOfSpeechId",
                principalTable: "PartsOfSpeech",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
