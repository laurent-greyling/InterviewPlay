using System.Collections.Generic;

namespace InterviewPlay.Models
{
    public class QuestionnaireItemModel
    {
        public int QuestionId { get; set; }

        /// <summary>
        /// Why have this again, when the questionnaire item already belongs to the questionnaire
        /// Don't think this is necessary unless somewhere we detatch this from the questionnaire, which will also not make sense
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// not sure if this is an enum in the real system? 
        /// what does type equal to?
        /// </summary>
        public int AnswerCategoryType { get; set; }

        public int OrderNumber { get; set; }

        /// <summary>
        /// Why text and not Langauge or Localization??
        /// Also the variable Text, text for what? This is more QuestionText 
        /// Why not do this with a n18i? If more langauges need to be added this seems bit unmanigiable
        /// </summary>
        public SurveyTextModel Texts { get; set; }

        public string QuestionItemText { get; set; }

        public int ItemType { get; set; }

        /// <summary>
        /// Naming all the deeper objects QuestionnaireItems does not make sense as they on the high level 
        /// questionnaire items, but in low levels they are actually something else.
        /// </summary>
        public List<CategoryModel> QuestionnaireItems { get; set; }
    }
}
