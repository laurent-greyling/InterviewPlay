using InterviewPlay.Models;
using InterviewPlay.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPlay.Controllers
{
    [Route("api/[controller]")]
    public class InterviewController : Controller
    {
        [HttpGet("[action]")]
        public SurveyModel InterviewDetails()
        {
            var interview = new BuildInterview();
            return interview.Build();
        }
    }
}