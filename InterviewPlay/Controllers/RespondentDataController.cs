using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewPlay.Models;
using InterviewPlay.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPlay.Controllers
{
    [Route("api/RespondentData")]
    public class RespondentDataController : Controller
    {
        private ISqlClient _client;

        public RespondentDataController()
        {
            _client = new SqlClient();
        }

        [HttpPost("PostResponse", Name = "response")]
        public async Task<IActionResult> PostResponse([FromBody] List<RespondentAnswerModel> respondentDataSet)
        {
            try
            {
                if (respondentDataSet == null)
                {
                    throw new Exception("Respondent Data contain no elements");
                }

                var respondentId = Guid.NewGuid().ToString();
                foreach (var respondentData in respondentDataSet)
                {
                    await _client.CreateTableIfNotExistAsync(respondentData.SurveyId);
                    await _client.InsertAnswersAsync(respondentData, respondentId);
                }

                return this.Ok();
            }
            catch (NullReferenceException)
            {
                return this.NoContent();
            }
            catch (Exception e)
            {
                return this.BadRequest();
            }
        }
    }
}