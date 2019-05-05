using Newtonsoft.Json;
using SurveyDeserialise.Entities;
using SurveyDeserialise.Models;
using System.IO;

namespace SurveyDeserialise.SurveyDetails
{
    public class SurveyDetailsRepository
    {
        private readonly SurveyDbContext _context;

        /// <summary>
        /// This would not sit in file structure like this usually, this will come from DB or blob or some place more secure
        /// </summary>
        private static readonly string SurveyJson = File.ReadAllText("../../../questionnaire.json");

        /// <summary>
        /// This is used to fill the Database tables with the Subject, Question and Category details
        /// These tables can/will be used as lookup tables. When a survey answers are saved it will only save the ids 
        /// of answers selected and the question/subject ids these answeres belong to.
        /// Then these tables can be used to see what text equals to for selected quesitons and answers
        /// </summary>
        /// <param name="context"></param>
        public SurveyDetailsRepository(SurveyDbContext context)
        {
            _context = context;
        }

        public void UpdateSurveyDetails()
        {
            var survey = JsonConvert.DeserializeObject<SurveyModel>(SurveyJson);

            foreach (var subject in survey.QuestionnaireItems)
            {
                var subjectEntity = MapQuestionnaireEntity(subject);

                _context.Questionnaire.Add(subjectEntity);

                foreach (var question in subject.QuestionnaireItems)
                {
                    var questionEntity = MapQuestionnaireItemEntity(question);

                    _context.QuestionnaireItem.Add(questionEntity);

                    if (question.QuestionnaireItems == null)
                        continue;

                    foreach (var category in question.QuestionnaireItems)
                    {
                        if (_context.Categories.Find(category.AnswerId) != null)
                            continue;

                        var categoryEntity = MapCategoryEntity(category);

                        _context.Categories.Add(categoryEntity);
                    }
                }
            }

            _context.SaveChanges();
        }

        private static CategoryEntity MapCategoryEntity(CategoryModel model)
        {
            return new CategoryEntity
            {
                AnswerId = model.AnswerId,
                AnswerType = model.AnswerType,
                OrderNumber = model.OrderNumber,
                CategoryTexts = model.Texts.EnUs, //only use the english one for this as it doesn't matter what language the respondent answer in, this can be mapped later in analyses based on customer preference
                ItemType = model.ItemType
            };
        }

        private static QuestionnaireItemEntity MapQuestionnaireItemEntity(QuestionnaireItemModel model)
        {
            return new QuestionnaireItemEntity
            {
                QuestionId = model.QuestionId,
                AnswerCategoryType = model.AnswerCategoryType,
                OrderNumber = model.OrderNumber,
                QuestionnaireItemText = model.Texts.EnUs, //only use the english one for this as it doesn't matter what language the respondent answer in, this can be mapped later in analyses based on customer preference
                ItemType = model.ItemType
            };
        }

        private static QuestionnaireEntity MapQuestionnaireEntity(QuestionnaireModel model)
        {
            return new QuestionnaireEntity
            {
                SubjectId = model.SubjectId,
                OrderNumber = model.OrderNumber,
                QuestionnaireText = model.Texts.EnUs, //only use the english one for this as it doesn't matter what language the respondent answer in, this can be mapped later in analyses based on customer preference
                ItemType = model.ItemType
            };
        }
    }
}
