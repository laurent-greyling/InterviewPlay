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

        public SurveyModel Build(string language)
        {
            _survey = JsonConvert.DeserializeObject<SurveyModel>(SurveyJson);

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
    }
}
