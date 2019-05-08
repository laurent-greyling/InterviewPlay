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

        public async Task CreateTableIfNotExistAsync(int surveyId)
        {
            //Currently this string is not safe. need it paramaterized to protect against sql injection
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
            await _context.Database.ExecuteSqlCommandAsync(createIfNotExist);
        }

        public async Task InsertAnswersAsync(RespondentAnswerModel answers, string respondentId)
        {
            //Currently this string is not safe. need it paramaterized to protect against sql injection
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

           await _context.Database.ExecuteSqlCommandAsync(insertAsnswers);
        }
    }
}
