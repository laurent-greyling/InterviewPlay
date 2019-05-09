using InterviewPlay.Models;
using System.Threading.Tasks;

namespace InterviewPlay.Services
{
    public interface IBuildInterview
    {
        /// <summary>
        /// This is used to create the survey model that is passed to the UI
        /// 'Will also set the text to what was selected
        /// </summary>
        /// <param name="language">Language you want to conduct survey in</param>
        /// <returns></returns>
        SurveyModel Build(string language, string respondentId);

        /// <summary>
        /// Determine if respondent is allowed to continue or if responent is already finished
        /// </summary>
        /// <param name="respondentId"></param>
        /// <returns></returns>
        Task<bool> RespondentSurveyState(string respondentId);
    }
}
