using InterviewPlay.Models;
using InterviewPlay.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPlay.Controllers
{
    [Route("api/[controller]")]
    public class InterviewController : Controller
    {
        private IBuildInterview _interviewBuilder;

        public InterviewController()
        {
            _interviewBuilder = new BuildInterview();
        }

        [Route("[action]/{language}/{respondentId}")]
        public SurveyModel InterviewDetails(string language, string respondentId)
        {
            return _interviewBuilder.Build(language, respondentId);
        }
    }
}