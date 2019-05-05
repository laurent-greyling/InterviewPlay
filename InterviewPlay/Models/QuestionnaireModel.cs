using System.Collections.Generic;

namespace InterviewPlay.Models
{
    public class QuestionnaireModel
    {
        public int SubjectId { get; set; }

        public int OrderNumber { get; set; }

        /// <summary>
        /// Why text and not Langauge or Localization??
        /// Also the variable Text, text for what? This is more QuestionText 
        /// Why not do this with a n18i? If more langauges need to be added this seems bit unmanigiable
        /// </summary>
        public SurveyTextModel Texts { get; set; }

        /// <summary>
        /// What is Itemtype?
        /// </summary>
        public int ItemType { get; set; }

        public List<QuestionnaireItemModel> QuestionnaireItems { get; set; }
    }
}
