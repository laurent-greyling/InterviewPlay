using InterviewPlay.Models;
using System.Threading.Tasks;

namespace InterviewPlay.Services
{
    public interface ISqlClient
    {
        /// <summary>
        /// Create the survey table if it does not exist yet
        /// </summary>
        /// <param name="surveyId"></param>
        Task CreateSurveyTableIfNotExistAsync(int surveyId);

        /// <summary>
        /// Create table for holding respondents who submitted their questionnaires
        /// </summary>
        /// <param name="surveyId"></param>
        Task CreateRespondentTableIfNotExistAsync(int surveyId);

        /// <summary>
        /// Insert Answers into DB
        /// </summary>
        /// <param name="answers"></param>
        /// <param name="respondentId"></param>
        Task InsertAnswersAsync(RespondentAnswerModel answers);

        /// <summary>
        /// Insert respondent final state details
        /// </summary>
        /// <param name="answers"></param>
        /// <param name="respondentId"></param>
        void InsertRespodentDetails(int surveyId, string respondentId);

        /// <summary>
        /// update respondent final state details
        /// </summary>
        /// <param name="answers"></param>
        /// <param name="respondentId"></param>
        void UpdateRespodentDetails(int surveyId, string respondentId);

        /// <summary>
        /// Return the respondent interview state
        /// </summary>
        /// <param name="respondentId"></param>
        /// <returns></returns>
        bool RespondentCompleted(int surveyId, string respondentId);
    }
}
