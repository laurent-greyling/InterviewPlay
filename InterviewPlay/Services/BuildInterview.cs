using InterviewPlay.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace InterviewPlay.Services
{
    public class BuildInterview : IBuildInterview
    {
        /// <summary>
        /// This would not sit in file structure like this usually, this will come from DB or blob or some place more secure
        /// </summary>
        private readonly string SurveyJson = File.ReadAllText("questionnaire.json");
        private SurveyModel _survey;

        public BuildInterview()
        {
            _survey = JsonConvert.DeserializeObject<SurveyModel>(SurveyJson);
        }

        public SurveyModel Build()
        {
            _survey = JsonConvert.DeserializeObject<SurveyModel>(SurveyJson);
            //var interview = new InterviewModel { InterviewId = _survey.QuestionnaireId };

            foreach (var subject in _survey.QuestionnaireItems)
            {
                subject.SubjectText = subject.Texts.EnUs;

                foreach (var question in subject.QuestionnaireItems)
                {
                    if (question.QuestionnaireItems == null)
                        continue;

                    foreach (var category in question.QuestionnaireItems)
                    {
                    }
                }
            }

            return _survey;
        }
    }
}
