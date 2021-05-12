using Microsoft.EntityFrameworkCore.Migrations;

namespace Entitys.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LevelsDescriptors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descriptor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelsDescriptors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Answer = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsDescriptors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descriptor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsDescriptors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    LevelsDescriptorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_LevelsDescriptors_LevelsDescriptorId",
                        column: x => x.LevelsDescriptorId,
                        principalTable: "LevelsDescriptors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    LevelsId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionDescriptorId = table.Column<int>(type: "INTEGER", nullable: true),
                    QuestionAnswerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Levels_LevelsId",
                        column: x => x.LevelsId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionsAnswers_QuestionAnswerId",
                        column: x => x.QuestionAnswerId,
                        principalTable: "QuestionsAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionsDescriptors_QuestionDescriptorId",
                        column: x => x.QuestionDescriptorId,
                        principalTable: "QuestionsDescriptors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_LevelsDescriptorId",
                table: "Levels",
                column: "LevelsDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_LevelsId",
                table: "Questions",
                column: "LevelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionAnswerId",
                table: "Questions",
                column: "QuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionDescriptorId",
                table: "Questions",
                column: "QuestionDescriptorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "QuestionsAnswers");

            migrationBuilder.DropTable(
                name: "QuestionsDescriptors");

            migrationBuilder.DropTable(
                name: "LevelsDescriptors");
        }
    }
}
