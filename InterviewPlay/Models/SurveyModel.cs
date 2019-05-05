using System.Collections.Generic;

namespace InterviewPlay.Models
{
    public class SurveyModel
    {
        public int QuestionnaireId { get; set; }

        public List<QuestionnaireModel> QuestionnaireItems { get; set; }
    }
}
