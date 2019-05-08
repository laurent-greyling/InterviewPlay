using InterviewPlay.Models;
using System.Threading.Tasks;

namespace InterviewPlay.Services
{
    interface ISqlClient
    {
        /// <summary>
        /// Create the survey table if it does not exist yet
        /// </summary>
        /// <param name="surveyId"></param>
        Task CreateTableIfNotExistAsync(int surveyId);

        /// <summary>
        /// Insert Answers into DB
        /// </summary>
        /// <param name="answers"></param>
        /// <param name="respondentId"></param>
        Task InsertAnswersAsync(RespondentAnswerModel answers, string respondentId);
    }
}
