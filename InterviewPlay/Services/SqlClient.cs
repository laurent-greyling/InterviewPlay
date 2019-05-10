using System.Data.SqlClient;
using System.Diagnostics;
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

        public async Task CreateRespondentTableIfNotExistAsync(int surveyId)
        {
            //this is currently substitute for logging
            //Where this is we would want logging, real logging
            Debug.WriteLine($"Check if table for respondent finish state for survey {surveyId} exist, if not create");

            var createIfNotExist = $@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
           WHERE TABLE_NAME = N'RespondentFinished_{surveyId}')
BEGIN
  CREATE TABLE RespondentFinished_{surveyId} (
    RespondentId varchar(255) NOT NULL PRIMARY KEY,
    IsFinished BIT DEFAULT 0,
);
END";
            await _context.Database.ExecuteSqlCommandAsync(createIfNotExist);
        }

        public async Task CreateSurveyTableIfNotExistAsync(int surveyId)
        {
            //this is currently substitute for logging
            //Where this is we would want logging, real logging
            Debug.WriteLine($"Create survey answer data table for {surveyId}");

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
            //parametarize atleast the fields that is user input.
            var insertAsnswers = $@"INSERT INTO RespondentAnswer_{answers.SurveyId} (
RespondentId,
SubjectId,
QuestionId,
CategoryId,
OpenAnswer) VALUES (
@respondentId,
@subjectId,
@questionId,
@categoryId,
@openAnswer)";

           await _context.Database.ExecuteSqlCommandAsync(insertAsnswers,
               new SqlParameter("@respondentId", answers.RespondentId),
               new SqlParameter("@subjectId", answers.SubjectId),
               new SqlParameter("@questionId", answers.QuestionId),
               new SqlParameter("@categoryId", answers.CategoryId),
               new SqlParameter("@openAnswer", answers.OpenAnswer == null ? string.Empty : answers.OpenAnswer));
        }

        public void InsertRespodentDetails(int surveyId, string respondentId)
        {
            //this is currently substitute for logging
            //Where this is we would want logging, real logging
            Debug.WriteLine($"Insert respondent {respondentId} for survey {surveyId} into RespondentFinished table with finished state as false");

            //parametarize atleast the fields that is user input.
            var insertAsnswers = $@"INSERT INTO RespondentFinished_{surveyId} (RespondentId) VALUES (@respondentId)";

            _context.Database.ExecuteSqlCommandAsync(insertAsnswers, new SqlParameter("@respondentId", respondentId));
        }

        public bool RespondentCompleted(int surveyId, string respondentId)
        {
            //parametarize atleast the fields that is user input.
            var respondentstate = $"SELECT * FROM RespondentFinished_{surveyId} WHERE RespondentId = @respondentId";
            var respondent = _context.RespondentFinalState.FromSql(respondentstate, new SqlParameter("@respondentId", respondentId)).ToList();
            return respondent.Count > 0 ? respondent.First().IsFinished : false;
        }

        public void UpdateRespodentDetails(int surveyId, string respondentId)
        {
            //this is currently substitute for logging
            //Where this is we would want logging, real logging
            Debug.WriteLine($"Update respondent {respondentId} finsish state for survey {surveyId}");

            //parametarize atleast the fields that is user input.
            var insertAsnswers = $@"UPDATE RespondentFinished_{surveyId} SET IsFinished = 1 WHERE RespondentId=@respondentId";

            _context.Database.ExecuteSqlCommandAsync(insertAsnswers, new SqlParameter("@respondentId", respondentId));
        }
    }
}
