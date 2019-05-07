using System;
using System.Collections.Generic;
using InterviewPlay.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPlay.Controllers
{
    [Route("api/RespondentData")]
    public class RespondentDataController : Controller
    {
        [HttpPost("PostResponse", Name = "response")]
        public IActionResult PostResponse([FromBody] List<RespondentAnswerModel> respondentDataSet)
        {
            try
            {
                var respondentId = Guid.NewGuid().ToString();
                foreach (var respondentData in respondentDataSet)
                {
                    respondentData.RespondentId = respondentId;
                    //Save data to data base here...
                }
                return this.Ok();
            }
            catch (InvalidCastException)
            {
                return this.BadRequest();
            }
        }
    }
}