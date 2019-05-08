using InterviewPlay.Models;

namespace InterviewPlay.Services
{
    interface ISqlClient
    {
        void CreateTableIfNotExist(int surveyId);

        void InsertAnswers(RespondentAnswerModel answers, string respondentId);
    }
}
