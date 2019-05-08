using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewPlay.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewPlay.Services
{
    public class SqlClient : ISqlClient
    {
        private SurveyDbContext _context;
        public SqlClient()
        {
            _context = new SurveyDbContext();
        }

        public void CreateTableIfNotExist(int surveyId)
        {
            var createIfNotExist = $@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_NAME = N'RespondentAnswer_{surveyId}')
BEGIN
  CREATE TABLE RespondentAnswer_{surveyId} (
    RespondentId varchar(255),
    SubjectId int,
    QuestionId int,
    CategoryId int,
    OpenAnswer varchar(255)
);
CREATE INDEX idx_category_question
ON RespondentAnswer_{surveyId} (CategoryId, QuestionId);
END";
            _context.Database.ExecuteSqlCommand(createIfNotExist);
        }

        public void InsertAnswers(RespondentAnswerModel answers, string respondentId)
        {
            var insertAsnswers = $@"INSERT INTO RespondentAnswer_{answers.SurveyId} (
RespondentId,
SubjectId,
QuestionId,
CategoryId,
OpenAnswer) VALUES (
'{respondentId}',
{answers.SubjectId},
{answers.QuestionId},
{answers.CategoryId},
'{answers.OpenAnswer}')";

            _context.Database.ExecuteSqlCommand(insertAsnswers);
        }
    }
}
