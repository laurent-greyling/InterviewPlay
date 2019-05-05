using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyDeserialise.Migrations
{
    public partial class CreateSurveyDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false),
                    AnswerType = table.Column<int>(nullable: false),
                    OrderNumber = table.Column<int>(nullable: false),
                    CategoryTexts = table.Column<string>(nullable: true),
                    ItemType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.AnswerId);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaire",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false),
                    OrderNumber = table.Column<int>(nullable: false),
                    QuestionnaireText = table.Column<string>(nullable: true),
                    ItemType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireItem",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false),
                    AnswerCategoryType = table.Column<int>(nullable: false),
                    OrderNumber = table.Column<int>(nullable: false),
                    QuestionnaireItemText = table.Column<string>(nullable: true),
                    ItemType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireItem", x => x.QuestionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Questionnaire");

            migrationBuilder.DropTable(
                name: "QuestionnaireItem");
        }
    }
}
