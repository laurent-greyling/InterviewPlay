using InterviewPlay.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace InterviewPlay.Services
{
    public class BuildInterview : IBuildInterview
    {
        /// <summary>
        /// This would not sit in file structure like this usually, this will come from DB or blob or some place more secure
        /// </summary>
        //private readonly string SurveyJson = File.ReadAllText("questionnaire.json");
        private SurveyModel _survey;
        private ISqlClient _client;

        /// <summary>
        /// Initialise the BuildInterview
        /// </summary>
        /// <param name="surveyJson">This can be used to send different Json file as questionnaire. Only here now of using the demo Json for project</param>
        public BuildInterview(ISqlClient client, string surveyJson = "")
        {
            //this is currently substitute for logging
            //Where this is we would want logging, real logging
            Debug.WriteLine($"Initialise BuildInterview controller");

            //This is for the purpose of this project, I use the surveyJson to pass in the json from tests
            if (string.IsNullOrEmpty(surveyJson))
            {
                surveyJson = File.ReadAllText("questionnaire.json");
            }
            _survey = JsonConvert.DeserializeObject<SurveyModel>(surveyJson);
            _client = client;
        }

        public SurveyModel Build(string language, string respondentId)
        {
            //this is currently substitute for logging
            //Where this is we would want logging, real logging
            Debug.WriteLine($"Build survey for respondent {respondentId} with language {language}");

            _client.InsertRespodentDetails(_survey.QuestionnaireId, respondentId);

            foreach (var subject in _survey.QuestionnaireItems)
            {
                //For more langauges this will need to be smarter
                subject.SubjectText = language == "nl" ? subject.Texts.NlNl : subject.Texts.EnUs;

                foreach (var question in subject.QuestionnaireItems)
                {
                    question.QuestionItemText = language == "nl" ? question.Texts.NlNl : question.Texts.EnUs;
                    if (question.QuestionnaireItems == null)
                        continue;

                    foreach (var category in question.QuestionnaireItems)
                    {
                        category.CategoryText = language == "nl" ? category.Texts.NlNl : category.Texts.EnUs;
                    }
                }
            }

            return _survey;
        }

        /// <summary>
        /// Check if respondent has finished a survey already or not
        /// </summary>
        /// <param name="respondentId"></param>
        /// <returns></returns>
        public async Task<bool> RespondentSurveyState(string respondentId)
        {
            //this is currently substitute for logging
            //Where this is we would want logging, real logging
            Debug.WriteLine($"Check if {respondentId} has already completed the interview");

            await _client.CreateRespondentTableIfNotExistAsync(_survey.QuestionnaireId);
            return _client.RespondentCompleted(_survey.QuestionnaireId, respondentId);
        }
    }
}
