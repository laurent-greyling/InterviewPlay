using InterviewPlay.Models;
using InterviewPlay.Services;
using Microsoft.AspNetCore.Mvc;
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
            if (await _interviewBuilder.RespondentSurveyState(respondentId))
            {
                return null;
            }
            return _interviewBuilder.Build(language, respondentId);
        }
    }
}