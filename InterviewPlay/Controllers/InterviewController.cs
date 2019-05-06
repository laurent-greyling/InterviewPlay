using InterviewPlay.Models;
using InterviewPlay.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPlay.Controllers
{
    [Route("api/[controller]")]
    public class InterviewController : Controller
    {
        [Route("[action]/{language}")]
        public SurveyModel InterviewDetails(string language)
        {
            var interview = new BuildInterview();
            return interview.Build(language);
        }
    }
}