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

        public void CreateRespondentTableIfNotExist(int surveyId)
        {
            var createIfNotExist = $@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_NAME = N'RespondentFinished_{surveyId}')
BEGIN
  CREATE TABLE RespondentFinished_{surveyId} (
    RespondentId varchar(255) NOT NULL PRIMARY KEY,
    IsFinished BIT DEFAULT 0,
);
END";
            _context.Database.ExecuteSqlCommandAsync(createIfNotExist);
        }

        public async Task CreateSurveyTableIfNotExistAsync(int surveyId)
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

        public async Task InsertAnswersAsync(RespondentAnswerModel answers)
        {
            //Currently this string is not safe. need it paramaterized to protect against sql injection
            var insertAsnswers = $@"INSERT INTO RespondentAnswer_{answers.SurveyId} (
RespondentId,
SubjectId,
QuestionId,
CategoryId,
OpenAnswer) VALUES (
'{answers.RespondentId}',
{answers.SubjectId},
{answers.QuestionId},
{answers.CategoryId},
'{answers.OpenAnswer}')";

           await _context.Database.ExecuteSqlCommandAsync(insertAsnswers);
        }

        public void InsertRespodentDetails(int surveyId, string respondentId)
        {
            var insertAsnswers = $@"INSERT INTO RespondentFinished_{surveyId} (RespondentId) VALUES ('{respondentId}')";

            _context.Database.ExecuteSqlCommandAsync(insertAsnswers);
        }

        public bool RespondentCompleted(int surveyId, string respondentId)
        {
            var respondentstate = $"SELECT * FROM RespondentFinished_{surveyId} WHERE RespondentId = '{respondentId}'";
            var respondent = _context.RespondentFinalState.FromSql(respondentstate).ToList();
            return respondent.Count > 0 ? respondent.First().IsFinished : false;
        }

        public void UpdateRespodentDetails(int surveyId, string respondentId)
        {
            var insertAsnswers = $@"UPDATE RespondentFinished_{surveyId} SET IsFinished = 1 WHERE RespondentId='{respondentId}'";

            _context.Database.ExecuteSqlCommandAsync(insertAsnswers);
        }
    }
}
