
namespace SurveyDeserialise.Models
{
    public class CategoryModel
    {
        public int AnswerId { get; set; }

        /// <summary>
        /// this belongs to the question and its id already, not sure why we would need this again?
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// I will assume in larger system all types are enums and not actually ints
        /// </summary>
        public int AnswerType { get; set; }

        public int OrderNumber { get; set; }

        public SurveyTextModel Texts { get; set; }

        public int ItemType { get; set; }

        /// <summary>
        /// do not know the signature of this item, cannot guess from the json
        /// </summary>
        public string QuestionnaireItems { get; set; }
    } 
}
