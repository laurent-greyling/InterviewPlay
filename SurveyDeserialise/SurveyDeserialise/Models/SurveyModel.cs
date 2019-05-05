using System.Collections.Generic;

namespace SurveyDeserialise.Models
{
    public class SurveyModel
    {
        public int QuestionnaireId { get; set; }

        public List<QuestionnaireModel> QuestionnaireItems { get; set; }
    }
}
