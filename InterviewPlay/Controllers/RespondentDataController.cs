using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public RespondentDataController(ISqlClient client)
        {
            _client = client;
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

                var respondentId = respondentDataSet.Select(x => x.RespondentId).ToList().First();
                var surveyId = respondentDataSet.Select(x => x.SurveyId).ToList().First();

                //this is currently substitute for logging
                //Where this is we would want logging, real logging
                Debug.WriteLine($"Start save respondent answer data for survey {surveyId} for respondent {respondentId}");

                await _client.CreateSurveyTableIfNotExistAsync(surveyId);

                foreach (var respondentData in respondentDataSet)
                {
                    await _client.InsertAnswersAsync(respondentData);
                }

                _client.UpdateRespodentDetails(surveyId, respondentId);
                return this.Ok();
            }
            catch (Exception)
            {
                //this is currently substitute for logging
                //Where this is we would want logging, real logging
                Debug.WriteLine($"No data to be saved. This could lead to missing data");
                throw;
            }
        }
    }
}