using InterviewPlay.Models;

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
        SurveyModel Build(string language);
    }
}
