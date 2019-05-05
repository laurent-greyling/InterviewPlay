using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyDeserialise.Entities
{
    /// <summary>
    /// QuestionnaireItem entity for adding data to the QuestionnaireItem Table
    /// </summary>
    public class QuestionnaireItemEntity
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionId { get; set; }
        public int AnswerCategoryType { get; set; }
        public int OrderNumber { get; set; }
        public string QuestionnaireItemText { get; set; }
        public int ItemType { get; set; }
    }
}
