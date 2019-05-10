using InterviewPlay.Models;
using InterviewPlay.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InterviewPlay.Controllers
{
    [Route("api/[controller]")]
    public class InterviewController : Controller
    {
        private IBuildInterview _interviewBuilder;

        public InterviewController(IBuildInterview interviewBuilder)
        {
            _interviewBuilder = interviewBuilder;
        }

        [Route("[action]/{language}/{respondentId}")]
        public async Task<SurveyModel> InterviewDetails(string language, string respondentId)
        {
            try
            {
                //this is currently substitute for logging
                //Where this is we would want logging, real logging
                Debug.WriteLine($"Start building the interview for respondent {respondentId}");

                if (await _interviewBuilder.RespondentSurveyState(respondentId))
                {
                    return null;
                }
                return _interviewBuilder.Build(language, respondentId);
            }
            catch (System.Exception e)
            {
                //this is currently substitute for logging
                //Where this is we would want logging, real logging
                Debug.WriteLine(e.Message);
                throw;
            }            
        }
    }
}